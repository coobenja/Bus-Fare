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

	public int coins = 100;

	private bool punching = false;
	public BoxCollider2D punchColl;
	public MeshRenderer punchMesh;
	public BoxCollider2D kickColl;
	public MeshRenderer kickMesh;
	public ParticleSystem kickEffect;
	public ParticleSystem punchEffect;

	public GameObject nickel;
	public GameObject dime;
	public GameObject quarter;

	public Animator playerAnim;


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

		playerAnim = transform.Find ("PCSprite").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Ignore Other Character Collisions
		Physics2D.IgnoreLayerCollision (8,8);

		//Player Movement/////////////////////////
		hor = Input.GetAxis ("Horizontal");
		vert = Input.GetAxis ("Vertical");

		if (punchTime <= .3f) {
			playerAnim.SetBool("walking", true);
			transform.Translate (Vector2.right * hor/13f * facingRight);
			transform.Translate (Vector2.up * vert/13f);
		}
		else
			playerAnim.SetBool("walking", false);
		//////////////////////////////////////////

		//Punching + Kicking//////////////////////////

		//Punching Animations
		if (punching && comboCounter == 1) {
			playerAnim.SetBool ("punch1", true);
		} else if (punching && comboCounter == 2) {
			playerAnim.SetBool ("punch2", true);
		} else if (punching && kickMesh.enabled == true) {
			playerAnim.SetBool ("kick", true); 
		} /*else if (!punching) {
			playerAnim.SetBool ("punch1", false);
			playerAnim.SetBool ("punch2", false);
			playerAnim.SetBool ("kick", false); 
		}*/
		else if (playerAnim.GetBool("walking") == false){
			playerAnim.SetBool ("kick", false);
			//playerAnim.SetBool ("idle", true);
			playerAnim.SetBool ("punch1", false);
			playerAnim.SetBool ("punch2", false);
		}
			


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
		else if (Input.GetKeyDown ("space") && !punching && comboCounter == 2) {
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
			coins += 10;

		}
		if (other.gameObject.name == "nickel(Clone)") {
			Destroy(other.gameObject);
			coins += 5;

		}
		if (other.gameObject.name == "quarter(Clone)") {
			Destroy(other.gameObject);
			coins += 25;

		}
		if(other.gameObject.tag == "NPCPunch") {
			Rigidbody2D deathCash;
			if (coins < 25) {
				coins = coins - 15;
				if (coins > 10) {
					deathCash = Instantiate(nickel, transform.position, transform.rotation) as Rigidbody2D;
				}
			}
			//gives off coins "proportional" to the amount you have
			else if (coins < 75) {
				coins = coins - coins - 15;
				deathCash = Instantiate (quarter, transform.position, transform.rotation) as Rigidbody2D;
				if (coins > 35) {
					deathCash = Instantiate (dime, transform.position, transform.rotation) as Rigidbody2D;
				}
			}
			// gives off a lot of coins...
			else {
				coins = coins - 55;
				deathCash = Instantiate (quarter, transform.position, transform.rotation) as Rigidbody2D;
				deathCash = Instantiate (dime, transform.position, transform.rotation) as Rigidbody2D;
				deathCash = Instantiate (nickel, transform.position, transform.rotation) as Rigidbody2D;
				deathCash = Instantiate (nickel, transform.position, transform.rotation) as Rigidbody2D;
			}
			Debug.Log ("Coins are: " + coins);
		}
		//Check for player death
		if (coins <= 0) {
			Destroy(gameObject);
		}
	}


}
