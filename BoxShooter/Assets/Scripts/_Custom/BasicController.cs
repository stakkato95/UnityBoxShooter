using System;
using System.Collections;

using UnityEngine;
using Ev = UnityEngine.Event;

public class BasicController : MonoBehaviour {

	public float movementSpeed = 4;
	public float gravity = 0.5f;

	CharacterController CharController;

	void Start() {
		CharController = gameObject.GetComponent<CharacterController> ();	
	}

	void Update () {
		var vert = Input.GetAxis ("Vertical");
		Debug.Log (vert);
		var movementZ = vert * Vector3.forward * movementSpeed * Time.deltaTime;

		var hor = Input.GetAxis ("Horizontal");
		Debug.Log (hor);
		var movementX = hor * Vector3.right * movementSpeed * Time.deltaTime;

		var movement = transform.TransformDirection (movementZ + movementX);

		movement.y -= gravity * Time.deltaTime;

		CharController.Move (movement);
	}
}
