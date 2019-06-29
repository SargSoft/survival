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
		Destroy(this.gameObject);
	}
}