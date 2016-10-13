using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class VRRenderScale : MonoBehaviour {

	public float renderScale = 1.25f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		VRSettings.renderScale = renderScale;
	}
}
