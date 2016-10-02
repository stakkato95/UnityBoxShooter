using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour
{
	public enum State
	{
		Moving,
		Staying
	}

	public int secondsOfStay = 2;
	float stayFinishTime;

	State CurrentState = State.Staying;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (Time.time);
		if (stayFinishTime >= Time.time) {
			CurrentState = State.Moving;
		}

		if ((transform.position.z <= 0 || 35 <= transform.position.z) && CurrentState == State.Moving) {
			CurrentState = State.Staying;

			stayFinishTime = Time.time + secondsOfStay;
		}

		if (CurrentState == State.Moving) {
			transform.Translate (35 <= transform.position.z ? new Vector3 (0, 0, -0.05f) : new Vector3 (0, 0, 0.05f));
		}
	}
}
