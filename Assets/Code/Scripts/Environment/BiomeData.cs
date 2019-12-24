using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeData : MonoBehaviour {
	// Defining custom struct and enum
	public enum BiomeType {IntertidalZone, PelagicZone, BenthicZone, AbyssalZone, Reef}
	public struct BiomeInfo {
		public BiomeType biome;
		public GameObject shallowFauna;
		public GameObject deepFauna;
		public GameObject shallowFlora;
		public GameObject deepFlora;
	}

	// Variables
	public GameObject water;
	public BiomeInfo[] biomeInfoArray;

	// GameObjects
	public GameObject shallowFauna;
	public GameObject deepFauna;
	public GameObject shallowFlora;
	public GameObject deepFlora;

	void Awake() {
		biomeInfoArray = new BiomeInfo[5]; // Needs to be the number of biomes in BiomeType

		// Intertidal Zone
		biomeInfoArray[0].biome = BiomeType.IntertidalZone;
		biomeInfoArray[0].shallowFauna = shallowFauna;
		biomeInfoArray[0].deepFauna = deepFauna;
		biomeInfoArray[0].shallowFlora = shallowFlora;
		biomeInfoArray[0].deepFlora = deepFlora;

		// Pelagic Zone
		biomeInfoArray[1].biome = BiomeType.PelagicZone;
		biomeInfoArray[1].shallowFauna = shallowFauna;
		biomeInfoArray[1].deepFauna = deepFauna;
		biomeInfoArray[1].shallowFlora = shallowFlora;
		biomeInfoArray[1].deepFlora = deepFlora;

		// Benthic Zone
		biomeInfoArray[2].biome = BiomeType.BenthicZone;
		biomeInfoArray[2].shallowFauna = shallowFauna;
		biomeInfoArray[2].deepFauna = deepFauna;
		biomeInfoArray[2].shallowFlora = shallowFlora;
		biomeInfoArray[2].deepFlora = deepFlora;

		// Abyssal Zone
		biomeInfoArray[3].biome = BiomeType.AbyssalZone;
		biomeInfoArray[3].shallowFauna = shallowFauna;
		biomeInfoArray[3].deepFauna = deepFauna;
		biomeInfoArray[3].shallowFlora = shallowFlora;
		biomeInfoArray[3].deepFlora = deepFlora;

		// Reef
		biomeInfoArray[4].biome = BiomeType.Reef;
		biomeInfoArray[4].shallowFauna = shallowFauna;
		biomeInfoArray[4].deepFauna = deepFauna;
		biomeInfoArray[4].shallowFlora = shallowFlora;
		biomeInfoArray[4].deepFlora = deepFlora;
	}
}
