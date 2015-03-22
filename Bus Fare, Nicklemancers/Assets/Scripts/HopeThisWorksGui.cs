using UnityEngine;
using System.Collections;

public class HopeThisWorksGui : MonoBehaviour {

	private PlayerController playerController;
	//private GUIText gui;
	// Use this for initialization
	void Start () {
		playerController = GameObject.Find("Player").GetComponent<PlayerController> ();
		//gui.text = "It's $5.00 to ride the bus, if you have the money press 'B', for Bus.";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.tag == "Player" && playerController.coins > 500){
			
			if (Input.GetKeyDown(KeyCode.R)){
				//gui.text = "You Have Stolen:" + playerController.coins + "cents";
				Application.LoadLevel("GameOver");
			}
}
}
}


