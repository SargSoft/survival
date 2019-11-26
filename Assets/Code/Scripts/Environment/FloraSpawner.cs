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

    private Vector3[] clusterPositions;
    private Vector3[][] floraPositions;
    private SpawnerSettings objectSettings;

    void Awake() {
		objectSettings = prefab.GetComponent<SpawnerSettings>();
        floraPositions = new Vector3[clusterSpawnCount][];
        float clusterSeparationDistance = (radiusAroundCluster + (clusterRadius * 2.0f));
        float floraSeparationDistance = (radiusAroundFlora + (objectSettings.objectRadius * objectSettings.objectScale * 2.0f));

        clusterPositions = GeneratePositions(transform.position, biomeRadius, clusterSeparationDistance, clusterSpawnCount, attemptsBeforeRejection, randomVector2insideSquare);

        for (int i = 0; i < clusterSpawnCount; i++) {
            floraPositions[i] = GeneratePositions(clusterPositions[i], clusterRadius, floraSeparationDistance, floraSpawnCount, attemptsBeforeRejection, randomVector2insideCircle);

            InstatiateObjects(prefab, this.gameObject, clusterPositions[i], floraSpawnCount, floraPositions[i]);
        }
	}

    protected void OnDrawGizmosSelected() {
		// DrawGizmos();
        // Implement in above Awake() section so that in draws a gizmo on each cluster
	}
}
