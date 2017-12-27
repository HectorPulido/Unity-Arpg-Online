using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChatScript : NetworkBehaviour
{
	Text text;
	InputField inputField;

	void Start () 
	{
        Transform s = CustomNetworkManager.singleton.transform;

        text = GameObject.Find ("TxtTexto").GetComponent <Text>(); 
		inputField = GameObject.Find ("input").GetComponent<InputField> ();
	}

	void Update () 
	{
		if (!isLocalPlayer)
			return;
        
		if(Input.GetKeyDown(KeyCode.Return))
		{
			if(inputField.text != "")
			{
				string message = inputField.text;
				inputField.text = "";

				CmdSend (CustomNetworkManager.playerName +": "+ message);
			}
		}
	}

	[Command]
	void CmdSend(string message)
	{
		RpcRecive (message);
	}

	[ClientRpc]
	public void RpcRecive(string message)
	{
		text.text += ">>" + message + "\n";
	}
}
