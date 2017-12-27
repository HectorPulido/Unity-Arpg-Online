using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerStats : Stats 
{
	[SyncVar(hook = "NameChanged")]
    public string charName;
	public Text nameText;

	void Start()
	{
		nameText.text = charName;
	}
	public override void OnStartLocalPlayer()
	{
		CmdChangeName (CustomNetworkManager.playerName);
	}
	void NameChanged (string Nombre)
	{
		nameText.text = Nombre;
	}
	[Command]
	public void CmdChangeName(string _Nombre)
	{
		charName = _Nombre;
	}

}
