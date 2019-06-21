using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCluster : MonoBehaviour
{
	public GameObject plantObject;
	private int resources;

	public void spawnSurrounding(int number, GameObject obj){
		for(int i = 0; i < number; i++){
			//Spawn surrounding plants adding a value to cluster resources available for each.
			resources

			 += 10;
			GameObject plant = Instantiate(obj, findPlantPosition(gameObject), obj.transform.rotation);
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
	public void Consume(){
		if (resources > 10){
			//delete this plant node.
		} else {
			resources -= 10;
		}
	}
}
