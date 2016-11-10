using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class track2inputManager : MonoBehaviour {

	public GameObject _carCamera;
	public GameObject _hoverCamera;
	private Vector3 _insideCarPos = new Vector3();
	private Vector3 _outsideCarPos = new Vector3();
	public bool _cameraIsInside;
	public Camera _camera;
	public LayerMask _buttonMask;
	public bool _pcBuild;



	// Use this for initialization
	void Start () {
		if (_pcBuild == false) {
			_hoverCamera.SetActive(true);
			_carCamera.SetActive(false);
			_cameraIsInside = false;	
		}



		_insideCarPos.x = 0;
		_insideCarPos.y = 0.965f;
		_insideCarPos.z = 0.304f;

		_outsideCarPos.x = 0;
		_outsideCarPos.y = 5.83f;
		_outsideCarPos.z = -10;



	}
	


	void Update () 
	{
		Ray _buttonRay = _camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		RaycastHit _buttonHit;
		if (Physics.Raycast(_buttonRay, out _buttonHit, 1000, _buttonMask))
		{
			if (Input.GetButtonDown("Fire1"))
			{
				if (_buttonHit.transform.name == "build button")
				{
					SceneManager.LoadScene("build car");
				}
				else if (_buttonHit.transform.name == "shop button")
				{
					SceneManager.LoadScene("Gallery");				
				}
			}
		}

		/*
		// change Camera position
		if (Input.GetKeyDown("c"))
		{
			if (_cameraIsInside)
			{
				_hoverCamera.SetActive(true);
				_carCamera.SetActive(false);
				_cameraIsInside = false;
			} else {
				_hoverCamera.SetActive(false);
				_carCamera.SetActive(true);
				_cameraIsInside = true;
			}
		}
		*/

		/*
		// implement this after Mac build
		// it's just to test in-car camera with VR mode
		// change input from mouse/tap to controller button at some time
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (_cameraIsInside)
			{
				_cameraIsInside = false;
				_camera.transform.position = new Vector3(_outsideCarPos.x, _outsideCarPos.y, _outsideCarPos.z);
			} else {
				_cameraIsInside = true;
				_camera.transform.position = new Vector3(_insideCarPos.x, _insideCarPos.y, _insideCarPos.z);
			}
		}
		*/
	}
}
