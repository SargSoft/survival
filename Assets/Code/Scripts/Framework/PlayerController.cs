﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerInputController {

	private PlayerInputs playerInputs;
	private movementState currentPlayerState;
	private movementState previousPlayerState;

	[Header("Mouse Settings")]
	[SerializeField] private float mouseYSensitivity;
	[SerializeField] private float mouseXSensitivity;

	[Header("Movement Options")]
	[SerializeField] private float baseMoveSpeed;
	[SerializeField] private float runMultiplier;
	[SerializeField] private float swimMultiplier;
	[SerializeField] private float crouchMultiplier;
	[SerializeField] private float crouchCameraMove;
	private bool isRun;
	private bool isCrouch;
	private bool inWater;

	[Header("Jump Options")]
	[SerializeField] private float jumpHeight;
	[SerializeField] private float jumpTime;
	[Range(0,1)][SerializeField] private float airControlPercentForward;
	[Range(0,1)][SerializeField] private float airControlPercentSideways;
	private float jumpCount;
	private float remainingJumpHeight;
	private bool isJump;

 	[Header("Physics")]
 	[SerializeField] private LayerMask discludePlayer;
 	[SerializeField] private float maxSlopeAngle;
 	[SerializeField] private bool slideOnSlope;
 	[SerializeField] private float slideMultiplier;
 	private bool isSteep;

 	[Header("References")]
 	[SerializeField] private CapsuleCollider capsuleCol;

 	[Header("Game Object")]
 	[SerializeField] private Transform bodyObject;
	[SerializeField] private Transform cameraObject;
	[SerializeField] private GameObject water;
	
	// Private Variables
	private Vector3 velocity;
	private Vector3 move;
	private Vector3 vel;
	private bool grounded;
	private Vector3 initialPosition;


	private void Awake() {
		LockCursor();
		isJump = false;
		currentPlayerState = movementState.Walk;
		previousPlayerState = movementState.Walk;
	}

	private void Update() {
		playerInputs = ReturnPlayerInputs();
		grounded = Grounded(transform.position, capsuleCol);
		initialPosition = transform.position;
		isSteep = IsSteep(SteepCheck(bodyObject), maxSlopeAngle);

		currentPlayerState = StateCheck(bodyObject, water);
		if (previousPlayerState != currentPlayerState) {
			Debug.Log(currentPlayerState);
		}
		previousPlayerState = currentPlayerState;

		CameraRotation();
		SimpleMove();
		PlayerPhysics();
		FinalMove();
		PlayerCollision();
	}

	// Calls all over the methods related to the players physics in the correct order
	private void PlayerPhysics() {
		// Gravity, jump, run, crouch
		velocity.y = SetSingleVelocity(velocity.y, Gravity(velocity, grounded, isJump));
		// Swim();
		Jump();
		Run();
		Crouch();
	}

	private void PlayerCollision() {
		
		float steepness = SteepCheck(bodyObject);

		// StickToGround
		Vector3 yPosition = StickToGround(transform.position, isJump, velocity.y);
		if (yPosition != transform.position) {
			transform.position = yPosition;
		}

		// Collision
		Vector3 collPosition = Collision(discludePlayer, capsuleCol, bodyObject, steepness);
		if (collPosition != transform.position) {
			transform.position = collPosition;
		}

		if (slideOnSlope) {
			Vector3 slidePosition = SlideOnSlope(transform.position, maxSlopeAngle, grounded, slideMultiplier);
			if (slidePosition != transform.position) {
				transform.position = slidePosition;
			}
		}

		// SlopeLimit
		if (steepness > maxSlopeAngle) {
			SlopeLimit(initialPosition, bodyObject, capsuleCol);
		}
	}

	// Locks the cursor
	private void LockCursor() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Takes the mouseX and mouseY, clamps the X rotation, adds the mouseX and mouseY to the quaternions yRot and xRot, and then rotates the camera and object appropriately
	private void CameraRotation() {
		float mouseX = playerInputs.mouseX * mouseXSensitivity;
		float mouseY = playerInputs.mouseY * mouseYSensitivity;

		mouseY = ClampXRotation(cameraObject.eulerAngles.x, mouseY);

		Quaternion yRot = transform.localRotation * Quaternion.Euler(0f, mouseX, 0f);
		Quaternion xRot = cameraObject.localRotation * Quaternion.Euler(-mouseY, 0f, 0f);

		cameraObject.localRotation = xRot;
		transform.localRotation = yRot;

		// For Swimming:
		// transform.eulerAngles = (Vector3.left * mouseY) + (Vector3.up * mouseX);
	}

	// Takes the horizontal and vertical inputs of the player for movement and adds them to velocity
	private void SimpleMove() {
		move = playerInputs.move;

		if (grounded) {
			velocity = ResetVelocity(velocity);
			velocity = AddVelocity(velocity, Vector3.Normalize(move));
		} else {
			velocity.z = AddSingleVelocity(velocity.z, AirMoveVector(velocity.z, move.z, airControlPercentForward));
			velocity.x = AddSingleVelocity(velocity.x, AirMoveVector(velocity.x, move.x, airControlPercentSideways));
		}
	}

	// Calculates the velocity (player movement this game tick) and transforms the player appropriately
	private void FinalMove() {
		float runSpeed = RunSpeed();
		float speedMult = SpeedMult();

		if (isSteep) {
			velocity = SetVelocity(velocity, new Vector3 (0, velocity.y, 0));
		}

		Vector3 vel = new Vector3(velocity.x * speedMult, velocity.y, velocity.z * runSpeed * speedMult) * baseMoveSpeed;

		vel = transform.TransformDirection(vel);
		transform.position += vel * Time.fixedDeltaTime;
	}

	// Returns a float that is the difference between the first two inputs multiplied by the third, which is a fraction. Used to reduce movement while not grounded.
	private float AirMoveVector(float current, float proposed, float AirControl) {
		float output = (proposed - current) * AirControl;
		return output;
	}

	// Checks if the jumpInput key has been pressed and then starts the jump count and the first JumpEvent()
	private void Jump() {
		if (playerInputs.jump == keyState.Down && grounded && !isJump && !isCrouch && !isSteep){
			isJump = true;
			jumpCount = jumpTime + 1f;
			remainingJumpHeight = jumpHeight;
			JumpEvent();
		} else if (isJump) {
			JumpEvent();
		}
	}

	// Checks the jump count, and if its still greater than 0 it addes y velocity accordingly, decreasing as the jumpCount does until ultimaltey isJump is assigned false
	private void JumpEvent() {
		if (jumpCount > 0) {
			jumpCount -= 1.0f;
			velocity.y = SetSingleVelocity(velocity.y, remainingJumpHeight / jumpTime);
			remainingJumpHeight -= remainingJumpHeight / jumpTime;
		} else if (jumpCount == 0) {
			isJump = false;
		}
	}

	// When the run input is pressed down or released, the moveSpeed variables is assigned the value of runMoveSpeeed or walkMoveSpeed, respectively
	private void Run() {
		if (playerInputs.run == keyState.Down && !isCrouch && !inWater && grounded && !isSteep) {
			isRun = true;
		} else if (playerInputs.run == keyState.Up && !isCrouch && !inWater) {
			isRun = false;
		}
	}

	// When the crouch input is pressed down or released the camera is transformed appropriately, and isCrouch is assigned a boolean value
	private void Crouch() {
		if (playerInputs.crouch == keyState.Down && !isRun && !inWater && CrouchWaterDistance() && !isSteep){
			cameraObject.transform.Translate(Vector3.down * crouchCameraMove, Space.World);
			isCrouch = true;

		} else if (playerInputs.crouch == keyState.Up && !isRun && !inWater && isCrouch) {
			cameraObject.transform.Translate(Vector3.up * crouchCameraMove, Space.World);
			isCrouch = false;
		}
	}

	// Checks if the player is running or not, and then either returns the runMultiplier, or just 1.0f
	private float RunSpeed() {
		if (isRun && velocity.z > 0) {
			return runMultiplier;
		} else {
			return 1.0f;
		}
	}

	// Checks if the player is crouching, or underwater, and then returns the appropriate movement speed multiplier
	private float SpeedMult() {
		if (isCrouch) {
			return crouchMultiplier;
		} else if (inWater) {
			return swimMultiplier;
		} else {
			return 1.0f;
		}
	}

	// Checks if the player is underwater, and was previously not in the water, and then puts the player in swimming mode
	private void Swim() {
		if (IsUnderwater(cameraObject, water) && !inWater) {
			if (isCrouch) cameraObject.transform.Translate(Vector3.up * crouchCameraMove);
			isRun = false;
			isCrouch = false;
			inWater = true;
			// charGravity = waterGravity; <- No Gravity underwater?
			Debug.Log("Underwater");
		} else if (!IsUnderwater(cameraObject, water) && inWater) {
			inWater = false;
			// charGravity = defaultGravity; <- No Gravity underwater?
			Debug.Log("Above Water");
		}
	}

	// Checks if the difference between the camera position and water position is greater than the crouch movement, and thus prevents crouching below the water surface
	private bool CrouchWaterDistance() {
		return cameraObject.transform.position.y - water.transform.position.y - 0.5 > crouchCameraMove;
	}
}