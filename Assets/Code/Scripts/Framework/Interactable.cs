using UnityEngine;

public class Interactable : MonoBehaviour
{
	public float radius = 3f;

	public virtual void Interact() {

	}

	void Update() {
		Interact();
	}
}
