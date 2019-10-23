using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

	private float gravity = -9.81f;
	private float terminalVelocity = -50f;
	private float scaleCompensationConstant = 0.5f; // Compensating for scale to make it feel accurate
	private float capsuleSizeCompensation = 0.925f; // Compensates for the face the collider is smaller to fix sliding on slopes issues
	
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
		return Physics.Raycast(origin, Vector3.down, objectCollider.bounds.extents.y + 0.25f);
	}

	// Checks the angle of the ground below the players feet, and if its greater than the max angle prevents the player from moving
	protected float SteepCheck(Transform objectPosition) {
		float output = 0f;
		RaycastHit hit;
		Ray downRay = new Ray((objectPosition.transform.position + Vector3.up), Vector3.down);

		if (Physics.Raycast(downRay, out hit)) {
			
			output = Vector3.Angle(hit.normal, Vector3.up);
		}

		return output;
	}

	// Returns boolean value depending whether SteepCheck returns a value greater than maxSlope
	protected bool IsSteep(float steepness, float maxSlope) {
		if (steepness > maxSlope) {
			return true;
		} else {
			return false;
		}
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
		Ray downRay = new Ray((origin + (Vector3.up * capsuleSizeCompensation)), Vector3.down);
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

	protected Vector3 SlideOnSlope(Vector3 origin, float maxAngle, bool grounded, float slideMultiplier) {
		RaycastHit hit;
		Ray downRay = new Ray((origin + (Vector3.up * capsuleSizeCompensation)), Vector3.down);
		Vector3 output = origin;

		if (Physics.Raycast(downRay, out hit)) {
			// Uses RaycastHit to determine if the angle of the floor is greater than the maxAngle, and if so slides the player down the hill
			if (FloatFloor(Vector3.Angle(hit.normal, Vector3.up), 2f) >= maxAngle && grounded) {
				Vector3 slideTemp = Vector3.Cross(hit.normal, Vector3.up);
				output += -Vector3.Cross(slideTemp, hit.normal) * slideMultiplier;

			}
		}

		return output;
	}

	// Checks for object collisions using a shperecast, computes the penetration, and then pushes the player back
	protected Vector3 Collision(LayerMask disclude, CapsuleCollider capsuleCol, Transform objectPosition, float steepness) {
		Collider[] overlaps = new Collider[4];
		Vector3 output = objectPosition.transform.position;
		// Calculating top and bottom of capsule
		Vector3 capsuleCenter = transform.TransformPoint(capsuleCol.center);
		Vector3 top = capsuleCenter + (Vector3.up * capsuleSizeCompensation);
		Vector3 bottom = capsuleCenter - (Vector3.up * capsuleSizeCompensation);
		int num = Physics.OverlapCapsuleNonAlloc(top, bottom, capsuleCol.radius, overlaps, disclude, QueryTriggerInteraction.UseGlobal);

		for (int i = 0; i < num; i++) {

			Transform t = overlaps [i].transform;
			Vector3 dir;
			float dist;

			if (Physics.ComputePenetration(capsuleCol, objectPosition.transform.position, objectPosition.transform.rotation, overlaps[i], t.position, t.rotation, out dir, out dist)) {
				Vector3 penetrationVector = dir * dist;
				output += penetrationVector;
			}
		}

		return output;
	}

	//
	protected Vector3 SlopeLimit(Vector3 initialPosition, Transform objectPosition, CapsuleCollider capsuleCol) {
		RaycastHit hit;
		Ray downRay = new Ray((objectPosition.transform.position + (Vector3.up * capsuleSizeCompensation)), Vector3.down);
		Vector3 output = objectPosition.transform.position;
		Vector3 capsuleCenter = transform.TransformPoint(capsuleCol.center);
		Vector3 top = capsuleCenter + (Vector3.up * capsuleSizeCompensation);
		Vector3 bottom = capsuleCenter - (Vector3.up * capsuleSizeCompensation);

		if (Physics.Raycast(downRay, out hit)) {
			
			Vector3 n = hit.normal;
			float a = Vector3.Angle(n, Vector3.up);

			Vector3 absoluteMoveDirection = Math3d.ProjectVectorOnPlane(n, objectPosition.transform.position - initialPosition);

			// Retrieve a vector pointing down the slope
			Vector3 r = Vector3.Cross(n, Vector3.down);
			Vector3 v = Vector3.Cross(r, n);

			float angle = Vector3.Angle(absoluteMoveDirection, v);

			// Calculate where to place the controller on the slope, or at the bottom, based on the desired movement distance
			Vector3 resolvedPosition = Math3d.ProjectPointOnLine(initialPosition, r, objectPosition.transform.position);
			Vector3 direction = Math3d.ProjectVectorOnPlane(n, resolvedPosition - objectPosition.transform.position);

			RaycastHit hit2;

			// Check if our path to our resolved position is blocked by any colliders
			if (Physics.CapsuleCast(top, bottom, capsuleCol.radius, direction.normalized, out hit2, direction.magnitude)) {
				output += v.normalized * hit2.distance;
			} else {
				output += direction;
			}
		}

		return output;

	}
}