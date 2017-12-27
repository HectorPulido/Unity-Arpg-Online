using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonControllerCustom : NetworkBehaviour {

	Rigidbody _rb;
	Animator _anim;
	Transform _Cam;
	FreeLookCam _FreeCam;

	public float Velocity;

	// Use this for initialization
	void Awake () {
		_Cam = Camera.main.transform;
		_rb = GetComponent<Rigidbody> ();
		_anim = GetComponent<Animator> ();

		_FreeCam = GameObject.FindObjectOfType<FreeLookCam> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isLocalPlayer)
			return;

		if (_FreeCam.Target != transform)
			_FreeCam.SetTarget(transform);

		if (_anim.GetCurrentAnimatorStateInfo (0).IsName ("Locomotion")) 
		{
			float vertical = Input.GetAxis ("Vertical");

			if (vertical > 0) {			
				_anim.SetFloat ("Velocidad", vertical);
				transform.eulerAngles = new Vector3 (transform.eulerAngles.x, _Cam.eulerAngles.y, transform.eulerAngles.z);
				_rb.velocity = transform.forward * Velocity * vertical * (Input.GetKey (KeyCode.LeftShift) ? 2 : 1);
			}
			if (Input.GetMouseButtonDown (0)) {
				_anim.SetTrigger ("Ataque");
                RaycastHit ht;
                Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(r, out ht, 999f))
                {
                    if (ht.collider.GetComponent<Stats>())
                    {
                        CmdChangeHealth(10, ht.collider.gameObject);                       
                    }
                }
			}

        }
		
	}
    [Command]
    void CmdChangeHealth(int cantidad, GameObject go)
    {
        go.SendMessage("ChangeHealth", cantidad);
    }
}
