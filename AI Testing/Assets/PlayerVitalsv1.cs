using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVitalsv1 : MonoBehaviour
{

	//Variables

	public Slider healthSlider;
	public int maxHealth;
	public int healthFallRate;

	public Slider energySlider;
	public int maxEnergy;
	public int energyFallRate;

	public Slider oxygenSlider;
	public int maxOxygen;
	public int oxygenFallRate;


    // Start is called before the first frame update
    void Start()
    {
        
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        energySlider.maxValue = maxEnergy;
        energySlider.value = maxEnergy;

        oxygenSlider.maxValue = maxOxygen;
        oxygenSlider.value = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
