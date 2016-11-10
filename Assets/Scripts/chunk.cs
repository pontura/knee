using UnityEngine;
using System.Collections;

public class chunk : MonoBehaviour {

	public bool _onSocket = false;
	public Transform _socketTransform;
	public float _myHeight;
	public Vector3 _tempPos = new Vector3();

	// Use this for initialization
	void Start () {
		_myHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 _tempPos = (transform.position.x, _myHeight, transform.position.z);

		_tempPos = new Vector3 (transform.position.x , _myHeight, transform.position.z);
		//transform.position = _tempPos;
	}

	/*
	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "socket")
		{
			_onSocket = true;
			_socketTransform = collision.gameObject.transform;
			//transform.GetComponent<Renderer>().enabled = true;
			//_parentChunkScript._onSocket = true;
			ChangeOutlineColor(Color.green);
		}
	}

	void OnCollisionExit (Collision collision)
	{
		if (collision.gameObject.tag == "socket")
		{
			_onSocket = false;
			//transform.GetComponent<Renderer>().enabled = false;
			//_parentChunkScript._onSocket = false;
			ChangeOutlineColor(Color.blue);

		}
	}
	*/

	public void ChangeOutlineColor (Color newColor, float newPower)
	{
		Renderer[] _objRenderers = gameObject.GetComponentsInChildren<Renderer>();

		foreach (Renderer _objRenderer in _objRenderers)
		{
			//_objRenderer.material.SetColor("_OutlineColor", newColor);
			_objRenderer.material.SetColor("_RimColor", newColor);
			_objRenderer.material.SetFloat("_RimPower", newPower);
		}
	}

}
