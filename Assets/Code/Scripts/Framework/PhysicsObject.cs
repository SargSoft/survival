using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

	private float gravity = -9.81f;
	private float terminalVelocity = -50f;
	private float scaleCompensationConstant = 0.5f; // Compensating for scale to make it feel accurate
	
	// Adds the new force to the Velocity and returns a Vector3
	public Vector3 AddVelocity(Vector3 velocity, Vector3 force) {
		return velocity + force;
	}

	// Returns the new force as a Vector3 (used when setting the value of Velocity)
	public Vector3 SetVelocity(Vector3 velocity, Vector3 force) {
		return force;
	}

	// Returns a Vector3 that is 0,0,0 (Used to reset the value of Velocity)
	public Vector3 ResetVelocity(Vector3 velocity) {
		return Vector3.zero;
	}

	// Uses a Raycast to check if the object is in contact with the ground and returns a boolean value
	public bool Grounded(Vector3 origin, Collider objectCollider) {
		return Physics.Raycast(origin, Vector3.down, objectCollider.bounds.extents.y + 0.1f);
	}

	// Returns a float that is the velocity.y of the object
	public float Gravity(Vector3 velocity, bool grounded, bool isJump) {
		if (!grounded && !isJump) {
			float airResistance = AirResisitance(velocity.y);
			float proposedGravity = velocity.y + (gravity * Time.fixedDeltaTime * airResistance * scaleCompensationConstant);
			return proposedGravity;
		} else {
			return 0f;
		}
	}

	// Calculates the air resistance for the Gravity method
	public float AirResisitance(float currentVelocity) {
		float output = -Mathf.Pow(currentVelocity / (terminalVelocity * scaleCompensationConstant), 2f) + 1;
		return output;
	}

	// Takes a float and floors it to a specified number of decimal places
	public float FloatFloor(float number, float decimalPlaces) {
		float output = number * Mathf.Pow(10f, decimalPlaces);
		output = Mathf.Floor(output);
		output = output / Mathf.Pow(10f, decimalPlaces);
		return output;
	}

	// Checks if the player camera is below the y position of the water surface plane
	public bool IsUnderwater(Transform objectPosition , GameObject water) {
		return objectPosition.transform.position.y < water.transform.position.y;
	}
}