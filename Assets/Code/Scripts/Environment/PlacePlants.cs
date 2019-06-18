using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlants : MonoBehaviour {
	[SerializeField] private GameObject flPlant;
	[SerializeField] private GameObject slPlant;
	[SerializeField] private int plantDensity;
	[SerializeField] private int plantClusterDensity;
    
	void Start() {
        foreach (GameObject spawnArea in GameObject.FindGameObjectsWithTag("spawnArea")) {
        	initPlants(spawnArea, depthDetection(spawnArea), plantDensity, plantClusterDensity);       	
        }
    }
    public void initPlants(GameObject area, GameObject plantClass, int density, int clusterSize) {
    	//for i in density instantiate plantClass.node randomly placed within area
    	//for i in cluster density instantiate plantClass.plant randomly within certain distance of node, making sure no overlap
    } 
    public GameObject depthDetection(GameObject zone) {
    	return new GameObject();
    	//Create the game object which will have a class attached with plant info

    	//Work out depth zone

    	//if depth zone is shallow change gameobject class properties to small plant

    	//other...

    }
    
}
