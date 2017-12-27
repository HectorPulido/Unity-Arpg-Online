using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkTransform : NetworkBehaviour
{
	[SyncVar]
	Vector3 SyncPos;
	[SyncVar]
	float SyncRotY;

	public Transform myTransform;
	public float LerpRate = 15f;

	void FixedUpdate()
	{
		SendTransform ();
		Lerp ();
	}

	void Lerp()
	{
		if (!isLocalPlayer) {
			myTransform.position = Vector3.Lerp (myTransform.position, SyncPos, Time.deltaTime * LerpRate);
			myTransform.eulerAngles = Vector3.Lerp (myTransform.eulerAngles, new Vector3(0,SyncRotY,0), Time.deltaTime * LerpRate);		
		}
	}

	[Command]
	void CmdTransformToServer(Vector3 pos, float rot)
	{
		SyncPos = pos;
		SyncRotY = rot;
	}

	[ClientCallback]
	void SendTransform()
	{
		if(isLocalPlayer)
			CmdTransformToServer (transform.position, transform.eulerAngles.y);
	}

}
