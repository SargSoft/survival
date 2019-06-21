using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {
	//UI Variables
	public Slider healthSlider;
	public int maxHealth;

	public Slider energySlider;
	public int maxEnergy;

	public Slider oxygenSlider;
	public int maxOxygen;
	// Start is called before the first frame update
	void Start() {
		//Initializing player vitals
		healthSlider.maxValue = maxHealth;
		healthSlider.value = maxHealth;

		energySlider.maxValue = maxEnergy;
		energySlider.value = maxEnergy;

		oxygenSlider.maxValue = maxOxygen;
		oxygenSlider.value = maxOxygen;
	}

	// Update is called once per frame
	void Update() {
		
	}
}
