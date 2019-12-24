using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour {
	
	[Header("Biome Settings")]
    public BiomeData.BiomeType thisBiome;
    public float biomeRadius = 500.0f;
    public int attemptsBeforeRejection = 30;
    public float shallowDeepBoundary = 20.0f;

    [Header("Flora Spawning Settings")]
    public int floraClusterSpawnCount = 10;
    public float radiusAroundFloraCluster = 50.0f;
	public float floraClusterRadius = 10.0f;
	public float radiusAroundFlora = 5.0f;
	public int floraSpawnCount = 10;

    [Header("Fauna Spawning Settings")]
    public int faunaClusterSpawnCount = 3;
    public float radiusAroundFaunaCluster = 20.0f;
    public float faunaClusterRadius = 10.0f;
    public float radiusAroundFauna = 20.0f;
    public int faunaSpawnCount = 15;
    [Range(0f, 0.5f)]
    public float faunaVerticalSpread = 0.1f;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, new Vector3(biomeRadius * 2.0f, biomeRadius * 2.0f, biomeRadius * 2.0f));
        // Implement in above Awake() section so that in draws a gizmo on each cluster
	}
}
