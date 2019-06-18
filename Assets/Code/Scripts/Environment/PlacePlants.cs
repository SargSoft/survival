using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlants : MonoBehaviour {
	public GameObject flPlant;
	public GameObject slPlant;
	[SerializeField] private int plantDensity;
	[SerializeField] private int plantClusterDensity;
    
	void Start() {
        foreach (GameObject spawnArea in GameObject.FindGameObjectsWithTag("spawnArea")) {
        	SpawnPlants(spawnArea);     	
        }
    }
    public GameObject SpawnPlants(GameObject zone) {
    	GameObject plantNode = new GameObject();
    	plantNode.name = "PlantNode";
        plantNode.AddComponent<PlantCluster>();
        PlantCluster tempScript = plantNode.GetComponent<PlantCluster>();
        tempScript.plantObject = slPlant;

        //Spawn plant objects surrounding cluster making child of
        tempScript.spawnSurrounding(plantClusterDensity, tempScript.plantObject);
        return plantNode;
    	//Create the game object which will have a class attached with plant info

    	//Work out depth zone

    	//if depth zone is shallow change gameobject class properties to small plant

    	//other...

    }
    public void FindPosition() {
    	//find position for each node.
    }
    public void FindDepth(GameObject area){
    	RaycastHit hit;
    	Vector3 dwn = area.transform.TransformDirection(Vector3.down);
    	if(Physics.Raycast(area.transform.position, dwn, out hit, 500)) Debug.Log("hit something below");
    }
    
}
