using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private float hor;
	private float vert;
	private int facingRight = 1;

	public GameObject fist;
	public float punchTime = .1f;
	private bool punching = false;
	public BoxCollider2D punchColl;
	public MeshRenderer punchMesh;

	// Use this for initialization
	void Start () {
		fist = transform.Find("Fist").gameObject;
		punchColl = fist.gameObject.GetComponentInChildren <BoxCollider2D>();
		punchMesh = fist.gameObject.GetComponentInChildren <MeshRenderer> ();
		punchMesh.enabled = false;
		punchColl.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Player Movement/////////////////////////
		hor = Input.GetAxis ("Horizontal");
		vert = Input.GetAxis ("Vertical");

		transform.Translate (Vector2.right * hor/13f * facingRight);
		transform.Translate (Vector2.up * vert/19f);
		//////////////////////////////////////////

		//Punching////////////////////////////////
		if (Input.GetKeyDown ("space") && !punching) {
			punching = true;
			punchColl.enabled = true;
			punchMesh.enabled = true;
			Debug.Log ("Start");
		}
		if (punching && punchTime > 0) {
			punchTime -= Time.deltaTime;
		} 
		else if (punching && punchTime <= 0 ){
			punchTime = .1f;
			punchColl.enabled = false;
			punchMesh.enabled = false;
			punching = false;
			Debug.Log("Stop");
		}
			//Punch ();
		//////////////////////////////////////////

		//Facing left and right////////////////
		if (hor >= .5f && facingRight == -1)
			Flip ();
		else if (hor < -.5f && facingRight == 1)
			Flip();
		///////////////////////////////////////

	}
	
	void Flip(){
		// Switch the way the player is labelled as facing
		facingRight = facingRight * -1;
		//Debug.Log ("Facing is: " + facingRight);
		
		transform.Rotate (0f, 180f, 0f);
	}


}
