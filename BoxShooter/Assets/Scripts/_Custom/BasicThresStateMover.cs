using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicThresStateMover : MonoBehaviour
{

	public enum Motion
	{
		RotationHorizontal,
		RotationVertical,
		HorizontalLeftRight,
		HorizontalBackForward,
		Vertical
	}

	public List<Motion> modesOfMotion = new List<Motion>
	{
		Motion.HorizontalBackForward,	
		Motion.RotationVertical,	
	};

	public float rotationDegrees = 180;
	public float motionMagnitude = 0.1f;

	void Update ()
	{
		foreach (var motion in modesOfMotion) {
			switch (motion) {
			case Motion.RotationHorizontal:
			case Motion.RotationVertical:
				{
					var rotationVector = motion == Motion.RotationVertical ? 
						Vector3.up : 
						Vector3.forward;

					gameObject.transform.Rotate (rotationVector * rotationDegrees * Time.deltaTime);
				}
				break;
			case Motion.HorizontalLeftRight: 
			case Motion.HorizontalBackForward:
				{
					var horizontalVector = motion == Motion.HorizontalBackForward ? 
						Vector3.left : 
						Vector3.right;
					gameObject.transform.Translate (horizontalVector * Mathf.Cos (Time.timeSinceLevelLoad) * motionMagnitude);
				}
				break;
			case Motion.Vertical: 
				{
					gameObject.transform.Translate (Vector3.up * Mathf.Cos (Time.timeSinceLevelLoad) * motionMagnitude);
				}
				break;
			}
		}
	}
}
