using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour
{
	public enum State
	{
		MovingForth,
		MovingBack,
		StartMovingForth,
		StartMovingBack,
		StayingToMoveForth,
		StayingToMoveBack
	}

	const int ResetedTimer = -1;

	public int secondsOfStay = 2;

	float stayFinishTime;
	State CurrentState;
	Vector3 Size;

	// Use this for initialization
	void Start ()
	{
		CurrentState = State.MovingForth;
		stayFinishTime = ResetedTimer;
		Size = GetComponent<BoxCollider> ().size;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (Time.time);
		var stayTimeElapsed = stayFinishTime == ResetedTimer ? false : stayFinishTime < Time.time;

		if (stayTimeElapsed) {
			CurrentState = CurrentState == State.StayingToMoveBack ? State.StartMovingBack : State.StartMovingForth;
		}

		if ((35 <= transform.position.z + Size.z && CurrentState == State.MovingForth) ||
			(transform.position.z - Size.z <= 0 && CurrentState == State.MovingBack)) {
			CurrentState = CurrentState == State.MovingForth ? State.StayingToMoveBack : State.StayingToMoveForth;

			stayFinishTime = Time.time + secondsOfStay;
		}

		Move ();
	}

	void Move ()
	{
		if (CurrentState == State.MovingForth || CurrentState == State.MovingBack ||
		    CurrentState == State.StartMovingForth || CurrentState == State.StartMovingBack) {
			transform.Translate (CurrentState == State.MovingBack || CurrentState == State.StartMovingBack ? new Vector3 (0, 0, -0.1f) : new Vector3 (0, 0, 0.1f));

			if (CurrentState == State.StartMovingForth || CurrentState == State.StartMovingBack) {
				CurrentState = CurrentState == State.StartMovingBack ? State.MovingBack : State.MovingForth;
				stayFinishTime = ResetedTimer;
			}
		}
	}
}
