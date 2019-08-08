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
	[SerializeField] private float walkMoveSpeed;
	[SerializeField] private float swimMoveSpeed;
	[SerializeField] private float runMoveSpeed;
	[SerializeField] private float crouchMoveSpeed;
	[SerializeField] private float crouchCameraMove;
	private float moveSpeed;
	private bool isRun;
	private bool isCrouch;
	private bool inWater;

	[Header("Jump Options")]
	[SerializeField] private float jumpHeight;
	[SerializeField] private float jumpTime;
	private float jumpCount;
	private float remainingJumpHeight;
	private bool isJump;

	[Header("Gravity")]
	[SerializeField] private float gravity;
	[SerializeField] private float terminalVelocity;
	[SerializeField] private float gravityMultiplier;
	private float currentGravity;

 	[Header("Physics")]
 	[SerializeField] private LayerMask discludePlayer;

 	[Header("References")]
 	[SerializeField] private SphereCollider sphereCol;

	// Private Variables
	private Vector3 velocity;
	private Vector3 move;
	private Vector3 vel;

	// Grounded Private Variables
	private bool grounded;
	private Vector3 liftPoint = new Vector3 (0, 1.2f, 0);
	private RaycastHit groundHit;
	private Vector3 groundCheckPoint = new Vector3 (0, -0.87f, 0);

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
		moveSpeed = walkMoveSpeed;
		coll = GetComponent<Collider> ();
		isJump = false;
	}

	private void Update() {
		CameraRotation();
		SimpleMove();
		grounded = Grounded();
		Gravity();
		Swim();
		Jump();
		Run();
		Crouch();
		Interact();
		FinalMove();
		StickToGround();
		CollisionCheck();
	}

	// Function that locks the cursor
	private void LockCursor() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

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

	private float FloatFloor(float number, float decimalPlaces) {
		float output = number * Mathf.Pow(10f, decimalPlaces);
		output = Mathf.Floor(output);
		output = output / Mathf.Pow(10f, decimalPlaces);
		return output;
	}

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

	private void SimpleMove() {
		move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
		velocity += move;

	}

	private void FinalMove() {
		Vector3 vel = new Vector3(velocity.x, velocity.y, velocity.z) * moveSpeed;
		vel = transform.TransformDirection(vel);
		transform.position += vel * Time.deltaTime;

		velocity = Vector3.zero;
	}

	private void Gravity() {
		if (!grounded && !isJump) {
			currentGravity += gravity * Time.deltaTime;

			if(currentGravity < terminalVelocity) {
				velocity.y -= currentGravity * Time.deltaTime * gravityMultiplier;
			} else if(currentGravity > terminalVelocity) {
				currentGravity = terminalVelocity;
				velocity.y -= currentGravity * Time.deltaTime * gravityMultiplier;
			}

		} else if(grounded) {
			currentGravity = 0;
		}
	}

	private bool Grounded() {
		return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);
	}

	private void StickToGround() {
		RaycastHit hit;
		Ray downRay = new Ray(transform.position, Vector3.down);

		if(Physics.Raycast(downRay, out hit)) {
			if(hit.distance > 0.9f && hit.distance < 1.1f && !isJump) {
				transform.position = new Vector3 (transform.position.x, transform.position.y + (1 - hit.distance), transform.position.z);
			}
		}

	}

	private void CollisionCheck() {
		Collider[] overlaps = new Collider[4];
		int num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(sphereCol.center), sphereCol.radius, overlaps, discludePlayer, QueryTriggerInteraction.UseGlobal);

		for (int i = 0; i < num; i++) {

			Transform t = overlaps [i].transform;
			Vector3 dir;
			float dist;

			if(Physics.ComputePenetration(sphereCol,transform.position, transform.rotation, overlaps[i], t.position, t.rotation, out dir, out dist)) {
				Vector3 penetrationVector = dir * dist;
				Vector3 velocityProjected = Vector3.Project(velocity, -dir);
				transform.position = transform.position + penetrationVector;
				vel -= velocityProjected;
			}
		}
	}

	private void Jump() { // Currently does not stop gravity so needs a rework
		if(Input.GetButtonDown(jumpInput) && grounded && !isJump && !isCrouch){
			isJump = true;
			jumpCount = jumpTime + 1f;
			remainingJumpHeight = jumpHeight;
			JumpEvent();
		} else if(isJump) {
			JumpEvent();
		}
	}

	private void JumpEvent() {
		if(jumpCount > 0) {
			jumpCount -= 1.0f;
			velocity.y += remainingJumpHeight / jumpTime;
			remainingJumpHeight -= remainingJumpHeight / jumpTime;
			Debug.Log(remainingJumpHeight);
		} else if(jumpCount == 0) {
			isJump = false;
			Debug.Log("Jump Finished");
		}
	}

	private void Run() {
		if(Input.GetButtonDown(runInput) && !isCrouch && !inWater) {
			moveSpeed = runMoveSpeed;
			isRun = true;
		} else if(Input.GetButtonUp(runInput) && !isCrouch && !inWater) {
			moveSpeed = walkMoveSpeed;
			isRun = false;
		}
	}

	private void Crouch() {
		if(Input.GetButtonDown(crouchInput) && !isRun && !inWater && CrouchWaterDistance()){
			cameraObject.transform.Translate(Vector3.down * crouchCameraMove);
			moveSpeed = crouchMoveSpeed;
			isCrouch = true;

		} else if(Input.GetButtonUp(crouchInput) && !isRun && !inWater && isCrouch) {
			cameraObject.transform.Translate(Vector3.up * crouchCameraMove);
			moveSpeed = walkMoveSpeed;
			isCrouch = false;
		}
	}

	private void Swim() {
		if(IsUnderwater() && !inWater) {
			if(isCrouch) cameraObject.transform.Translate(Vector3.up * crouchCameraMove);
			moveSpeed = swimMoveSpeed;
			isRun = false;
			isCrouch = false;
			inWater = true;
			// charGravity = waterGravity; <- No Gravity underwater?
			Debug.Log("Underwater");
		} else if(!IsUnderwater() && inWater) {
			moveSpeed = walkMoveSpeed;
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

	private void Interact() {
		if(Input.GetButtonDown(interactInput)) {
			Debug.Log("Interact button pressed");
		}
	}
}