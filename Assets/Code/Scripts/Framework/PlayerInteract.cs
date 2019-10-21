using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : PlayerInputController {

	private PlayerInputs playerInputs;

	[Header("Game Object")]
	[SerializeField] private Transform cameraObject;

	[Header("Interact Options")]
	[SerializeField] private float interactRange;
	
	private void Update() {
		playerInputs = ReturnPlayerInputs();

		Interact();
	}

	// Checks if the player presses the interact button
	private void Interact() {
		if (playerInputs.interact == keyState.Down) {
			// Debug.Log("Interact button pressed");

			RaycastHit hit;
			Ray forwardRay = new Ray(cameraObject.position, cameraObject.forward);

			if (Physics.Raycast(forwardRay, out hit, interactRange)) {
				Interactable interactable = hit.collider.GetComponent<Interactable>();
				if (interactable != null) {
					// Debug.Log("Interactable");
				}
			}
		}
	}
}