using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloraSpawner : Spawner {

    // Arrays
    private Vector3[] floraClusterPositions;
    private Vector3[][] floraPositions;
    private float[] distanceFromSeaFloor;
    private GameObject[] flora;

    // Settings
    private SpawnerSettings objectSettings;
    private Biome biomeSettings;
    private BiomeData biomeData;
    private BiomeData.BiomeInfo biomeObjects;

    void Start() {
        biomeSettings = GetComponent<Biome>();
        biomeData = GetComponent<BiomeData>();

        biomeObjects = biomeData.biomeInfoArray[(int)biomeSettings.thisBiome];

		objectSettings = biomeObjects.shallowFlora.GetComponent<SpawnerSettings>(); //Note: Need to use multiple settings for different types of fauna in future to prevent collisions
        
        // Instantiating Arrays
        floraClusterPositions = new Vector3[biomeSettings.floraClusterSpawnCount];
        floraPositions = new Vector3[biomeSettings.floraClusterSpawnCount][];
        distanceFromSeaFloor = new float[biomeSettings.floraSpawnCount];
        flora = new GameObject[biomeSettings.floraSpawnCount];

        float clusterSeparationDistance = (biomeSettings.radiusAroundFloraCluster + (biomeSettings.floraClusterRadius * 2.0f));
        float floraSeparationDistance = (biomeSettings.radiusAroundFlora + (objectSettings.objectRadius * objectSettings.objectScale * 2.0f));
        float clusterSpawningRadius = biomeSettings.biomeRadius - biomeSettings.floraClusterRadius;
        float heightFromWater = transform.position.y - biomeData.water.transform.position.y;

        floraClusterPositions = GeneratePositions(transform.position, clusterSpawningRadius, clusterSeparationDistance, biomeSettings.floraClusterSpawnCount, biomeSettings.attemptsBeforeRejection, randomVector2insideSquare);

        for (int i = 0; i < biomeSettings.floraClusterSpawnCount; i++) {

            if (floraClusterPositions[i] != transform.position) {
                floraPositions[i] = GeneratePositions(floraClusterPositions[i], biomeSettings.floraClusterRadius, floraSeparationDistance, biomeSettings.floraSpawnCount, biomeSettings.attemptsBeforeRejection, randomVector2insideCircle);

                for (int count = 0; count < biomeSettings.floraSpawnCount; count++ ) {
                    float depth = waterDepth(floraPositions[i][count], heightFromWater);
                    distanceFromSeaFloor[count] = heightFromWater + depth;

                    if (depth < biomeSettings.shallowDeepBoundary) {
                    flora[count] = biomeObjects.shallowFlora;
                    } else {
                    flora[count] = biomeObjects.deepFlora;
                    }

                    if (floraPositions[i][count] != floraClusterPositions[i]) {
                        InstantiateFlora(flora[count], this.gameObject, floraClusterPositions[i], distanceFromSeaFloor[count], biomeSettings.floraSpawnCount, floraPositions[i][count]);
                    }
                }
            }
        }
	}
}
