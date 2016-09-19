using UnityEngine;
using System.Collections;

public class BasicMover : MonoBehaviour {

	public float rotationPerSecond = 180;
	public float motionUpAndDown = 0.1f;

	public bool doSpin = true;
	public bool doMotion = false;

	// Update is called once per frame
	void Update () {
		if (doSpin) {
			//rotate arount the up axis
			gameObject.transform.Rotate (Vector3.up * rotationPerSecond * Time.deltaTime);
		}

		if (doMotion) {
			//move up and down
			gameObject.transform.Translate (Vector3.down * Mathf.Cos (Time.timeSinceLevelLoad) * motionUpAndDown);
		}
	}
}
