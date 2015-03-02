using UnityEngine;
using System.Collections;

public class DrawHurtbox : MonoBehaviour {

	void Awake() {
#if UNITY_EDITOR
		//Continue
#else
		Destroy (this);
#endif
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		CircleCollider2D[] hurtboxes = GetComponentsInChildren<CircleCollider2D> ();
		for (int i = 0; i < hurtboxes.Length; i++) {
			Gizmos.DrawSphere(hurtboxes[i].transform.position, hurtboxes[i].radius);
		}
	}
}
