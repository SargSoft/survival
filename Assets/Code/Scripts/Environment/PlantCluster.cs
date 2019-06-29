using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCluster : MonoBehaviour
{
	public GameObject plantObject;
	private int resources;
	private float plantMeshX;

	public void spawnSurrounding(int number, GameObject obj) {
		for( int i = 0; i < number; i++ ){
			//Spawn surrounding plants adding a value to cluster resources available for each.
			resources += 10;
			GameObject plant = Instantiate(obj, findPlantPosition(gameObject), obj.transform.rotation);
			plant.transform.parent = gameObject.transform;

			// Get mesh x size to calculate minimum distance other plants can be placeds
			Mesh plantMesh = plant.GetComponent<MeshFilter>().mesh;
			plantMeshX = plantMesh.bounds.size.x;	
		}
	}

	public Vector3 findPlantPosition(GameObject cluster) {
		return new Vector3(
			Random.Range( cluster.transform.position.x + 10, cluster.transform.position.x - 10 ),
			cluster.transform.position.y,
			Random.Range( cluster.transform.position.z + 10, cluster.transform.position.x - 10 )
		);
	}

	

	// Check each plant is not clipping before final placement
	private void checkForClipping(GameObject firstObject, GameObject secondObject) {
		RaycastHit hit;
	}

	public void Consume(){
		if ( resources > 10 ){
			//delete this plant node.
		} else {
			resources -= 10;
		}
	}
}
