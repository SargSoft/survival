using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignDepthZones : MonoBehaviour
{
	[Header("Prefab References")]
	[SerializeField] private GameObject shallowPlant;
	[SerializeField] private GameObject deepPlant;
	[SerializeField] private GameObject waterSpace;
	[SerializeField] private GameObject sector;
	
	[Header("Controls")]
 	[Range(1,20)]
	[SerializeField] private int plantDensity = 3;
	[SerializeField] private int numberOfTerrain;

	private int spawnedZones = 7;
	private List<GameObject> terrainObjects = new List<GameObject>();
	private List<GameObject> spawnSectors = new List<GameObject>();

    void Start()
    {
    	//Adds all terrain Objects to list
        for( int i = 0 ; i < numberOfTerrain ; i++ ){
        	terrainObjects.Add(this.gameObject.transform.GetChild(i).gameObject);
        }
        spawnPlants();
    }

    void spawnPlants() {

    	Vector3 newPosition = new Vector3(0,0,0);		
    	for ( int axisZ = 0; axisZ < 10; axisZ++){
    		newPosition.x = 0;
    		for (int axisX = 0; axisX < 10; axisX++){
    			createSector(newPosition, "X: " + axisX + " Z: " + axisZ);
    			newPosition.x += 100;
    		}
    		newPosition.z += 100;
    	}
    	
    	foreach (GameObject sec in spawnSectors) {
    		Debug.Log(sec.name);
    	}

    	// for each quadrant row

    		//for each quadrant length 

    			//Decide if this quadrant is a spawn zone, if yes 

    			//find centre of quadrant
    			// if ray cast from 0 down to centre of quadrant is inside boundry 

    }

    // A zone is marked as active or dormant, less chance the more there are (should be balanced)
    private bool isSpawnZone() {
    	bool returnValue = false;
    	float spawnThreshold = 5.0f;
    	float spawnChance = 10.0f;

    	for ( int i = 0; i <= spawnedZones; i++ ) {
    		spawnThreshold += 0.20f;
    		Debug.Log(spawnThreshold);
    	} 

    	if ( Random.Range( 0.0f, spawnChance ) >= spawnThreshold ) {
    		return true;
    	} else {
    		Debug.Log("false");
    		return false;
    	}
    }

    void createSector(Vector3 createAt, string name) {
    	GameObject newSector = Instantiate(sector, createAt, this.transform.rotation);
    	newSector.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
    	newSector.name = name;
    	spawnSectors.Add(newSector);
    }
}
