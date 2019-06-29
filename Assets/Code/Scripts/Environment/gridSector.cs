using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridSector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
   public void fillSector() {
    	//spawn plant nodes
   		depthDetection();
    	Debug.Log("fill Zone with plant clusters of correct type");
    	Destroy(this.gameObject);
    }
    private void depthDetection() {
    	//Cast ray to work out depth
    	Debug.Log("what depth is this?");
    }
}
