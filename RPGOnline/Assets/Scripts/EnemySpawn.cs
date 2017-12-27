using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawn : NetworkBehaviour {

	public GameObject Prefab;
	public float timeSpawn;
	public int enemyCount;


	void Start () 
	{
		if(isServer)
			InvokeRepeating ("Spawn", 0, timeSpawn);	
	}

	void Spawn()
	{
		if (GameObject.FindGameObjectsWithTag ("Enemigo").Length <= enemyCount) {
			GameObject go = Instantiate (Prefab, transform.position, Quaternion.identity) as GameObject;
			NetworkServer.Spawn (go);
		}
	}

}
