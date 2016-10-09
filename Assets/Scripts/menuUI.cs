using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class menuUI : MonoBehaviour {

    public GameObject[] nameButtons,charButtons,playerModels,networkObjs,netButtons;
    public string playerName;
    public int playerId;
    
	void Start () {
        DontDestroyOnLoad(gameObject);

        for (int k = 0; k < nameButtons.Length; k++)
            nameButtons[k].SetActive(true);
	
	}
	
	// Update is called once per frame
	void Update () {
        {
            //Debug.Log("send name");
            GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
            for (int k = 0; k < Players.Length; k++)
            {
                //if (!Players[k].GetComponent<playerVar>().isLocalPlayer)
                //Players[k].GetComponent<playerVar>().changeName(inputText.text);
                Players[k].GetComponent<playerValues>().changeName(playerName);
                Players[k].GetComponent<playerValues>().changeId(playerId);
            }
        }
	
	}
    public void setName()
    {
        playerName = GameObject.Find("inText").GetComponent<Text>().text.ToString();
        for (int k = 0; k < nameButtons.Length; k++)
            nameButtons[k].SetActive(false);
        for (int k = 0; k < charButtons.Length; k++)
            charButtons[k].SetActive(true);
        
    }
    public void setCharacter(int num)
    {
        playerId = num;
        for (int k = 0; k < charButtons.Length; k++)
            charButtons[k].SetActive(false);
        /*
        for (int k=0;k<networkObjs.Length;k++)
        {
            GameObject obj = (GameObject)Instantiate(networkObjs[k]);
            obj.name = networkObjs[k].name;

            if (obj.name == "GameController")
            {
                obj.GetComponent<playerValues>().playerName = playerName;
                obj.GetComponent<playerValues>().playerId = playerId;
            }
            
        }
        */
        for (int k = 0; k < netButtons.Length; k++)
            netButtons[k].active = true;
    }
    public void hideNetUI()
    {
        for (int k = 0; k < netButtons.Length; k++)
            netButtons[k].SetActive(false);
    }
}
