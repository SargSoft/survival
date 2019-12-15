using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	private float minFaunaSpawnDist = 1f; // Minimum distance fauna can spawn from surface and floor
	protected Vector3[] GeneratePositions(Vector3 inputPosition, float spawnRadius, float objectSeparation, int spawnCount, int attemptsBeforeRejection, System.Func<Vector2> randomNumberMethod) {
		Vector3[] outputPositions = new Vector3[spawnCount];
		Vector2[] proposedPositions = new Vector2[spawnCount];
		float sqrObjectSeparation = objectSeparation * objectSeparation;

		for (int i = 0; i < spawnCount; i++) {
			int attemptsLeft = attemptsBeforeRejection;
			
			while (attemptsLeft > 0) {
				bool viablePosition = true;
				Vector2 tempPosA = (randomNumberMethod() * spawnRadius);
				
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
			outputPositions[i] = inputPosition + (new Vector3(tempPos.x ,0 , tempPos.y));
		}

		return outputPositions;
	}

	protected void InstantiateFlora(GameObject prefab, GameObject parentObject, Vector3 inputPosition, float distanceToFloor, int count, Vector3 positionsList) {
		Vector3 pos = positionsList;
		pos += Vector3.down * distanceToFloor;

		Object.Instantiate(prefab, pos, Quaternion.identity, parentObject.transform);
	}
	protected void InstantiateFauna(GameObject prefab, GameObject parentObject, Vector3 inputPosition, float distanceToFloor, float heightAboveWater, int count, Vector3 positionsList) {
		Vector3 pos = positionsList;
		float rand = Random.Range(distanceToFloor - minFaunaSpawnDist, heightAboveWater + minFaunaSpawnDist);
		pos += Vector3.down * rand;

		Object.Instantiate(prefab, pos, Quaternion.identity, parentObject.transform);
	}
	protected Vector2 randomVector2insideSquare() {
		float outX = Random.Range(-1.0f, 1.0f);
		float outY = Random.Range(-1.0f, 1.0f);
		return new Vector2(outX, outY);
	}

	protected Vector2 randomVector2insideCircle() {
		return Random.insideUnitCircle;
	}

	protected float waterDepth(Vector3 inputPosition, float heightAboveWater) {
		float output = 0.0f;

		RaycastHit hit;
		Ray downRay = new Ray(inputPosition, Vector3.down);

		if (Physics.Raycast(downRay, out hit)) {
			output = (hit.distance - heightAboveWater);
			
		}
		return output;
	}
	protected void DrawGizmos(Vector3 objectPosition, float spawnRadius) {
		Gizmos.color = new Color(1.0f, 0, 0, 0.5f);
		Gizmos.DrawSphere(objectPosition, spawnRadius);
	}

}
