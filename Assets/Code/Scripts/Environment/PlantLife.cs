using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLife : MonoBehaviour
{

    public GameObject floor; // Refrence to the ocean floor prefab
    public GameObject plant; // Refrence to plant object to spawn in
    public GameObject midPlant; // Second plant

    //sub plants
    public GameObject sSubPlant;
    public GameObject mSubPlant;

    public int plantNumber;

    // Use this for initialization
    void Start()
    {
        //Populate shallow depth bounding boxes with required plants
        foreach (GameObject sPlant in GameObject.FindGameObjectsWithTag("shallows"))
        {
            createPlants(plantNumber, sPlant, plant, GameObject.Find("plants"));
        }
        //Populate mid depth bounding boxes with required plants
        foreach (GameObject mPlant in GameObject.FindGameObjectsWithTag("mid"))
        {
            createPlants(plantNumber, mPlant, midPlant, GameObject.Find("midPlants"));
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    void createPlants(int number, GameObject area, GameObject plantType, GameObject parent)
    {      
        for (var i = 0; i <= number; i++)
        {
            //Instantiate plant object and assign it a name while making it a child of nearest plants.
            GameObject thisPlant = Instantiate(plantType, findPlantPosition(area), plantType.transform.rotation);
            thisPlant.name = plantType.name;
            thisPlant.transform.parent = parent.transform;

            if(thisPlant.transform.position.y > floor.transform.position.y)
            {
                RaycastHit hit;
                Vector3 dwn = thisPlant.transform.TransformDirection(Vector3.down);
                if(Physics.Raycast(thisPlant.transform.position, dwn, out hit, 50))
                {
                    thisPlant.transform.position = hit.point;
                }
            }
        }
    }
    public Vector3 findPlantPosition(GameObject obj)
    {
        Renderer r = obj.GetComponent<Renderer>();
        float randomX = Random.Range(r.bounds.min.x, r.bounds.max.x);
        float randomZ = Random.Range(r.bounds.min.z, r.bounds.max.z);
        Vector3 spawnPosition = new Vector3(randomX, 45.0f, randomZ);
        return spawnPosition;
    }

}

// ToDo
// - Add function for checking next plant to place does not collide with existing plants.