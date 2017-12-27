using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class CustomNetworkManager : NetworkManager 
{
	public Button connect;
	public Button startHost;

	public InputField ip;
	public InputField port;

	public GameObject offlineCanvas;
	public GameObject onlineCanvas;

    public static string playerName = "";
	public InputField nameField;

	void Start () 
	{
		onlineCanvas.SetActive (false);

		connect.onClick.RemoveAllListeners ();
		startHost.onClick.RemoveAllListeners ();

		connect.onClick.AddListener (startClient);
		startHost.onClick.AddListener(ServerStart);

        for (int i = 0; i < prefabs.Length; i++)
        {
            ClientScene.RegisterPrefab(prefabs[i]);
        }        
	}
	bool SetPort()
	{
		if (port.text != "") {
			int _puerto;
			bool b = int.TryParse (port.text, out _puerto);
			NetworkManager.singleton.networkPort = _puerto;
			return b;
		} else {
			NetworkManager.singleton.networkPort = 5556;
			return true;
		}

	}
	void SetIp()
	{
		if (ip.text != "") {
			NetworkManager.singleton.networkAddress = ip.text;
		} else {
			NetworkManager.singleton.networkAddress = "localhost";
		}
	}
	public void startClient()
	{
		if (SetPort ()) 
		{
			SetIp ();
			NetworkManager.singleton.StartClient ();		
		}
	}
	public void ServerStart()
	{
		if (SetPort ()) 
		{
			NetworkManager.singleton.StartHost ();		
		}
	}
	void OnInnit()
	{
		offlineCanvas.SetActive (false);
		onlineCanvas.SetActive (true);

		if (nameField.text != "")
			playerName = nameField.text;
		else
			playerName = Time.deltaTime.ToString();
	}
	public override void OnStartHost()
	{
		base.OnStartHost ();
		OnInnit ();
	}
	public override void OnStartClient(NetworkClient client)
	{
		base.OnStartClient (client);
		OnInnit ();
    }


    ///Eleccion de personaje
    ///

    public GameObject[] prefabs;
    int prefabId;

    public void ChangeId(int id) { prefabId = id; }
    public class EleccionDeClase : MessageBase { public int idClase; }

    //SERVIDOR
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        //base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
        EleccionDeClase ec = extraMessageReader.ReadMessage<EleccionDeClase>();
        int prefabAInstanciar = ec.idClase;
        GameObject go = Instantiate(prefabs[prefabAInstanciar]) as GameObject;
        NetworkServer.AddPlayerForConnection(conn, go, playerControllerId);
    }

    //CLIENTE
    public override void OnClientConnect(NetworkConnection conn)
    {
        //base.OnClientConnect(conn);
        EleccionDeClase ec = new EleccionDeClase();
        ec.idClase = prefabId;
        ClientScene.AddPlayer(conn, 0, ec);
    }
}
