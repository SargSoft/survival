using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLife : MonoBehaviour {

    public GameObject floor; // Refrence to the ocean floor prefab
    public GameObject plant; // Refrence to plant object to spawn in
    public GameObject midPlant; // Second plant
    public int numOfPlants; // number of plants to spawn

    void Start() {
        //Populate shallow depth bounding boxes with required plants
        foreach (GameObject shallowsBox in GameObject.FindGameObjectsWithTag("shallows")) {
        	Debug.Log("one sPlant in shalllows");
            CreatePlants(numOfPlants, shallowsBox, plant, GameObject.Find("Plants"));
        }
        //Populate mid depth bounding boxes with required plants
        foreach (GameObject mediumBox in GameObject.FindGameObjectsWithTag("mid")) {
            CreatePlants(numOfPlants, mediumBox, midPlant, GameObject.Find("MidPlants"));
        }
    }
    // Creates a set number of plants(arg1) in and area(arg2) of type(arg3) and makes them child of(arg4).
    void CreatePlants(int number, GameObject area, GameObject plantType, GameObject parent) {      
        for (var i = 0; i <= number; i++) {
            //Instantiate plant object and assign it a name while making it a child of nearest plants.
            GameObject thisPlant = Instantiate(plantType, FindPlantPosition(area), plantType.transform.rotation);
            thisPlant.name = plantType.name;
            thisPlant.transform.parent = parent.transform;
            // Use raycast to set plant to same height as area
            if(thisPlant.transform.position.y > floor.transform.position.y) {
                RaycastHit hit;
                Vector3 dwn = thisPlant.transform.TransformDirection(Vector3.down);
                if(Physics.Raycast(thisPlant.transform.position, dwn, out hit, 50)) {
                    thisPlant.transform.position = hit.point;
                }
            }
        }
    }
    // Returns a random xz position within the area occupied by a game object and spawns at a set height (raycast used elsewhere "sticks" spawned object to "area" object)
    public Vector3 FindPlantPosition(GameObject obj) {
        Renderer r = obj.GetComponent<Renderer>();
        float randomX = Random.Range(r.bounds.min.x, r.bounds.max.x);
        float randomZ = Random.Range(r.bounds.min.z, r.bounds.max.z);
        Vector3 spawnPosition = new Vector3(randomX, 50.0f, randomZ);
        return spawnPosition;
    }
}