using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour {

	[SerializeField] private enum GizmoType { Never, SelectedOnly, Always }

	[SerializeField] private Boid prefab;
	[SerializeField] private float spawnRadius = 10.0f;
	[SerializeField] private int spawnCount = 10;
	[SerializeField] private GizmoType showSpawnRegion;

	void Awake() {
		for (int i = 0; i < spawnCount; i++) {
			Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
			Object.Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
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
