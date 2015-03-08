using UnityEngine;
using System.Collections;

public class Hurtbox : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		SendMessageUpwards ("OnHurtboxHit", SendMessageOptions.DontRequireReceiver);
	}
}
