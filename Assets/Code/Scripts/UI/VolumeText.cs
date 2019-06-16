using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumeText : MonoBehaviour {

	private TextMeshProUGUI volumeText;

	void Start() {
		volumeText = GetComponent<TextMeshProUGUI> ();
	}

    void Update() {
    	volumeText.text = MainMenu.displayVolume;
    	// Debug.Log("The current volume is: " + MainMenu.displayVolume);
    }
}
