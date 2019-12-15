using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeData : MonoBehaviour {

	public enum BiomeType {IntertidalZone, PelagicZone, BenthicZone, AbyssalZone, Reef}
	[System.Serializable]
	public struct BiomeInfo {
		public BiomeType biome;
		public GameObject shallowFauna;
		public GameObject deepFauna;
		public GameObject shallowFlora;
		public GameObject deepFlora;
	}

	[Header("Water")]
	public GameObject water;
	
	[Header("Biomes")]
	public BiomeInfo intertidalZone;
	public BiomeInfo pelagicZone;
	public BiomeInfo benthicZone;
	public BiomeInfo abyssalZone;
	public BiomeInfo reef;
}
