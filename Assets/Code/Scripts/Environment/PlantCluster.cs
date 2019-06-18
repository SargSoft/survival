using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCluster : MonoBehaviour
{
	[SerializeField] private GameObject node;
	[SerializeField] private GameObject plantObject;
	[SerializeField] private int clusterResourcesAvailable;

	void spawnSurrounding(int number){
		for(int i = 0; i < number; i++){
			Debug.Log("spawn plant nearby");
		}
	}
}
