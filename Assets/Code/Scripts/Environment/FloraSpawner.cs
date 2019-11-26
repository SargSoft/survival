using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloraSpawner : Spawner {

    private Vector3[] clusterPositions;
    private Vector3[][] floraPositions;
    private SpawnerSettings objectSettings;
    private Biome biomeSettings;

    void Awake() {
        biomeSettings = GetComponent<Biome>();
		objectSettings = biomeSettings.shallowFlora.GetComponent<SpawnerSettings>();
        floraPositions = new Vector3[biomeSettings.clusterSpawnCount][];
        float clusterSeparationDistance = (biomeSettings.radiusAroundCluster + (biomeSettings.clusterRadius * 2.0f));
        float floraSeparationDistance = (biomeSettings.radiusAroundFlora + (objectSettings.objectRadius * objectSettings.objectScale * 2.0f));
        float clusterSpawningRadius = biomeSettings.biomeRadius - biomeSettings.clusterRadius;

        clusterPositions = GeneratePositions(transform.position, clusterSpawningRadius, clusterSeparationDistance, biomeSettings.clusterSpawnCount, biomeSettings.attemptsBeforeRejection, randomVector2insideSquare);

        for (int i = 0; i < biomeSettings.clusterSpawnCount; i++) {

            if (clusterPositions[i] != transform.position) {
                floraPositions[i] = GeneratePositions(clusterPositions[i], biomeSettings.clusterRadius, floraSeparationDistance, biomeSettings.floraSpawnCount, biomeSettings.attemptsBeforeRejection, randomVector2insideCircle);
                float depth = waterDepth(clusterPositions[i], 118.1f);
                GameObject flora;

                if (depth < 20.0f) {
                    flora = biomeSettings.shallowFlora;
                } else {
                    flora = biomeSettings.deepFlora;
                }

                InstatiateObjects(flora, this.gameObject, clusterPositions[i], biomeSettings.floraSpawnCount, floraPositions[i]);
            }
        }
	}

    protected void OnDrawGizmosSelected() {
		// DrawGizmos();
        // Implement in above Awake() section so that in draws a gizmo on each cluster
	}
}
