using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {
    
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    private bool isJumping;

    private void Awake() {
    	charController = GetComponent<CharacterController>();
    }

    private void Update() {
    	PlayerMovement();
    }

    private void PlayerMovement() {
    	float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;
    	float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;

    	Vector3 forwardMovement = transform.forward * vertInput;
    	Vector3 rightMovement = transform.right * horizInput;

    	// Note: SimpleMove applies Time.deltaTime automatically so no need to do so above
    	charController.SimpleMove(forwardMovement + rightMovement);
    }

    private void JumpInput() {

    }

    private IEnumerator JumpEvent() {
    	yield return null;
    }
}
