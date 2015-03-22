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
	
	public bool punch1ready = true;
	public bool punch2ready = true;
	public bool kickReady = true;
	public bool comboResetBool = true;
	
	public bool punch1 = true;
	public bool kicking = false;
	
	public float punchTimeoutLimit = .5f;
	public float punchDelay = .1f;
	public float punchDelayCountdown=0;
	public float punchTimeout = 0;
	public float punchDuration = .2f;
	private float timeOfPunch;
	
	
	public float kickDelay = 2f;
	public float kickDelayCountdown=0;
	public float kickDuration = .5f;
	private float timeOfKick;
	public float kickOffset = 1f;
	
	public AudioSource kickSFX;
	public AudioSource punchSFX1;
	public AudioSource punchSFX2;
	public AudioSource gruntSFX1;
	public AudioSource gruntSFX2;
	
	
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
			//playerAnim.SetTrigger("walking");
			transform.Translate (Vector2.right * hor/13f * facingRight);
			transform.Translate (Vector2.up * vert/13f);
		}
		
		//////////////////////////////////////////
		
		//Punching + Kicking//////////////////////////
		
		punchTimeout-=Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.X) && !kicking) {
			punchTimeout = punchTimeoutLimit;
		}
		
		if (punchTimeout > 0) {
			if (punchDelayCountdown <= 0) {
				punching = true;
				timeOfPunch = Time.time;
				punchDelayCountdown = punchDelay;
				playerAnim.SetBool ("punching", true);
				punchColl.enabled = true;
				//punchMesh.enabled = true;
				
				//Special Effects!
				punchEffect.Play ();
				punchSFX1.Play();
			}
		} else {
			
			punching = false;
			playerAnim.SetBool ("punching", false);
		}
		
		if (punchDuration + timeOfPunch <= Time.time) {
			punchColl.enabled = false;
			punchMesh.enabled = false;
		}
		
		punchDelayCountdown-=Time.deltaTime;
		
		//Kick
		if (Input.GetKeyDown (KeyCode.Z) && !kicking && !punching && kickDelayCountdown <= 0) {
			kicking = true;
			playerAnim.SetBool ("kicking", true);
			timeOfKick = Time.time;
			kickDelayCountdown = kickDelay;
		}
		
		if((timeOfKick+kickOffset)<=Time.time && kicking){
			kickColl.enabled = true;
			//kickMesh.enabled = true;
			kickEffect.Play ();
			kickSFX.Play();
			gruntSFX2.Play();
			kicking = false;
			playerAnim.SetBool ("kicking",false);
		}
		
		if ((kickDuration + timeOfKick) <= Time.time) {
			kickColl.enabled = false;
			kickMesh.enabled = false;
		}
		kickDelayCountdown -= Time.deltaTime;
		
		//Walking//
		
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
			playerAnim.SetBool ("walkingBool", true);
		} else {
			playerAnim.SetBool ("walkingBool", false);
		}
		
		
		
		/*
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
			playerAnim.SetBool("punching",false);
			
			//playerAnim.SetTrigger ("idle");
			//Debug.Log("Stop");
		}
		*/
		
		
		
		//Allows us to kick stuff
		/*
		//Kicks, then stops kicking
		if (kicking) {
			playerAnim.SetTrigger ("kick");
			kicking = false;
		}

		//Decides whether to do punch1 or punch2 (both the same gameplay wise)
		if (punching && punch1) {
			playerAnim.SetTrigger("punch1");
			punch1 = false;
			punching = false;

		}
		else if (punching && !punch1) {
			playerAnim.SetTrigger("punch2");
			punch1 = true;
			punching = false;
		}
		*/
		
		//Punching Animations
		/*
		if (punching && comboCounter == 1) {
			playerAnim.SetTrigger ("punch1");
		} else if (punching && comboCounter == 2) {
			playerAnim.SetTrigger ("punch2");
		} else if (punching && kickMesh.enabled == true) {
			playerAnim.SetTrigger ("kick"); 
		} 

		if (comboCounter == 1 && punch1ready) {
			comboResetBool = true;
			playerAnim.SetTrigger ("punch1");
			punch1ready = false;
		} else if (comboCounter == 2 && punch2ready) {
			playerAnim.SetTrigger ("punch2");
			punch2ready = false;
		} else if (kickMesh.enabled == true && kickReady) {
			playerAnim.SetTrigger ("kick"); 
			kickReady = false;
		} 
		if (comboCounter == 0 && comboResetBool) {
			playerAnim.SetTrigger ("comboReset");
			punch1ready = true;
			punch2ready = true;
			kickReady = true;
			comboResetBool=false;
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

			//playerAnim.SetTrigger ("idle");
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

		*/
		
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
			//Debug.Log ("Coins are: " + coins);
		}
		//Check for player death
		if (coins <= 0) {
			Destroy(gameObject);
		}
	}
	
	
}
