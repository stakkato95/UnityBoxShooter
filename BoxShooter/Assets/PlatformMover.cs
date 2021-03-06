﻿using System.Collections.Generic;

using UnityEngine;

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

	public enum Axis { X, Y, Z }

	const int ResetedTimer = -1;

	public float secondsOfStay = 2;
	public int platformSpeedMetersPerSecond = 1;
	public Axis axisOfMoving = Axis.Z;
	public float coordinateA = 35;
	public float coordinateB = 0;

	float stayFinishTime;
	State CurrentState;

	Vector3 Size;

	Dictionary<Axis, Vector3> axisToDirections = new Dictionary<Axis, Vector3>()
	{
		{Axis.X, Vector3.right},
		{Axis.Y, Vector3.up},
		{Axis.Z, Vector3.forward},
	};

	// Use this for initialization
	void Start ()
	{
		stayFinishTime = ResetedTimer;

		Size = GetComponent<BoxCollider> ().size;

		CurrentState = GetCurrentCenter(false) == GetTerminalPoint(false) ? State.MovingForth : State.MovingBack;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var stayTimeElapsed = stayFinishTime == ResetedTimer ? false : stayFinishTime < Time.time;

		if (stayTimeElapsed) {
			CurrentState = CurrentState == State.StayingToMoveBack ? State.StartMovingBack : State.StartMovingForth;
		}

		if (IsOutOfMovementArea()) {
			CurrentState = CurrentState == State.MovingForth ? State.StayingToMoveBack : State.StayingToMoveForth;

			stayFinishTime = Time.time + secondsOfStay;
		}

		Move ();
	}

	void Move ()
	{
		if (CurrentState == State.MovingForth || CurrentState == State.MovingBack ||
		    CurrentState == State.StartMovingForth || CurrentState == State.StartMovingBack) {
			transform.Translate (CurrentState == State.MovingBack || CurrentState == State.StartMovingBack ? 
				-GetMovementVector() * Time.deltaTime * platformSpeedMetersPerSecond : 
				GetMovementVector() * Time.deltaTime * platformSpeedMetersPerSecond);

			if (CurrentState == State.StartMovingForth || CurrentState == State.StartMovingBack) {
				CurrentState = CurrentState == State.StartMovingBack ? State.MovingBack : State.MovingForth;
				stayFinishTime = ResetedTimer;
			}
		}
	}

	float GetTargetAxisCurrentPosition()
	{
		return axisOfMoving == Axis.X ? 
			transform.position.x : 
			(axisOfMoving == Axis.Y ? 
				transform.position.y : 
				transform.position.z);
	}

	float GetTargetAxisCurrentSize()
	{
		return axisOfMoving == Axis.X ? Size.x : (axisOfMoving == Axis.Y ? Size.y : Size.z);
	}

	bool IsOutOfMovementArea()
	{
		var positionInferiorLoverLimit = GetCurrentCenter(false) <= GetTerminalPoint(false) && CurrentState == State.MovingBack;
		var positionAboveToplimit = GetTerminalPoint(true) <= GetCurrentCenter(true) && CurrentState == State.MovingForth;

		return (positionInferiorLoverLimit || positionAboveToplimit);
	}

	float GetTerminalPoint(bool isTop)
	{
		return isTop ? 
			Mathf.Max (coordinateA, coordinateB) : 
			Mathf.Min (coordinateA, coordinateB);
	}

	float GetCurrentCenter(bool isForBiggerPosition)
	{
		return isForBiggerPosition ? 
			GetTargetAxisCurrentPosition () :
			GetTargetAxisCurrentPosition (); 
	}

	Vector3 GetMovementVector()
	{
		return axisToDirections [axisOfMoving];
	}
}
