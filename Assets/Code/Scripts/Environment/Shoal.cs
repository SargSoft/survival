using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoal : MonoBehaviour {

	// [SerializeField] Camera cameraObject;
	[SerializeField] GameObject boidPrefab;
	[SerializeField] float moveSpeed;
	[SerializeField] int shoalSize;
	[SerializeField] float spawnRadius;
	[SerializeField] float boidInteractionRadius;
	private float height = 28.86751f;
	private float width = 56.48201f;

	private GameObject[] boidsArray;


	void Start() {
		// Debug.Log("Height: " + (2.0f * 25f * Mathf.Tan(60f * 0.5f * Mathf.Deg2Rad)));
		// Debug.Log("Width: " + (cameraObject.aspect * 2.0f * 25f * Mathf.Tan(60f * 0.5f * Mathf.Deg2Rad)));

		boidsArray = new GameObject[shoalSize];

		for (int i = 0; i < shoalSize; i++) {
			Vector2 randomPositionv2 = Random.insideUnitCircle * spawnRadius;
			Vector3 randomPositionv3 = randomPositionv2;
			Vector3 depthCompensation = new Vector3(0, 0, -10);
			Vector3 position = transform.position + randomPositionv3 + depthCompensation;
			Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
			GameObject boid = Instantiate(boidPrefab, position, rotation) as GameObject;
			boidsArray[i] = boid;
		}
	}

	void Update() {

		for (int i = 0; i < shoalSize; i++) {
			GameObject boid = boidsArray[i];

			Quaternion tempRotation = SteeringBehaviours(boid.transform.position);

			if (tempRotation != Quaternion.Euler(Vector3.zero)) {
				boid.transform.rotation = tempRotation;
			}

			boid.transform.position += boid.transform.up * moveSpeed;

			ScreenWrap(boid);
		}
	}

	private float CheckBound(float position, float diameter) {
		float min = -1 * (diameter / 2);
		float max = (diameter / 2);
		float output = position;

		if (position > max) {
			output = min + (position - max);
		} else if (position < min) {
			output = max + (position - min);
		}

		return output;

	}

	private void ScreenWrap(GameObject boid) {
		float checkY = CheckBound(boid.transform.position.y, height);
		float checkX = CheckBound(boid.transform.position.x, width);

		if (boid.transform.position.y != checkY) {
			boid.transform.position = new Vector3 (boid.transform.position.x, checkY, boid.transform.position.z);
		}

		if (boid.transform.position.x != checkX) {
			boid.transform.position = new Vector3 (checkX, boid.transform.position.y, boid.transform.position.z);
		}
	}

	private Quaternion SteeringBehaviours(Vector3 currentBoidPosition) {
		List<Vector3> boidPositionsList = new List<Vector3>();
		Quaternion output = Quaternion.Euler(Vector3.zero);

		for (int i = 0; i < shoalSize; i++) {
			GameObject tempBoid = boidsArray[i];
			float boidDist = Vector3.Distance(currentBoidPosition, tempBoid.transform.position);

			if(boidDist < boidInteractionRadius && boidDist != 0.0f) {
				boidPositionsList.Add(tempBoid.transform.position);
			}
		}

		if (boidPositionsList.Count != 0) {
			output = Cohesion(currentBoidPosition, boidPositionsList);
			output = Alignment(currentBoidPosition, boidPositionsList);
			output = Separation(currentBoidPosition, boidPositionsList);
		}

		return output;
	}

	private Quaternion Separation(Vector3 currentBoidPosition, List<Vector3> boidPositionsList) {
		Quaternion output = Quaternion.Euler(Vector3.zero);

		return output;
	}

	private Quaternion Alignment(Vector3 currentBoidPosition, List<Vector3> boidPositionsList) {
		Quaternion output = Quaternion.Euler(Vector3.zero);

		return output;
	}

	private Quaternion Cohesion(Vector3 currentBoidPosition, List<Vector3> boidPositionsList) {
		Quaternion output = Quaternion.Euler(Vector3.zero);

		Vector3 sum = Vector3.zero;
		float i = 0.0f;

		foreach(Vector3 tempBoidPosition in boidPositionsList) {
			i++;
			sum += tempBoidPosition;
		}

		sum = sum / i;

		Quaternion.LookRotation(sum);

		return output;
	}
}
