using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignDepthZones : MonoBehaviour
{
	[Header("Prefab References")]
	[SerializeField] private GameObject shallowPlant;
	[SerializeField] private GameObject deepPlant;
	[SerializeField] private GameObject waterLevel;
	[SerializeField] private GameObject sector;
	[SerializeField] private GameObject plantNode;
	
	[Header("Controls")]
 	[Range(1,20)]
	[SerializeField] private int plantDensity = 1;
	[SerializeField] private int numberOfTerrain;

	private List<GameObject> spawnSectors = new List<GameObject>();

    void Start()
    {
        spawnZones();
    }

    // If isSpawnZone() == true create sector in that grid space
    void spawnZones() {
    	Vector3 newPosition = new Vector3(0,0,0);		
    	for ( int axisZ = 0; axisZ < 10; axisZ++){
    		newPosition.x = 0;
    		for (int axisX = 0; axisX < 10; axisX++){
    			if(isSpawnZone()){
    				createSector(newPosition, "X: " + axisX + " Z: " + axisZ);
    			}			
    			newPosition.x += 100;
    		}
    		newPosition.z += 100;
    	}
    }

    // 25% chance returns true
    private bool isSpawnZone() {
    	if ( Random.Range( 0.0f, 10.0f ) >= 7.5f ) {
    		return true;
    	} else {
    		return false;
    	}
    }

    //Creates instance of spawn sector and has it call fillSector() found on that instance.
    void createSector(Vector3 createAt, string name) {
    	GameObject newSector = Instantiate(sector, createAt, this.transform.rotation);
    	newSector.name = name;
    	GridSector scriptReference = newSector.GetComponent<GridSector>();
    	scriptReference.depthDetection(deepPlant, shallowPlant);
    	scriptReference.fillSector(plantNode, plantDensity);
    }
}