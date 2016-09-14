using UnityEngine;
using System.Collections;

public class BasicMover : MonoBehaviour {

	public float rotationPerSecond = 180;

	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (Vector3.up * rotationPerSecond * Time.deltaTime);
	}
}
