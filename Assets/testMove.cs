using UnityEngine;
using System.Collections;

public class testMove : MonoBehaviour {

	public float _startY;

	// Use this for initialization
	void Start () {
		_startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, _startY, transform.position.z);
	}
}
