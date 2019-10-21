using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

	private float gravity = -9.81f;
	private float terminalVelocity = -50f;
	private float scaleCompensationConstant = 0.5f; // Compensating for scale to make it feel accurate
	
	// Adds the new force to the Velocity and returns a Vector3
	protected Vector3 AddVelocity(Vector3 velocity, Vector3 force) {
		return velocity + force;
	}

	// Returns the new force as a Vector3 (used when setting the value of Velocity)
	protected Vector3 SetVelocity(Vector3 velocity, Vector3 force) {
		return force;
	}

	// Adds the force to the velocity and returns a float (used for single axis)
	protected float AddSingleVelocity(float velocity, float force) {
		return velocity + force;
	}

	// Returns the new force as a float (used when setting the value of Velocity along a single axis)
	protected float SetSingleVelocity(float velocity, float force) {
		return force;
	}

	// Returns a Vector3 that is 0,0,0 (Used to reset the value of Velocity)
	protected Vector3 ResetVelocity(Vector3 velocity) {
		return Vector3.zero;
	}

	// Uses a Raycast to check if the object is in contact with the ground and returns a boolean value
	protected bool Grounded(Vector3 origin, Collider objectCollider) {
		return Physics.Raycast(origin, Vector3.down, objectCollider.bounds.extents.y + 0.1f);
	}

	// Checks the angle of the ground below the players feet, and if its greater than the max angle prevents the player from moving
	private void SteepCheck(float maxAngle) {
		// RaycastHit hit;
		// Ray downRay = new Ray((transform.position + Vector3.up), Vector3.down);

		// if (Physics.Raycast(downRay, out hit)) {
			
		// 	if (FloatFloor(Vector3.Angle(hit.normal, Vector3.up), 2f) >= maxAngle && grounded) {
		// 		velocity = ResetVelocity(velocity);
		// 	}
		// }
	}

	// Returns a float that is the velocity.y of the object
	protected float Gravity(Vector3 velocity, bool grounded, bool isJump) {
		if (!grounded && !isJump) {
			float airResistance = AirResisitance(velocity.y);
			float proposedGravity = velocity.y + (gravity * Time.fixedDeltaTime * airResistance * scaleCompensationConstant);
			return proposedGravity;
		} else {
			return 0f;
		}
	}

	// Calculates the air resistance for the Gravity method
	protected float AirResisitance(float currentVelocity) {
		float output = -Mathf.Pow(currentVelocity / (terminalVelocity * scaleCompensationConstant), 2f) + 1;
		return output;
	}

	// Takes a float and floors it to a specified number of decimal places
	protected float FloatFloor(float number, float decimalPlaces) {
		float output = number * Mathf.Pow(10f, decimalPlaces);
		output = Mathf.Floor(output);
		output = output / Mathf.Pow(10f, decimalPlaces);
		return output;
	}

	// Prevents the rotation exceeding the bounds, in this case 80 degrees up and down
	protected float ClampXRotation(float current, float rotation) {
		if (current - rotation > 80f && current - rotation < 280f) {
			if (rotation < 0f) {
				return FloatFloor(current, 4) - 80f + 0.001f;
			} else if (rotation > 0f) {
				return FloatFloor(current, 4) - 280f - 0.001f;
			} else {
				Debug.Log("Error: Outside bounds without mouse movement");
				return 0f;
			}
		} else {
			return rotation;
		}
	}

	// Checks if the player camera is below the y position of the water surface plane
	protected bool IsUnderwater(Transform objectPosition , GameObject water) {
		return objectPosition.transform.position.y < water.transform.position.y;
	}

	// Uses a Raycast to adjust the players height to make it stick to the ground (when going up and down slopes especially), and also makes the player slide down slopes over a certain angle
	protected Vector3 StickToGround(Vector3 origin, bool isJump, float downVel) {
		RaycastHit hit;
		Ray downRay = new Ray((origin + Vector3.up), Vector3.down);
		Vector3 output = origin;
		float downwardsVelocityCompensation = 1.7f + (downVel * 3.4f * Time.fixedDeltaTime);

		if (Physics.Raycast(downRay, out hit)) {
			// Checks if the player is within 2.1f of the top of the player, and if so transforms the players position so they on the ground surface
			if (hit.distance >= downwardsVelocityCompensation && hit.distance <= 2.1f && !isJump) {
				output = new Vector3(origin.x, origin.y + (2.0f - hit.distance), origin.z);
			}
		}

		return output;
	}

	// protected void SlideOnSlope(Vector3 origin, float maxAngle) {
	// 	RaycastHit hit;
	// 	Ray downRay = new Ray((origin + Vector3.up), Vector3.down);
	// 	Vector3 slide = new Vector3(0, 0, 0);

	// 	if (Physics.Raycast(downRay, out hit)) {
	// 		// Uses RaycastHit to determine if the angle of the floor is greater than the maxAngle, and if so slides the player down the hill
	// 		if (FloatFloor(Vector3.Angle(hit.normal, Vector3.up), 2f) >= maxAngle && grounded) {
	// 			// Vector3 slideTemp = Vector3.Cross(hit.normal, Vector3.up);
	// 			// slide += -Vector3.Cross(slideTemp, hit.normal);

	// 			CollisionCheckRename(discludePlayer);
	// 		}

	// 		// transform.position += slide * slideMultiplier;
	// 	}
	// }

	// Checks for object collisions using a shperecast, computes the penetration, and then pushes the player back
	protected void CollisionCheckRename(LayerMask disclude) {
		// Collider[] overlaps = new Collider[4];
		// // Calculating top and bottom of capsule
		// Vector3 capsuleCenter = transform.TransformPoint(capsuleCol.center);
		// Vector3 top = capsuleCenter + Vector3.up;
		// Vector3 bottom = capsuleCenter - Vector3.up;
		// int num = Physics.OverlapCapsuleNonAlloc(top, bottom, capsuleCol.radius, overlaps, disclude, QueryTriggerInteraction.UseGlobal);

		// for (int i = 0; i < num; i++) {

		// 	Transform t = overlaps [i].transform;
		// 	Vector3 dir;
		// 	float dist;

		// 	if (Physics.ComputePenetration(capsuleCol, transform.position, transform.rotation, overlaps[i], t.position, t.rotation, out dir, out dist)) {
		// 		Vector3 penetrationVector = dir * dist;
		// 		transform.position = transform.position + penetrationVector;
		// 	}
		// }
	}
}