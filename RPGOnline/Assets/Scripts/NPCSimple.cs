using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NPCSimple : MonoBehaviour {


	public string textBlock;
	Flowchart fc;

	void Start()
	{
		fc = GameObject.FindObjectOfType<Flowchart> ();
	}


	void OnMouseDown()
	{
		fc.ExecuteBlock (textBlock);

	}
}
