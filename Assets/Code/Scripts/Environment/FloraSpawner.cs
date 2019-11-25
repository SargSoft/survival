using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloraSpawner : Spawner {

    [Header("Cluster Spawning Settings")]
    [SerializeField] private float biomeRadius = 500.0f;
    [SerializeField] private int clusterSpawnCount = 10;
    [SerializeField] private float radiusAroundCluster = 50.0f;
	[SerializeField] private float clusterRadius = 10.0f;
    [Header("Flora Spawning Settings")]
	[SerializeField] private float radiusAroundFlora = 5.0f;
	[SerializeField] private int floraSpawnCount = 10;
    [Header("Other")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int attemptsBeforeRejection = 30;

    private Vector3[] plantPositions;
    private SpawnerSettings objectSettings;
    private float objectDiameter;
	private float objectSeparation;

    void Awake() {
		// objectSettings = prefab.GetComponent<SpawnerSettings>();
		// objectDiameter = (objectSettings.objectRadius * objectSettings.objectScale * 2.0f);
		// objectSeparation = objectDiameter + radiusAroundFlora;
		// plantPositions = GeneratePositions(clusterRadius, objectSeparation, floraSpawnCount, attemptsBeforeRejection, randomVector2insideCircle);

        plantPositions = GeneratePositions(biomeRadius, objectSeparation, clusterSpawnCount, attemptsBeforeRejection, randomVector2insideSquare);

		InstatiateObjects(prefab, this.gameObject, floraSpawnCount, plantPositions);
	}

    protected void OnDrawGizmosSelected() {
		// DrawGizmos();
        // Implement in above Awake() section so that in draws a gizmo on each cluster
	}
}
