﻿using UnityEngine;
using System.Collections;
using UnityUtilLib;

public enum CharacterFacing {
	Left = -1,
	Right = 1
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Character : MonoBehaviour {
	
	private const string groundedTag = "Ground";

	[SerializeField]
	private float walkSpeed = 10;
	[SerializeField]
	private float runSpeed = 20;
	[SerializeField]
	private float jumpHeight = 20;
	[SerializeField]
	private int jumpCount = 2;
	[SerializeField]
	private float jumpDampening = 0.5f;
	[SerializeField]
	private Transform modelTransform;

	private Rigidbody2D rigBod;
	private int jumpsRemaining = 2;

	private bool grounded = true;
	public bool IsGrounded {
		get {
			return grounded;
		}
	}

	private CharacterFacing facingDirection = CharacterFacing.Right;
	public CharacterFacing FacingDirection {
		get {
			return facingDirection;
		}
	}

	private const float facingDeadZone = 0.01f;

	//TODO: implement
	private bool running;
	private bool hit;
	private MultiplayerInput.PlayerInput controlInput;

	private void Awake() {
		rigBod = GetComponent<Rigidbody2D>();
		hit = false;
		controlInput = MultiplayerInput.GetPlayerControl (1);
	}

	//FixedUpdate is for handling physics/control
	protected virtual void FixedUpdate() {
		float dt = Time.fixedDeltaTime;
		float horizontal = controlInput.GetAxisRaw ("Left Stick Horizontal");
		float vertical = controlInput.GetAxisRaw ("Left Stick Vertical");
		Debug.Log(horizontal + " " + vertical);
		float m = rigBod.mass;
		Vector2 v = rigBod.velocity;

		if(Mathf.Abs(v.x) > facingDeadZone) {
			if(v.x > 0) {
				facingDirection = CharacterFacing.Right;
			} else if(v.x < 0) {
				facingDirection = CharacterFacing.Left;
			}
		}

		modelTransform.rotation = Quaternion.Euler (0f, 90 * (int)facingDirection, 0f);

		float horizontalSpeed = (running) ? runSpeed : walkSpeed;
		Vector2 movementForce = Vector2.right * (horizontalSpeed - v.magnitude);
		movementForce.x *= Util.Sign (horizontal);
		rigBod.AddForce (movementForce);
		//If on the ground
		if(grounded || jumpsRemaining > 0) {
			if(controlInput.GetButtonDown("Left Stick Vertical") && vertical > 0f && jumpsRemaining > 0){
				Jump (jumpHeight * Mathf.Pow(jumpDampening, jumpCount - jumpsRemaining), dt);
				--jumpsRemaining;
			}
		}

		//Debug.Log ("" + horizontal + " " + v + " " + movementForce);
	}

	void OnCollisionEnter2D(Collision2D col) {
		jumpsRemaining = jumpCount;
		GroundedCheck (col.gameObject, true);
	}

	void OnCollisionExit2D(Collision2D col) {
		GroundedCheck (col.gameObject, false);
	}

	private void GroundedCheck(GameObject collidedObject, bool groundedValue) {
		if (collidedObject.CompareTag(groundedTag)) {
			//Debug.Log((groundedValue) ? "Grounded" : "No Longer Grounded");
			grounded = groundedValue;
		}
	}

	private void Jump(float height, float dt) {
		Vector2 v = rigBod.velocity;
		float m = rigBod.mass;
		float g = Physics.gravity.magnitude;
		// 1/2mv^2 = mgh
		// v^2 = 2gh
		// v = sqrt(2gh)
		float v0 = Mathf.Sqrt(2f * g * height);
		// f = ma
		// a = (vf - v0) / dt
		rigBod.velocity += v0 * Vector2.up;
	}
}
