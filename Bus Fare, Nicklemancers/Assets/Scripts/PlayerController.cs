using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private float hor;
	private float vert;
	public int facingRight = 1;

	public GameObject fist;
	public GameObject foot;
	public float punchTime = .1f;
	public float comboBuffer = .5f;
	public int comboCounter = 0;

	private int money = 0;

	private bool punching = false;
	public BoxCollider2D punchColl;
	public MeshRenderer punchMesh;
	public BoxCollider2D kickColl;
	public MeshRenderer kickMesh;
	public ParticleSystem kickEffect;
	public ParticleSystem punchEffect;


	// Use this for initialization
	void Start () {
		fist = transform.Find("Fist").gameObject;
		foot = transform.Find("Foot").gameObject;

		kickEffect = foot.transform.Find ("skillAttack").GetComponent<ParticleSystem> ();
		punchEffect = fist.transform.Find ("skillAttackFist").GetComponent<ParticleSystem> ();

		punchColl = fist.gameObject.GetComponentInChildren <BoxCollider2D>();
		punchMesh = fist.gameObject.GetComponentInChildren <MeshRenderer> ();
		punchMesh.enabled = false;
		punchColl.enabled = false;

		kickColl = foot.gameObject.GetComponentInChildren <BoxCollider2D>();
		kickMesh = foot.gameObject.GetComponentInChildren <MeshRenderer> ();
		kickMesh.enabled = false;
		kickColl.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Ignore Other Character Collisions
		Physics2D.IgnoreLayerCollision (8,8);

		//Player Movement/////////////////////////
		hor = Input.GetAxis ("Horizontal");
		vert = Input.GetAxis ("Vertical");

		if (punchTime <= .3f) {
			transform.Translate (Vector2.right * hor/13f * facingRight);
			transform.Translate (Vector2.up * vert/13f);
		}
		//////////////////////////////////////////

		//Punching + Kicking//////////////////////////
		//Start Punch
		if (Input.GetKeyDown ("space") && !punching && comboCounter != 2) {
			punching = true;
			punchColl.enabled = true;
			punchMesh.enabled = true;

			//Special Effects!
			punchEffect.Play();

			//Debug.Log ("Start");
			//Combo counting
			comboBuffer = 1f;
			comboCounter += 1;
		}
		//Finisher Kick
		else if (Input.GetKeyDown ("space") && !punching) {
			comboBuffer = 0f;
			comboCounter = 0;
			//Special Effects!
			kickEffect.Play();

			//kickColl.enabled = true;
			kickMesh.enabled = true;
			punching = true;
			//Ending Lag
			punchTime = 1f;
			Time.timeScale = .2f;
		}
		//Decrement Punch Duration
		if (punching && punchTime > 0) {
			punchTime -= Time.deltaTime;
			//Turns off Kick
			if(punchTime < .7f){
				kickColl.enabled = false;
				kickMesh.enabled = false;
			}
			//Reverts Timescale
			if(punchTime < .9f && kickMesh.enabled == true){
				Time.timeScale = 1f;
				kickColl.enabled = true;
			}
		}
		//Finish Punch
		else if (punching && punchTime <= 0 ){
			punchTime = .1f;
			punchColl.enabled = false;
			punchMesh.enabled = false;
			punching = false;
			//Debug.Log("Stop");
		}
		//Combo buffer decrement
		if (comboBuffer > 0) {
			comboBuffer -= Time.deltaTime;
		} 
		//Reset combo
		else if (comboBuffer <= 0) {
			comboBuffer = 0;
			comboCounter = 0;
		}

		//Debug.Log ("ComboBuffer is: " + comboBuffer + " Combocount is: " + comboCounter);
		//////////////////////////////////////////

		//Facing left and right////////////////
		if (hor >= .1f && facingRight == -1 && punchTime <= .3f)
			Flip ();
		else if (hor < -.1f && facingRight == 1 && punchTime <= .3f)
			Flip();
		///////////////////////////////////////

	}
	
	void Flip(){
		// Switch the way the player is labelled as facing
		facingRight = facingRight * -1;
		//Debug.Log ("Facing is: " + facingRight);
		
		transform.Rotate (0f, 180f, 0f);
	}

	//Adding force for the kick yo
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "NPC" && kickColl.enabled == true) {
			other.gameObject.GetComponent<Rigidbody2D>().
				AddForce(new Vector2(10f * facingRight, 0), ForceMode2D.Impulse);
		}
		if (other.gameObject.name == "dime(Clone)") {
			Destroy(other.gameObject);
			money += 10;
			Debug.Log("Bling");
		}
		if (other.gameObject.name == "nickel(Clone)") {
			Destroy(other.gameObject);
			money += 5;
			Debug.Log("Bling");
		}
		if (other.gameObject.name == "quarter(Clone)") {
			Destroy(other.gameObject);
			money += 25;
			Debug.Log("Bling");
		}

	}


}
