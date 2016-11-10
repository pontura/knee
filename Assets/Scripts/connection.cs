using UnityEngine;
using System.Collections;

public class connection : MonoBehaviour {

	public bool _atSocket;
	public chunk _parentChunkScript;
	public string _socketKey;

	 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "socket" && other.name == _socketKey)
		{
			//_atSocket = true;
			//transform.GetComponent<Renderer>().enabled = true;
			_parentChunkScript._onSocket = true;
			_parentChunkScript.ChangeOutlineColor(Color.green, 1);
			_parentChunkScript._socketTransform = other.transform;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "socket" && other.name == _socketKey)
		{
			//_atSocket = false;
			//transform.GetComponent<Renderer>().enabled = false;
			_parentChunkScript._onSocket = false;
			_parentChunkScript.ChangeOutlineColor(Color.magenta, 1);

		}
	}
		
}
