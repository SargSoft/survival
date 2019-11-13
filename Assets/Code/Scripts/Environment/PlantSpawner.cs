using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour {

	[SerializeField] private enum GizmoType { Never, SelectedOnly, Always }
	[SerializeField] private GameObject prefab;
	[SerializeField] private LayerMask discludeLayer;
	[SerializeField] private float spawnRadius = 10.0f;
	[SerializeField] private float radiusAroundObject = 5.0f;
	[SerializeField] private int spawnCount = 10;
	[SerializeField] private int attemptsBeforeRejection = 30;
	[SerializeField] private GizmoType showSpawnRegion;
	
	private Vector3[] plantPositions;
	private SphereCollider col;
	private float colDiameter;
	private float objectSeparation;

	void Awake() {
		col = prefab.GetComponentInChildren<SphereCollider>();
		colDiameter = (col.radius * 2.0f); // Implement a way that the prefabs themself contain this information so it can take different colliders
		objectSeparation = colDiameter + radiusAroundObject;
		plantPositions = GeneratePositions(spawnRadius, objectSeparation, spawnCount, attemptsBeforeRejection);

		for (int i = 0; i < spawnCount; i++) {
			Vector3 pos = plantPositions[i];

			if (pos != transform.position) {
				RaycastHit hit;
				Ray downRay = new Ray(pos, Vector3.down);

				if (Physics.Raycast(downRay, out hit)) {
					pos += Vector3.down * hit.distance;
				}

				plantPositions[i] = pos;
				Object.Instantiate(prefab, pos, Quaternion.identity, transform);
			}
		}
	}

	private Vector3[] GeneratePositions(float spawnRadius, float objectSeparation, int spawnCount, int attemptsBeforeRejection) {
		Vector3[] outputPositions = new Vector3[spawnCount];
		Vector2[] proposedPositions = new Vector2[spawnCount];
		float sqrObjectSeparation = objectSeparation * objectSeparation;

		for (int i = 0; i < spawnCount; i++) {
			int attemptsLeft = attemptsBeforeRejection;
			
			while (attemptsLeft > 0) {
				bool viablePosition = true;
				Vector2 tempPosA = Random.insideUnitCircle * spawnRadius;
				
				for (int indexB = 0; indexB < i; indexB++) {
					Vector2 tempPosB = proposedPositions[indexB];
					Vector2 tempVector = new Vector2(tempPosA.x - tempPosB.x, tempPosA.y - tempPosB.y);
					float sqrVectorDistance = Vector2.SqrMagnitude(tempVector);
			
					if (sqrVectorDistance < sqrObjectSeparation) {
						viablePosition = false;
						attemptsLeft -= 1;
						break;
					}
				}

				if (viablePosition) {
					proposedPositions[i] = tempPosA;
					break;
				}
			}
		}

		//converts Vector2's to desired Vector3's
		for (int i = 0; i < spawnCount; i++) {
			Vector2 tempPos = proposedPositions[i];
			outputPositions[i] = transform.position + (new Vector3(tempPos.x ,0 , tempPos.y));
		}

		return outputPositions;
	}
	private void OnDrawGizmos() {
		if (showSpawnRegion == GizmoType.Always) {
			DrawGizmos();
		}
	}

	private void OnDrawGizmosSelected() {
		if (showSpawnRegion == GizmoType.SelectedOnly) {
			DrawGizmos();
		}
	}

	private void DrawGizmos() {
		Gizmos.color = new Color(1.0f, 0, 0, 0.5f);
		Gizmos.DrawSphere(transform.position, spawnRadius);
	}

}
