using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour {
	
	[Header("Cluster Spawning Settings")]
    public float biomeRadius = 500.0f;
    public int clusterSpawnCount = 10;
    public float radiusAroundCluster = 50.0f;
	public float clusterRadius = 10.0f;
    [Header("Flora Spawning Settings")]
	public float radiusAroundFlora = 5.0f;
	public int floraSpawnCount = 10;
    public int attemptsBeforeRejection = 30;
    [Header("GameObjects")]
    public GameObject shallowFlora;
    public GameObject deepFlora;
}
