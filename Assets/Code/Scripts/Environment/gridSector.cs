using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSector : MonoBehaviour
{
	private GameObject plantType;



	// Assigns plantType the correct object
    public void depthDetection(GameObject deepPlant, GameObject shallowPlant) {
		//Cast ray to work out depth
    }

    //spawn plant nodes
	public void fillSector(GameObject node, int density) {
		Vector3 newPosition = transform.position + new Vector3 (50,0,50);
		GameObject plantCluster = Instantiate(node, newPosition, transform.rotation);

		PlantNode scriptReference = plantCluster.GetComponent<PlantNode>();
		for (int i = 0 ; i < density ; i++) {
			scriptReference.spawnPlant();
		}
		Destroy(this.gameObject);
	}
}