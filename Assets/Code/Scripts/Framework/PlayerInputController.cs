using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : PhysicsObject {
	
	[Header("Input Manager")]
	private string mouseXInput = "Mouse X";
	private string mouseYInput = "Mouse Y";
	private string jumpInput = "Jump";
	private string runInput = "Run";
	private string crouchInput = "Crouch";
	private string interactInput = "Interact";

	private float currentMouseX;
	private float currentMouseY;
	private Vector3 currentMove;
	private keyState currentJump;
	private keyState currentRun;
	private keyState currentCrouch;
	private keyState currentInteract;
	private PlayerInputs currentPlayerInputs;

	protected enum keyState {Up, Down, Null};

	protected struct PlayerInputs {
		public float mouseX;
		public float mouseY;
		public Vector3 move;
		public keyState jump;
		public keyState run;
		public keyState crouch;
		public keyState interact;
	}

	protected PlayerInputs ReturnPlayerInputs() {

		currentMouseX = Input.GetAxis(mouseXInput);

		currentMouseY = Input.GetAxis(mouseYInput);

		currentMove = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

		if (Input.GetButtonDown(jumpInput)) {
			currentJump = keyState.Down;
		} else if (Input.GetButtonUp(jumpInput)) {
			currentJump = keyState.Up;
		} else {
			currentJump = keyState.Null;
		}

		if (Input.GetButtonDown(runInput)) {
			currentRun = keyState.Down;
		} else if (Input.GetButtonUp(runInput)) {
			currentRun = keyState.Up;
		} else {
			currentRun = keyState.Null;
		}

		if (Input.GetButtonDown(crouchInput)) {
			currentCrouch = keyState.Down;
		} else if (Input.GetButtonUp(crouchInput)) {
			currentCrouch = keyState.Up;
		} else {
			currentCrouch = keyState.Null;
		}

		if (Input.GetButtonDown(interactInput)) {
			currentInteract = keyState.Down;
		} else if (Input.GetButtonUp(interactInput)) {
			currentInteract = keyState.Up;
		} else {
			currentInteract = keyState.Null;
		}

		currentPlayerInputs = new PlayerInputs() {
			mouseX = currentMouseX,
			mouseY = currentMouseY,
			move = currentMove,
			jump = currentJump,
			run = currentRun,
			crouch = currentCrouch,
			interact = currentInteract		
		};

		return currentPlayerInputs;
	}
}
