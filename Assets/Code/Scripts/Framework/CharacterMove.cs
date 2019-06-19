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
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float crouchCameraMove;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    private float movementSpeed;
 	private bool isJumping;

    [Header("GameObject")]
    [SerializeField] private GameObject camera;
    private CharacterController charController;

    [Header("Physics")]
    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    //
    private void Awake() {
    	charController = GetComponent<CharacterController>();
    	movementSpeed = walkSpeed;
    }

    //
    private void Update() {
    	PlayerMovement();
    }

    //
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

    	Run();
    	Crouch();
    	JumpInput();
    }

    //
    private void JumpInput() {
    	if(Input.GetButtonDown(jumpInputName) && !isJumping) {
    		isJumping = true;
    		StartCoroutine(JumpEvent());
    	}
    }

    //
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

    //
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

    //
    private void Run() {
    	if(Input.GetButtonDown(runInputName)) {
    		movementSpeed = runSpeed;
    	} else if(Input.GetButtonUp(runInputName)) {
    		movementSpeed = walkSpeed;
    	}
    }

    //
    private void Crouch() {
    	if(Input.GetButtonDown(crouchInputName)) {
    		camera.transform.Translate(Vector3.down * crouchCameraMove);
    		movementSpeed = crouchSpeed;

    	} else if(Input.GetButtonUp(crouchInputName)) {
    		camera.transform.Translate(Vector3.up * crouchCameraMove);
    		movementSpeed = walkSpeed;
    	}
    }
}