using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {

	[SerializeField]
	private Transform target;
	public Transform Target {
		get {
			return target;
		}
		set {
			target = value;
		}
	}

	[SerializeField]
	private float trackSpeed = 10;

	// Track target
	void LateUpdate() {
		if (target != null) {
			Vector3 v = transform.position;
			v.x = target.position.x;
			v.y = target.position.y;
			transform.position = Vector3.MoveTowards (transform.position, v, trackSpeed * Time.deltaTime);
		}
	}
}