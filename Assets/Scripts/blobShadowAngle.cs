using UnityEngine;
using System.Collections;

public class blobShadowAngle : MonoBehaviour {

	public Quaternion _myRotation;

	// Use this for initialization
	void Start () {
		_myRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = _myRotation;
	}
}
