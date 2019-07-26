using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
	[Header("Mouse Settings")]
	[SerializeField] private string mouseXInputName;
	[SerializeField] private string mouseYInputName;
	[SerializeField] private float mouseYSensitivity;
	[SerializeField] private float mouseXSensitivity;
	private float mouseYAxisMin = -90f;
	private float mouseYAxisMax = 90f;

	[Header("Game Object")]
	[SerializeField] private Transform playerBody;

	private float mouseX;
	private float mouseY;

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

	private void CameraRotation() {
		mouseX += Input.GetAxis (mouseXInputName) * mouseXSensitivity;
		mouseY += Input.GetAxis (mouseYInputName) * mouseYSensitivity;
		mouseY = Mathf.Clamp (mouseY, mouseYAxisMin, mouseYAxisMax);

		transform.eulerAngles = (Vector3.left * mouseY);
		playerBody.eulerAngles = Vector3.up * mouseX;

		// For Swimming:
		// transform.eulerAngles = (Vector3.left * mouseY) + (Vector3.up * mouseX);

	}

}
