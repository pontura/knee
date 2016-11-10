using UnityEngine;
using System.Collections;

public class eyeCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision collision) {
		Debug.Log("eye collider collsionstay");
		foreach (ContactPoint contact in collision.contacts) {
			Debug.Log("foreach contact point");
			Debug.Log("contact point in collision = " + contact.otherCollider.name);
			if (contact.otherCollider.name == "Floor")
			{
				Debug.Log("floor pos = " + contact.point);
			}
		}
	}
}
