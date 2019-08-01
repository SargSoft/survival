using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
	[Header("Mouse Settings")]
	[SerializeField] private string mouseXInputName;
	[SerializeField] private string mouseYInputName;
	[SerializeField] private float mouseYSensitivity;
	[SerializeField] private float mouseXSensitivity;

	[Header("Game Object")]
	[SerializeField] private Transform playerBody;
	

	// Initial function that runs when the scene is played, lockks the cursor and rests xAxisClamp
	private void Awake() {
		LockCursor();
		// xAxisClamp = 0.0f;
	}

	private void LateUpdate() {
		CameraRotation();
	}

	// Function that locks the cursor
	private void LockCursor() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// private float ClampXRotation(float current, float rotation) {
	// 	if(current + rotation <)
	// }


	private void CameraRotation() {
		float mouseX =+ Input.GetAxis(mouseXInputName) * mouseXSensitivity;
		float mouseY =+ Input.GetAxis(mouseYInputName) * mouseYSensitivity;

		// mouseY = ClampXRotation(transform.eulerAngles.x, mouseY);
		Debug.Log(transform.eulerAngles.x);

		Quaternion yRot = playerBody.localRotation * Quaternion.Euler(0f, mouseX, 0f);
		Quaternion xRot = transform.localRotation * Quaternion.Euler(-mouseY, 0f, 0f);

		transform.localRotation = xRot;
		playerBody.localRotation = yRot;

		// For Swimming:
		// transform.eulerAngles = (Vector3.left * mouseY) + (Vector3.up * mouseX);
	}

}