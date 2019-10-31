using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoal : MonoBehaviour {

	// [SerializeField] Camera cameraObject;
	[SerializeField] GameObject boidPrefab;
	[SerializeField] float moveSpeed;
	[SerializeField] int shoalSize;
	[SerializeField] float spawnRadius;
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
}
