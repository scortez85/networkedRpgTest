using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class playerMove : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;

        float vert = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");

        transform.Translate(0, 0, vert * 5 * Time.deltaTime);
        transform.Rotate(0, horiz * 150 * Time.deltaTime, 0);
	
	}
}
