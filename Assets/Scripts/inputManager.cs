using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;			// so I can use Text class


//using UnityEngine;
using System;
//using System.Collections;
using System.Collections.Generic;

public class inputManager : MonoBehaviour {

	public Transform _heldTransform;
	public chunk _heldTransScript;
	public float _rotSpeed;
	public Camera _camera;
	public Transform _pivot;
	public Transform _floor;
	public Transform _carParent;
	public Color _noOutline = new Color(1,1,1,0);
	public Color myColor = Color.blue;
	public Text _debugText;  		// KEEP!  needed for mapping new controllers

	public LayerMask _floorMask;
	public LayerMask _carPartMask;
	public LayerMask _buttonMask;
	public Vector3 _floorRaycastPoint = new Vector3();
	public Vector3 _tempPos = new Vector3();
	public RaycastHit _carRaycastHit;

	public Transform _blueprint;
	public bool _partHitByRay;



	void Start () 
	{
	}
	


	void Update () 
	{
		CheckInput();
		FloorRaycast();
		CarPartRaycast();
		ButtonRaycast();
	}



	void ButtonRaycast ()
	{
		Ray buttonRay = _camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		RaycastHit buttonHit;
		if (Physics.Raycast(buttonRay, out buttonHit, 1000, _buttonMask))
		{
			if (Input.GetButtonDown("Fire1"))
			{
				if (buttonHit.transform.name == "Button - Drive")
				{
					SceneManager.LoadScene("track 2");
				}
				else if (buttonHit.transform.name == "Button - Shop")
				{
					SceneManager.LoadScene("Gallery");
				}
				else if (buttonHit.transform.name == "Button - Rotate")
				{
					_blueprint.Rotate(Vector3.forward * -90);
				}
			}
		} 
	}

	void FloorRaycast()
	{
		Ray floorRay = _camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		RaycastHit floorHit;
		if (Physics.Raycast(floorRay, out floorHit, 1000, _floorMask))
		{
			_floorRaycastPoint = floorHit.point;
		}

		if (_heldTransform != null)
		{
			_tempPos = new Vector3(_floorRaycastPoint.x, _heldTransform.position.y, _floorRaycastPoint.z);
			_heldTransform.position = _tempPos;
		}
	} // end of FloorRaycast()


	void CarPartRaycast()
	{
		Ray ray = _camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		if (Physics.Raycast(ray, out _carRaycastHit, 1000, _carPartMask))
		{
			_partHitByRay = true;
		} 
		else
		{
			_partHitByRay = false;
		}

		// end of Raycast
	}

	public void ChangeOutlineColor (Color newColor, float newPower)
	{
		Renderer[] _objRenderers = _heldTransform.GetComponentsInChildren<Renderer>();
		foreach (Renderer _objRenderer in _objRenderers)
		{
			//_objRenderer.material.SetColor("_OutlineColor", newColor);
			_objRenderer.material.SetColor("_RimColor", newColor);
			_objRenderer.material.SetFloat("_RimPower", newPower);

		}
	}

	void CheckInput ()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			// if player is holding something
			if (_heldTransform != null)
			{
				// if object is on a socket, set it to socket rot/pos and make child
				if (_heldTransform.GetComponent<chunk>()._onSocket == true)
				{
					_heldTransform.position = _heldTransScript._socketTransform.position;
					_heldTransform.rotation = _heldTransScript._socketTransform.rotation;
					_heldTransform.parent = _carParent.transform;
					_heldTransScript._socketTransform.gameObject.SetActive(false);							// so that a 2nd chunk cannot be snapped to this socket
				}
				// if object is NOT on a socket, drop it
				else 
				{
					_heldTransform.parent = null;
				}
				ChangeOutlineColor(Color.white, 1000);														// remove outline from chunk
				_heldTransform = null;


			// if nothing is held, pickup the object under reticle
			} else if (_partHitByRay){
				_heldTransform = _carRaycastHit.transform;													// identify the chunk
				Debug.Log("heldtransform = " + _heldTransform);
				_heldTransScript = _heldTransform.GetComponent<chunk>();			
				_heldTransform.GetComponent<Rigidbody>().isKinematic = true;								// stop gravity and collisions
				ChangeOutlineColor(Color.magenta, 1);														// blue outline on chunk
				_heldTransScript._socketTransform.gameObject.SetActive(true);								// disabled earlier so that 2nd chunk could not go here, too
//
			} // end of nothing is held
		} // end of Input "Fire1"


		// if something is held, check for inputs and rotate
		if (_heldTransform != null)
		{
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");

			// check WASD keys


			if (h > 0)
			{
				//_heldTransform.Rotate(Vector3.right * _rotSpeed, Space.World);			// we don't want this angle of rotation any more
				_heldTransform.Translate(Vector3.up * 0.5f, Space.World);
			}

			if (h < 0)
			{
				//_heldTransform.Rotate(Vector3.right * -_rotSpeed, Space.World);			// we don't want this angle of rotation any more
				_heldTransform.Translate(Vector3.forward * -0.5f, Space.World);
			}


			if (v > 0)																		// used to be (h < 0); rotated for controller
			{
				_heldTransform.Rotate(Vector3.up * _rotSpeed, Space.World);
			}

			if (v < 0)
			{
				_heldTransform.Rotate(Vector3.up * -_rotSpeed, Space.World);
			}

			/*
			// check mousewheel
			// removed because we don't want the height of any piece to change
			var d = Input.GetAxis("Mouse ScrollWheel");
			if (d > 0)
			{
				_heldTransform.Translate(Vector3.forward * 0.5f, _pivot.transform);
			}
			if (d < 0)
			{
				_heldTransform.Translate(Vector3.forward * -0.5f, _pivot.transform);
			}
			*/

		} // end of if something is held
			


		// 
		// show input text on screen (debug)
		// KEEP this code to test future controllers!!!
		//

		foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(kcode))
			{
				Debug.Log("KeyCode down: " + kcode);
				_debugText.text = kcode.ToString();
			}
		}

		if (Input.GetAxis("Axis3") > 0)
		{
			_debugText.text = "Axis3";
		}
		if (Input.GetAxis("Axis5") > 0)
		{
			_debugText.text = "Axis5";
		}
		if (Input.GetAxis("Axis6") > 0)
		{
			_debugText.text = "Axis6";
		}
		if (Input.GetAxis("Axis7") > 0)
		{
			_debugText.text = "Axis7";
		}
		if (Input.GetAxis("Axis8") > 0)
		{
			_debugText.text = "Axis8";
		}
		if (Input.GetAxis("Axis9") > 0)
		{
			_debugText.text = "Axis9";
		}
		if (Input.GetAxis("Axis10") > 0)
		{
			_debugText.text = "Axis10";
		}
		//
		// end of debug text
		// KEEP



	} // end of CheckInput
}