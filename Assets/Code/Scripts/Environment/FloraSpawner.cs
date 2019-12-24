using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloraSpawner : Spawner {

    // Arrays
    private Vector3[] floraClusterPositions;
    private Vector3[][] floraPositions;
    private float[] distanceFromSeaFloor;
    private GameObject flora;

    // Settings
    private SpawnerSettings shallowFloraSettings;
    private SpawnerSettings deepFloraSettings;
    private Biome biomeSettings;

    void Start() {
        biomeSettings = GetComponent<Biome>();

		shallowFloraSettings = biomeSettings.shallowFlora.GetComponent<SpawnerSettings>();
        deepFloraSettings = biomeSettings.deepFlora.GetComponent<SpawnerSettings>();
        
        // Instantiating Arrays
        floraClusterPositions = new Vector3[biomeSettings.floraClusterSpawnCount];
        floraPositions = new Vector3[biomeSettings.floraClusterSpawnCount][];
        distanceFromSeaFloor = new float[biomeSettings.floraSpawnCount];

        float clusterSeparationDistance = (biomeSettings.radiusAroundFloraCluster + (biomeSettings.floraClusterRadius * 2.0f));
        float shallowFloraSeparationDistance = (biomeSettings.radiusAroundFlora + (shallowFloraSettings.objectRadius * shallowFloraSettings.objectScale * 2.0f));
        float deepFloraSeparationDistance = (biomeSettings.radiusAroundFlora + (deepFloraSettings.objectRadius * deepFloraSettings.objectScale * 2.0f));
        float clusterSpawningRadius = biomeSettings.biomeRadius - biomeSettings.floraClusterRadius;
        float heightFromWater = transform.position.y - biomeSettings.water.transform.position.y;

        floraClusterPositions = GeneratePositions(transform.position, clusterSpawningRadius, clusterSeparationDistance, biomeSettings.floraClusterSpawnCount, biomeSettings.attemptsBeforeRejection, randomVector2insideSquare);

        for (int i = 0; i < biomeSettings.floraClusterSpawnCount; i++) {

            if (floraClusterPositions[i] != transform.position) {
                float clusterDepth = waterDepth(floraClusterPositions[i], heightFromWater);

                if(clusterDepth < biomeSettings.shallowDeepBoundary) {
                    floraPositions[i] = GeneratePositions(floraClusterPositions[i], biomeSettings.floraClusterRadius, shallowFloraSeparationDistance, biomeSettings.floraSpawnCount, biomeSettings.attemptsBeforeRejection, randomVector2insideCircle);
                    
                    InstantiateFlora(biomeSettings.shallowFlora, this.gameObject, floraClusterPositions[i], heightFromWater, biomeSettings.floraSpawnCount, floraPositions[i]);
                
                } else {
                    floraPositions[i] = GeneratePositions(floraClusterPositions[i], biomeSettings.floraClusterRadius, deepFloraSeparationDistance, biomeSettings.floraSpawnCount, biomeSettings.attemptsBeforeRejection, randomVector2insideCircle);
                    
                    InstantiateFlora(biomeSettings.deepFlora, this.gameObject, floraClusterPositions[i], heightFromWater, biomeSettings.floraSpawnCount, floraPositions[i]);
                
                }
            }
        }
	}
}
