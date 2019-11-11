using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour {

	[SerializeField] private enum GizmoType { Never, SelectedOnly, Always }
	[SerializeField] private GameObject prefab;
	[SerializeField] private LayerMask discludeLayer;
	[SerializeField] private float spawnRadius = 10.0f;
	[SerializeField] private int spawnCount = 10;
	[SerializeField] private GizmoType showSpawnRegion;
	
	private Vector3[] plantPositions;

	void Awake() {
		plantPositions = new Vector3[spawnCount];

		for (int i = 0; i < spawnCount; i++) {
			Quaternion zero = Quaternion.identity;
			Vector3 pos = transform.position + (Random.insideUnitSphere * spawnRadius);

			RaycastHit hit;
			Ray downRay = new Ray(pos, Vector3.down);

			if (Physics.Raycast(downRay, out hit)) {
				pos += Vector3.down * hit.distance;
			}

			plantPositions[i] = pos;
			Object.Instantiate(prefab, pos, zero);
		}
	}

	private void OnDrawGizmos() {
		if (showSpawnRegion == GizmoType.Always) {
			DrawGizmos();
		}
	}

	private void OnDrawGizmosSelected() {
		if (showSpawnRegion == GizmoType.SelectedOnly) {
			DrawGizmos();
		}
	}

	private void DrawGizmos() {
		Gizmos.color = new Color(0, 0, 0.5f, 0.5f);
		Gizmos.DrawSphere(transform.position, spawnRadius);
	}

}
