using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

	Transform Cam;
	void Start () 
	{
		Cam = Camera.main.transform;	
	}

	void Update () 
	{
		
		transform.LookAt (Cam.position);
	}
}
