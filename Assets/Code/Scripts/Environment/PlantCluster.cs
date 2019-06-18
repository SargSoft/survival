using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCluster : MonoBehaviour
{
	public GameObject plantObject;
	public int clusterResourcesAvailable;

	public void spawnSurrounding(int number, GameObject obj){
		for(int i = 0; i < number; i++){
			//Spawn surrounding plants adding a value to cluster resources available for each.
			clusterResourcesAvailable += 10;
			GameObject plant = Instantiate(obj, findPlantPosition(gameObject), gameObject.transform.rotation);
			plant.transform.parent = gameObject.transform;

			//position spawned plants randomly around this.transform
		}
	}
	public Vector3 findPlantPosition(GameObject cluster){
		return new Vector3(
			Random.Range(cluster.transform.position.x + 1, cluster.transform.position.x - 1),
			cluster.transform.position.y,
			Random.Range(cluster.transform.position.z + 1, cluster.transform.position.x - 1)
		);
	}
}
