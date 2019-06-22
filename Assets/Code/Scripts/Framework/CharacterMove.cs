using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {
	[Header("Input Names")]
	[SerializeField] private string horizontalInputName;
	[SerializeField] private string verticalInputName;
	[SerializeField] private string runInputName;
	[SerializeField] private string crouchInputName;
	[SerializeField] private string jumpInputName;

	[Header("Movement")]
	[SerializeField] private float walkSpeed;
	[SerializeField] private float runSpeed;
	[SerializeField] private float swimSpeed;
	[SerializeField] private float crouchSpeed;
	[SerializeField] private float crouchCameraMove;
	[SerializeField] private AnimationCurve jumpFallOff;
	[SerializeField] private float jumpMultiplier;
	private float movementSpeed;
	private bool isJumping;
	private bool isCrouch;
	private bool isRun;

	[Header("GameObject")]
	[SerializeField] private GameObject camera;
	[SerializeField] private GameObject water;
	private CharacterController charController;

	[Header("Physics")] // Two varialbes, ray length is the length of the ray shooting down to detect floor, slopForce is the downwards force applied to remove jitters
	[SerializeField] private float slopeForce;
	[SerializeField] private float slopeForceRayLength;
	private bool inWater;

	// Called once after objects are initialized, used to initialize variables and get the Character Controller Component
	private void Awake() {
		charController = GetComponent<CharacterController>();
		movementSpeed = walkSpeed;
		inWater = false;
	}

	// Called once per frame and calls the PlayerMovement function, which controls all player movement
	private void Update() {
		PlayerMovement();
	}

	// Called once per frame, first 5 lines deal with the keyboard movement, then stops slope jittering, then calls other functions
	private void PlayerMovement() {
		float vertInput = Input.GetAxis(verticalInputName);
		float horizInput = Input.GetAxis(horizontalInputName);

		Vector3 forwardMovement = transform.forward * vertInput;
		Vector3 rightMovement = transform.right * horizInput;

		// Note: SimpleMove applies Time.deltaTime automatically so no need to do so above
		charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

		if((vertInput != 0 || horizInput != 0) && OnSlope()) {
			charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);
		}

		Swim();
		Run();
		Crouch();
		JumpInput();
		Debug.Log(movementSpeed);
	}

	// Checks to make sure player has pressed the jump key, and also is not already jumping
	private void JumpInput() {
		if(Input.GetButtonDown(jumpInputName) && !isJumping) {
			isJumping = true;
			StartCoroutine(JumpEvent());
		}
	}

	// Called when player has jumped and isnt already jumping, and executes the jump
	private IEnumerator JumpEvent() {
		charController.slopeLimit = 90.0f;
		float timeInAir = 0.0f;

		do {
			float jumpForce = jumpFallOff.Evaluate(timeInAir);
			charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
			timeInAir += Time.deltaTime;
			yield return null;
		} while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

		charController.slopeLimit = 45.0f;
		isJumping = false;
	}

	// Raycast used to detect the angle of the floor below the player, and if the player is on a slope applies a downwards force to stop jitters while walking down
	private bool OnSlope() {
		if (isJumping) {
			return false;
		}
		RaycastHit hit;

		if(Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeForceRayLength)) {
			if(hit.normal != Vector3.up) {
				return true;
			}
		}
		
		return false;
	}

	// Called when the player presses the run key, and increases the players movement while the key is held down
	private void Run() {
		if(Input.GetButtonDown(runInputName) && !isCrouch && !IsUnderwater()) {
			movementSpeed = runSpeed;
			isRun = true;
		} else if(Input.GetButtonUp(runInputName) && !isCrouch && !IsUnderwater()) {
			movementSpeed = walkSpeed;
			isRun = false;
		}
	}

	// Called when the player presses the crouch key, and lowers the camera while the key is held down
	private void Crouch() {
		if(Input.GetButtonDown(crouchInputName) && !isRun && !IsUnderwater() && CrouchWaterDistance()) {
			camera.transform.Translate(Vector3.down * crouchCameraMove);
			movementSpeed = crouchSpeed;
			isCrouch = true;

		} else if(Input.GetButtonUp(crouchInputName) && !isRun && !IsUnderwater() && isCrouch) {
			camera.transform.Translate(Vector3.up * crouchCameraMove);
			movementSpeed = walkSpeed;
			isCrouch = false;
		}
	}

	//
	private void Swim() {
		if(IsUnderwater() && !inWater) {
			if(isCrouch) camera.transform.Translate(Vector3.up * crouchCameraMove);
			movementSpeed = swimSpeed;
			isRun = false;
			isCrouch = false;
			inWater = true;
			Debug.Log("Underwater");
		} else if(!IsUnderwater() && inWater) {
			movementSpeed = walkSpeed;
			inWater = false;
			Debug.Log("Above Water");
		}
		
	}

	// Checks if the player camera is below the y position of the water surface plane
	private bool IsUnderwater() {
		return camera.transform.position.y < water.transform.position.y;
	}

	// Checks if the difference between the camera position and water position is greater than the crouch movement, and thus prevents crouching below the water surface
	private bool CrouchWaterDistance() {
		return camera.transform.position.y - water.transform.position.y > crouchCameraMove;
	}
}