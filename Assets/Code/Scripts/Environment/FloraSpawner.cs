using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloraSpawner : Spawner {

    [SerializeField] private GameObject prefab;
	[SerializeField] private float spawnRadius = 10.0f;
	[SerializeField] private float radiusAroundObject = 5.0f;
	[SerializeField] private int spawnCount = 10;
	[SerializeField] private int attemptsBeforeRejection = 30;

    private Vector3[] plantPositions;
    private SpawnerSettings objectSettings;
    private float objectDiameter;
	private float objectSeparation;

    void Awake() {
		objectSettings = prefab.GetComponent<SpawnerSettings>();
		objectDiameter = (objectSettings.objectRadius * objectSettings.objectScale * 2.0f);
		objectSeparation = objectDiameter + radiusAroundObject;
		plantPositions = GeneratePositions(spawnRadius, objectSeparation, spawnCount, attemptsBeforeRejection, randomVector2insideCircle);

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

    protected void OnDrawGizmosSelected() {
		// DrawGizmos();
        // Implement in above Awake() section so that in draws a gizmo on each cluster
	}
}
