using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BiomeSettings : ScriptableObject {
	// Settings
    [Header ("Intertidal Zone")]
	public GameObject biome1ShallowFlora;
    public GameObject biome1DeepFlora;
    public GameObject biome1ShallowFauna;
    public GameObject biome1DeepFauna;
	
    [Header ("Pelagic Zone")]
	public GameObject biome2ShallowFlora;
    public GameObject biome2DeepFlora;
    public GameObject biome2ShallowFauna;
    public GameObject biome2DeepFauna;

    [Header ("Benthic Zone")]
	public GameObject biome3ShallowFlora;
    public GameObject biome3DeepFlora;
    public GameObject biome3ShallowFauna;
    public GameObject biome3DeepFauna;

    [Header ("Abyssal Zone")]
	public GameObject biome4ShallowFlora;
    public GameObject biome4DeepFlora;
    public GameObject biome4ShallowFauna;
    public GameObject biome4DeepFauna;

    [Header ("Reef")]
	public GameObject biome5ShallowFlora;
    public GameObject biome5DeepFlora;
    public GameObject biome5ShallowFauna;
    public GameObject biome5DeepFauna;

    [Header ("Tropical")]
	public GameObject biome6ShallowFlora;
    public GameObject biome6DeepFlora;
    public GameObject biome6ShallowFauna;
    public GameObject biome6DeepFauna;
}