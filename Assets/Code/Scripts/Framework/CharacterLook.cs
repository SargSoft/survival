using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLook : MonoBehaviour {
	[Header("Mouse Settings")]
	[SerializeField] private string mouseXInputName;
	[SerializeField] private string mouseYInputName;
	[SerializeField] private float mouseSensitivity;

	[SerializeField] private Transform playerBody;

	private float xAxisClamp;
	private static bool inWater;

	// Initial function that runs when the scene is played, lockks the cursor and rests xAxisClamp
	private void Awake() {
		LockCursor();
		xAxisClamp = 0.0f;
	}

	// Called every frame, and runs the CameraRotation function
	private void Update() {
		inWater = CharacterMove.inWater;
		Debug.Log("Static Variable: " + inWater);
		// CameraRotation();
	}

	// Function that locks the cursor
	private void LockCursor() {
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Function that gets the mouse movement, ensures it would not move past the clamps, then transforms accordingly
	private void CameraRotation() {
		float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

		xAxisClamp += mouseY;

		if(xAxisClamp > 90.0f) {
			xAxisClamp = 90.0f;
			mouseY = 0.0f;
			ClampXAxisRotationToValue(270.0f);
		} else if(xAxisClamp < -90.0f) {
			xAxisClamp = -90.0f;
			mouseY = 0.0f;
			ClampXAxisRotationToValue(90.0f);
		}
		// if(!inWater) {

		// 	} else if(inWater) {
				
		// 	}
		transform.Rotate(Vector3.left * mouseY);
		playerBody.Rotate(Vector3.up * mouseX);
	}

	// Making sure the Vector3 and eulerAngles play nice
	private void ClampXAxisRotationToValue(float value) {
		Vector3 eulerRotation = transform.eulerAngles;
		eulerRotation.x = value;
		transform.eulerAngles = eulerRotation;
	}
}
