using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaunaSpawner : Spawner {

    // Arrays
	private Vector3[] faunaClusterPositions;
    private Vector3[][] faunaPositions;
    private float[] distanceFromSeaFloor;
    private GameObject[] fauna;

    //Settings
    private SpawnerSettings objectSettings;
    private Biome biomeSettings;

	void Awake() {
        biomeSettings = GetComponent<Biome>();
		objectSettings = biomeSettings.fauna.GetComponent<SpawnerSettings>(); //Note: Need to use multiple settings for different types of fauna in future to prevent collisions
        
        // Instatiating Arrays
        faunaPositions = new Vector3[biomeSettings.faunaClusterSpawnCount][];
        faunaPositions = new Vector3[biomeSettings.clusterSpawnCount][];
        distanceFromSeaFloor = new float[biomeSettings.faunaSpawnCount];
        fauna = new GameObject[biomeSettings.faunaSpawnCount];
        
        float clusterSeparationDistance = (biomeSettings.radiusAroundFaunaCluster + (biomeSettings.faunaClusterRadius * 2.0f));
        float faunaSeparationDistance = (biomeSettings.radiusAroundFauna + (objectSettings.objectRadius * objectSettings.objectScale * 2.0f));
        float clusterSpawningRadius = biomeSettings.biomeRadius - biomeSettings.faunaClusterRadius;
        float heightFromWater = transform.position.y - biomeSettings.water.transform.position.y;

        faunaClusterPositions = GeneratePositions(transform.position, clusterSpawningRadius, clusterSeparationDistance, biomeSettings.faunaClusterSpawnCount, biomeSettings.attemptsBeforeRejection, randomVector2insideSquare);

        for (int i = 0; i < biomeSettings.clusterSpawnCount; i++) {

            if (faunaClusterPositions[i] != transform.position) {
                faunaPositions[i] = GeneratePositions(faunaClusterPositions[i], biomeSettings.faunaClusterRadius, faunaSeparationDistance, biomeSettings.faunaSpawnCount, biomeSettings.attemptsBeforeRejection, randomVector2insideCircle);

                for (int count = 0; count < biomeSettings.faunaSpawnCount; count++) {
                    float depth = waterDepth(faunaPositions[i][count], heightFromWater);
                    distanceFromSeaFloor[count] = depth + heightFromWater;

                    if (depth < biomeSettings.shallowDeepBoundary) {
                    fauna[count] = biomeSettings.fauna;
                    } else {
                    fauna[count] = biomeSettings.fauna;
                    }
                }

                InstantiateFauna(fauna, this.gameObject, faunaClusterPositions[i], distanceFromSeaFloor, heightFromWater, biomeSettings.faunaSpawnCount, faunaPositions[i]);
            }
        }
	}
}
