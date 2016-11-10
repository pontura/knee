using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class galleryInput : MonoBehaviour {

	public Camera _camera;
	public LayerMask _buttonMask;
	public Transform _highlight;
	public ParticleSystem _carSpawnEffect;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
				else if (_buttonHit.transform.name == "drive button")
				{
					SceneManager.LoadScene("track 2");
				}
				else if (_buttonHit.transform.name == "buy button")
				{
					// something here, an effect...
				}
				else
				{
					_highlight.position = _buttonHit.transform.position;
					_highlight.rotation = _buttonHit.transform.rotation;
					_carSpawnEffect.Emit(120);
				}
			}
		}

	}
}
