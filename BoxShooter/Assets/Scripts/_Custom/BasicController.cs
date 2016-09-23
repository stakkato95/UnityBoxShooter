using UnityEngine;
using System.Collections;

public class BasicController : MonoBehaviour {

	public float movementSpeed = 4;
	public float gravity = 0.5f;

	CharacterController CharController;

	void Start() {
		CharController = gameObject.GetComponent<CharacterController> ();	
	}

	// Update is called once per frame
	void Update () {
		var movementZ = Input.GetAxis ("Vertical") * Vector3.forward * movementSpeed * Time.deltaTime;

		var movementX = Input.GetAxis ("Horizontal") * Vector3.right * movementSpeed * Time.deltaTime;

		var movement = transform.TransformDirection (movementZ + movementX);

		movement.y -= gravity * Time.deltaTime;

		CharController.Move (movement);
	}
}
