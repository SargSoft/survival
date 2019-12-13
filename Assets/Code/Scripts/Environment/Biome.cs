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
    public float shallowDeepBoundary = 20.0f;
    public int attemptsBeforeRejection = 30;
    [Header("Fauna Spawning Settings")]
    public int faunaClusterSpawnCount = 3;
    public float radiusAroundFaunaCluster = 20.0f;
    public float faunaClusterRadius = 10.0f;
    public float radiusAroundFauna = 20.0f;
    public int faunaSpawnCount = 15;
    [Range(0f, 0.5f)]
    public float faunaVerticalSpread = 0.1f;
    [Header("GameObjects")]
    public GameObject water;
    public GameObject shallowFlora;
    public GameObject deepFlora;
    public GameObject fauna;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, new Vector3(biomeRadius * 2.0f, biomeRadius * 2.0f, biomeRadius * 2.0f));
        // Implement in above Awake() section so that in draws a gizmo on each cluster
	}
}
