using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLife : MonoBehaviour {

    [SerializeField] private GameObject floor; // Refrence to the ocean floor prefab
    [SerializeField] private GameObject plant; // Refrence to plant object to spawn in
    [SerializeField] private GameObject midPlant; // Second plant
    [SerializeField] private int numOfPlants; // number of plants to spawn

    void Start() {
        //Populate shallow depth bounding boxes with required plants
        foreach (GameObject shallowsBox in GameObject.FindGameObjectsWithTag("shallows")) {
            CreatePlants(numOfPlants, shallowsBox, plant, GameObject.Find("Plants"));
        }
        //Populate mid depth bounding boxes with required plants
        foreach (GameObject mediumBox in GameObject.FindGameObjectsWithTag("mid")) {
            CreatePlants(numOfPlants, mediumBox, midPlant, GameObject.Find("MidPlants"));
        }
    }
    // Returns a random xz position within the area occupied by a game object and spawns at a set height (raycast used elsewhere "sticks" spawned object to "area" object)
    public Vector3 FindPlantPosition(GameObject obj) {
        Renderer r = obj.GetComponent<Renderer>();
        return new Vector3(Random.Range(r.bounds.min.x, r.bounds.max.x), 50.0f, Random.Range(r.bounds.min.z, r.bounds.max.z));
    }
    // Sticks obj to an object below it
    public void StickObjectToBelow(GameObject obj, GameObject below) {
        RaycastHit hit;
        Vector3 dwn = obj.transform.TransformDirection(Vector3.down);
        if(Physics.Raycast(obj.transform.position, dwn, out hit, 50)) obj.transform.position = hit.point;
    }
    // Creates a set number of plants(arg1) in and area(arg2) of type(arg3) and makes them child of(arg4).
    public void CreatePlants(int number, GameObject area, GameObject plantType, GameObject parent) {      
        for (var i = 0; i <= number; i++) {
            //Instantiate plant object and assign it a name while making it a child of nearest plants.
            GameObject thisPlant = Instantiate(plantType, FindPlantPosition(area), plantType.transform.rotation);
            StickObjectToBelow(thisPlant, floor);
            thisPlant.name = plantType.name;
            thisPlant.transform.parent = parent.transform;         
        }
    }
}