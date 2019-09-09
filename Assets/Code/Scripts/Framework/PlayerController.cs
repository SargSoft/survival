using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

	[Header("Input Manager")]
	[SerializeField] private string mouseXInput;
	[SerializeField] private string mouseYInput;
	[SerializeField] private string jumpInput;
	[SerializeField] private string runInput;
	[SerializeField] private string crouchInput;
	[SerializeField] private string interactInput;

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

	[Header("Gravity")]
	[SerializeField] private float gravity;
	[SerializeField] private float terminalVelocity;
	[SerializeField] private float gravityMultiplier;
	private float currentGravity;

 	[Header("Physics")]
 	[SerializeField] private LayerMask discludeGround;
 	[SerializeField] private LayerMask discludeObjects;
 	[SerializeField] private float maxSlopeAngle;
 	[SerializeField] private float slideMultiplier;

 	[Header("References")]
 	[SerializeField] private CapsuleCollider capsuleCol;

	// Private Variables
	private Vector3 velocity;
	private Vector3 move;
	private Vector3 vel;

	// Grounded Private Variables
	private bool grounded;

	//Player Camera Options
	[Header("Mouse Settings")]
	[SerializeField] private float mouseYSensitivity;
	[SerializeField] private float mouseXSensitivity;

	[Header("Game Object")]
	[SerializeField] private Transform cameraObject;
	[SerializeField] private GameObject water;

	// New Variables
	private Collider coll;

	private void Awake() {
		LockCursor();
		coll = GetComponent<Collider> ();
		isJump = false;
	}

	private void Update() {
		grounded = Grounded();
		CameraRotation();
		SimpleMove();
		Gravity();
		SteepCheck(maxSlopeAngle);
		Swim();
		Jump();
		Run();
		Crouch();
		Interact();
		FinalMove();
		StickToGround(maxSlopeAngle);
		CollisionCheck(discludeGround);
	}

	// Function that locks the cursor
	private void LockCursor() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Prevents the rotation exceeding the bounds, in this case 80 degrees up and down
	private float ClampXRotation(float current, float rotation) {
		if(current - rotation > 80f && current - rotation < 280f) {
			if(rotation < 0f) {
				return FloatFloor(current, 4) - 80f + 0.001f;
			} else if(rotation > 0f) {
				return FloatFloor(current, 4) - 280f - 0.001f;
			} else {
				Debug.Log("Error: Outside bounds without mouse movement");
				return 0f;
			}
		} else {
			return rotation;
		}
	}

	// Takes a float and floors it to a specified number of decimal places
	private float FloatFloor(float number, float decimalPlaces) {
		float output = number * Mathf.Pow(10f, decimalPlaces);
		output = Mathf.Floor(output);
		output = output / Mathf.Pow(10f, decimalPlaces);
		return output;
	}

	// Takes the mouseX and mouseY, clamps the X rotation, adds the mouseX and mouseY to the quaternions yRot and xRot, and then rotates the camera and object appropriately
	private void CameraRotation() {
		float mouseX = Input.GetAxis(mouseXInput) * mouseXSensitivity;
		float mouseY = Input.GetAxis(mouseYInput) * mouseYSensitivity;

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
		move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

		if(grounded) {
			VelocityReset();
			velocity += BasicMoveVector(move);
		} else {
			velocity.y = 0f;
			velocity.z += AirMoveVector(velocity.z, move.z, airControlPercentForward);
			velocity.x += AirMoveVector(velocity.x, move.x, airControlPercentSideways);
		}
	}

	// Calculates the velocity (player movement this game tick) and transforms the player appropriately
	private void FinalMove() {
		float runSpeed = RunSpeed();
		float speedMult = SpeedMult();

		Debug.Log("Velocity: " + velocity);
		Vector3 vel = new Vector3(velocity.x * speedMult, velocity.y, velocity.z * runSpeed * speedMult) * baseMoveSpeed;
		// Debug.Log("Vel: " + vel);

		vel = transform.TransformDirection(vel);
		transform.position += vel * Time.fixedDeltaTime;
	}

	// Resets the velocity variable (used for controlling movement) to zero
	private void VelocityReset() {
		velocity = Vector3.zero;
	}

	// Returns a normalized vector3 of the horizontal and vertical movement inputs
	private Vector3 BasicMoveVector(Vector3 input) {
		return Vector3.Normalize(input);
	}

	//
	private float AirMoveVector(float current, float proposed, float AirControl) {
		return (proposed - current) * AirControl;
	}

	// Takes a float as the input, checks if it is above or below the upper or lower bounds, respectively, and then returns either the input or appropriate bound
	private float NumberLimits(float input, float upper, float lower) {
		if(input > upper) {
			return upper;
		} else if(input < lower) {
			return lower;
		} else {
			return input;
		}
	}

	// Applies a downwards accelleration if they player is in the air until they reach the terminal velocity
	private void Gravity() {
		if (!grounded && !isJump) {
			currentGravity += gravity * Time.fixedDeltaTime;

			if(currentGravity < terminalVelocity) {
				velocity.y -= currentGravity * Time.fixedDeltaTime * gravityMultiplier;
			} else if(currentGravity > terminalVelocity) {
				currentGravity = terminalVelocity;
				velocity.y -= currentGravity * Time.fixedDeltaTime * gravityMultiplier;
			}

		} else if(grounded) {
			currentGravity = 0;
		}
	}

	// Uses a Raycast to check if the player is in contact with the ground and returns a boolean value
	private bool Grounded() {
		return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);
	}

	// Uses a Raycast to adjust the players height to make it stick to the ground (when going up and down slopes especially), and also makes the player slide down slopes over a certain angle
	private void StickToGround(float maxAngle) {
		RaycastHit hit;
		Ray downRay = new Ray((transform.position + Vector3.up), Vector3.down);
		Vector3 slide = new Vector3(0, 0, 0);

		if(Physics.Raycast(downRay, out hit)) {
			// Uses RaycastHit to determine if the angle of the floor is greater than the maxAngle, and if so slides the player down the hill
			if(FloatFloor(Vector3.Angle(hit.normal, Vector3.up), 2f) >= maxAngle && grounded) {
				// Vector3 slideTemp = Vector3.Cross(hit.normal, Vector3.up);
				// slide += -Vector3.Cross(slideTemp, hit.normal);

				CollisionCheck(discludeObjects);
			}

			// transform.position += slide * slideMultiplier;

			// Checks if the player is within 2.1f of the top of the player, and if so transforms the players position so they on the ground surface
			if(hit.distance >= 0f && hit.distance <= 2.1f && !isJump) {
				transform.position = new Vector3 (transform.position.x, transform.position.y + (2.0f - hit.distance), transform.position.z);
			}
		}
	}

	// Checks the angle of the ground below the players feet, and if its greater than the max angle prevents the player from moving
	private void SteepCheck(float maxAngle) {
		RaycastHit hit;
		Ray downRay = new Ray((transform.position + Vector3.up), Vector3.down);

		if(Physics.Raycast(downRay, out hit)) {
			
			if(FloatFloor(Vector3.Angle(hit.normal, Vector3.up), 2f) >= maxAngle && grounded) {
				VelocityReset();
			}
		}
	}

	// Checks for object collisions using a shperecast, computes the penetration, and then pushes the player back
	private void CollisionCheck(LayerMask disclude) {
		Collider[] overlaps = new Collider[4];
		// Calculating top and bottom of capsule
		Vector3 capsuleCenter = transform.TransformPoint(capsuleCol.center);
		Vector3 top = capsuleCenter + Vector3.up;
		Vector3 bottom = capsuleCenter - Vector3.up;
		int num = Physics.OverlapCapsuleNonAlloc(top, bottom, capsuleCol.radius, overlaps, disclude, QueryTriggerInteraction.UseGlobal);

		for (int i = 0; i < num; i++) {

			Transform t = overlaps [i].transform;
			Vector3 dir;
			float dist;

			if(Physics.ComputePenetration(capsuleCol, transform.position, transform.rotation, overlaps[i], t.position, t.rotation, out dir, out dist)) {
				Vector3 penetrationVector = dir * dist;
				transform.position = transform.position + penetrationVector;
			}
		}
	}

	// Checks if the 
	private void Jump() {
		if(Input.GetButtonDown(jumpInput) && grounded && !isJump && !isCrouch){
			isJump = true;
			jumpCount = jumpTime + 1f;
			remainingJumpHeight = jumpHeight;
			JumpEvent();
		} else if(isJump) {
			JumpEvent();
		}
	}

	// Checks the jump count, and if its still greater than 0 it addes y velocity accordingly, decreasing as the jumpCount does until ultimaltey isJump is assigned false
	private void JumpEvent() {
		if(jumpCount > 0) {
			jumpCount -= 1.0f;
			velocity.y += remainingJumpHeight / jumpTime;
			remainingJumpHeight -= remainingJumpHeight / jumpTime;
		} else if(jumpCount == 0) {
			isJump = false;
		}
	}

	// When the run input is pressed down or released, the moveSpeed variables is assigned the value of runMoveSpeeed or walkMoveSpeed, respectively
	private void Run() {
		if(Input.GetButtonDown(runInput) && !isCrouch && !inWater) {
			isRun = true;
		} else if(Input.GetButtonUp(runInput) && !isCrouch && !inWater) {
			isRun = false;
		}
	}

	// When the crouch input is pressed down or released the camera is transformed appropriately, and isCrouch is assigned a boolean value
	private void Crouch() {
		if(Input.GetButtonDown(crouchInput) && !isRun && !inWater && CrouchWaterDistance()){
			cameraObject.transform.Translate(Vector3.down * crouchCameraMove);
			isCrouch = true;

		} else if(Input.GetButtonUp(crouchInput) && !isRun && !inWater && isCrouch) {
			cameraObject.transform.Translate(Vector3.up * crouchCameraMove);
			isCrouch = false;
		}
	}

	// Checks if the player is running or not, and then either returns the runMultiplier, or just 1.0f
	private float RunSpeed() {
		if(isRun && velocity.z > 0) {
			return runMultiplier;
		} else {
			return 1.0f;
		}
	}

	// Checks if the player is crouching, or underwater, and then returns the appropriate movement speed multiplier
	private float SpeedMult() {
		if(isCrouch) {
			return crouchMultiplier;
		} else if(inWater) {
			return swimMultiplier;
		} else {
			return 1.0f;
		}
	}

	// Checks if the player is underwater, and was previously not in the water, and then puts the player in swimming mode
	private void Swim() {
		if(IsUnderwater() && !inWater) {
			if(isCrouch) cameraObject.transform.Translate(Vector3.up * crouchCameraMove);
			isRun = false;
			isCrouch = false;
			inWater = true;
			// charGravity = waterGravity; <- No Gravity underwater?
			Debug.Log("Underwater");
		} else if(!IsUnderwater() && inWater) {
			inWater = false;
			// charGravity = defaultGravity; <- No Gravity underwater?
			Debug.Log("Above Water");
		}
	}

	// Checks if the player camera is below the y position of the water surface plane
	private bool IsUnderwater() {
		return cameraObject.transform.position.y < water.transform.position.y;
	}

	// Checks if the difference between the camera position and water position is greater than the crouch movement, and thus prevents crouching below the water surface
	private bool CrouchWaterDistance() {
		return cameraObject.transform.position.y - water.transform.position.y - 0.5 > crouchCameraMove;
	}

	// Checks if the player presses the interact button
	private void Interact() {
		if(Input.GetButtonDown(interactInput)) {
			Debug.Log("Interact button pressed");
		}
	}
}