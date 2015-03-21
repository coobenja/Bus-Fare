using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private float hor;
	private float vert;
	private int facingRight = 1;

	public GameObject fist;

	// Use this for initialization
	void Start () {
		fist = transform.Find("Fist").gameObject;	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		hor = Input.GetAxis ("Horizontal");
		if (hor > .05)
			hor = 1;
		else if (hor < -.05)
			hor = -1;
		else
			hor = 0;


		vert = Input.GetAxis ("Vertical");

		Debug.Log (hor);

		transform.Translate (Vector2.right * hor/13f * facingRight);
		transform.Translate (Vector2.up * vert/19f);

		//Facing left and right////////////////
		if (hor >= .05f && facingRight == -1)
			Flip ();
		else if (hor < .05f && facingRight == 1)
			Flip();
		///////////////////////////////////////

	}

	void Punch(){

	}

	void Flip(){
		// Switch the way the player is labelled as facing
		facingRight = facingRight * -1;
		
		transform.Rotate (0f, 180f, 0f);
	}


}
