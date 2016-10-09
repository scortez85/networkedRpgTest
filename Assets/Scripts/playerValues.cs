using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class playerValues : NetworkBehaviour {
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int playerId;
    public int numOfPlayers;
    public GameObject[] playerModels;
    //public GameObject player;
    private NetworkManager netManager;

    [RPC]
    public void RpcSyncPlayerId(int num)
    {
        playerId = num;
    }
    [RPC]
    public void RpcSyncName(string name)
    {
        playerName = "empty";
        playerName = name;
    }
    [Command]
    public void CmdName(string name)
    {
        playerName = name;
    }
    [Command]
    public void CmdId(int Id)
    {
        playerId = Id;
    }
    public void changeName(string name)
    {
        if (!isLocalPlayer)
            CmdName(name);
        RpcSyncName(name);
    }
    public void changeId(int ID)
    {
        if (!isLocalPlayer)
            CmdId(ID);
        RpcSyncPlayerId(ID);
    }
    void Start () {
        DontDestroyOnLoad(gameObject);
        //snag values from previous ui && spawn player model
        if (isLocalPlayer)
        {
            playerName = GameObject.Find("menuUI").GetComponent<menuUI>().playerName;
            playerId = GameObject.Find("menuUI").GetComponent<menuUI>().playerId;

            for (int k=0;k<playerModels.Length;k++)
            {
                playerModels[k].SetActive(false);
            }
            playerModels[playerId].SetActive(true);
            playerModels[playerId].transform.parent = gameObject.transform;
        }
        if (isLocalPlayer)
        {
            CmdName(GameObject.Find("menuUI").GetComponent<menuUI>().playerName);
            CmdId(playerId);
        }
        RpcSyncName(GameObject.Find("menuUI").GetComponent<menuUI>().playerName);
        RpcSyncPlayerId(playerId);

    }
	
	// Update is called once per frame
	void Update () {
        //turn off temp models and sync models
        for (int k=0;k<playerModels.Length;k++)
        {
            playerModels[k].SetActive(false);
        }
        playerModels[playerId].SetActive(true);

        //keep all clients Nsync
        RpcSyncName(playerName);
        RpcSyncPlayerId(playerId);

        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerController");
        if (numOfPlayers < players.Length)
        {
            RpcSyncName(playerName);
            numOfPlayers++;
            Debug.Log("PlayerLog");
        }


    }
    void OnGUI()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerController");
        GUI.Label(new Rect(10, 10, 128, 32), "Players in Room");
        for (int k =0;k<players.Length;k++)
            GUI.Label(new Rect(10,48 + (k * 32),256,32),players[k].GetComponent<playerValues>().playerName + " choice: " + players[k].GetComponent<playerValues>().playerId.ToString());
    }
}
