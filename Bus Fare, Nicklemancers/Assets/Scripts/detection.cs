using UnityEngine;
using System.Collections;

public class detection : MonoBehaviour {

	private int money = 25;

	private bool right = true;


	public Transform player;
	public BoxCollider2D punchArm;
	[HideInInspector]
	public float windUpTimer = 0;
	[HideInInspector]
	public bool windingUp = false;
	[HideInInspector]
	public float windUpLimit = 1f;

	public float detect;
	public float escape;
	private float speed = .035f;

	public bool hitStun;
	[HideInInspector]
	public float stunTime = 1f;

	public bool chasing;

	private float time;
	private float startTime;
	private float lifetime = 7f;
	private float flickrTime = 6f;
	private SpriteRenderer sprite_renderer;
	private NPCCoins npcCoins;
	private bool keepCheckingForLTZero = true;
	private float startCoins;

	private bool keepCheckingForZero = true;
	private float startTime2;
	public float waitTime;
	private float time2;
	public float punchSpeed=1f;
	public bool superAggressive = false;

	public Animator NPC1anim;



	//public GameObject manager;
	public Manager manager;

	void Start(){
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		player = GameObject.Find ("Player").transform;
		npcCoins = GetComponent<NPCCoins>();

		//Punching arm set up
		punchArm = transform.Find ("Arm").GetComponent<BoxCollider2D> ();
		punchArm.enabled = false;

		//initial amount of coins
		startCoins = npcCoins.coins;
		if (superAggressive) {
			 npcCoins.aggressed = true;
		}



	}

	// Update is called once per frame
	void FixedUpdate () {

		/*if (superAggressive) {
			npcCoins.aggressed = true;
		}*/
		if (npcCoins.aggressed)
			NPC1anim.SetBool ("NPC1aggro", true);

		//Facing left and right
		if (npcCoins.aggressed) {
			if (player.position.x - transform.position.x > 0 && !right) {
				Flip ();
				//Debug.Log("Flip right");
			} else if (player.position.x - transform.position.x <= 0 && right) {
				Flip ();
				//Debug.Log("Flip left");
			}
		}

		//Ignore Other Character Collisions
		Physics2D.IgnoreLayerCollision (8, 8);


		//One NPC has been Aggressed, one more may join him in the fight
		/*if (npcCoins.coins < startCoins) {
			aggressed = true;
		}*/


		//Movement/Chasing Logic///////////////////
		float dist = Vector2.Distance (player.position, transform.position);


		//Debug.Log (dist);
		if (dist <= detect && manager.numChasers <= manager.chaseMax && npcCoins.aggressed && !chasing) {
			chasing = true;
			manager.numChasers += 1;

		}

		if (dist >= escape && chasing) {
			chasing = false;
			npcCoins.aggressed = false;
			manager.numChasers -= 1;
		}

		if (chasing && !hitStun && dist > .9f) {
			transform.position = Vector2.MoveTowards (transform.position, player.position, speed);
			NPC1anim.SetBool ("NPC1walking", true);
		}else{
			NPC1anim.SetBool ("NPC1walking", false);
		}
		//////////////////////////////////////////

		//Winding up and hittin the player/////////



		///////////////////////////////////////////

		//Hitstun Logic///////////////////////////

		if (hitStun && stunTime > 0) {
			stunTime -= Time.deltaTime;
			Debug.Log ("Hitstun of: " + stunTime);
		} else if (hitStun && stunTime <= 0) {
			stunTime = 1f;
			hitStun = false;
		}

		if (npcCoins.coins <= 0) {

			////////////////KARL, NPC DIES HERE, ADD ANIMATION REFERENCE HERE/////////////
			NPC1anim.SetBool ("NPC1dead",true);
			if(chasing)
				manager.numChasers -= 1;
			chasing = false;

			if (keepCheckingForLTZero) {
				startTime = Time.time;
				keepCheckingForLTZero = false;
			}
			npcCoins.aggressed=false;
			/*
			Vector2 deathForce;
			if (right) {
				deathForce = new Vector2 (-7f, 0f);
			} else 
				deathForce = new Vector2 (7f, 0f);

			GetComponent<Rigidbody2D> ().AddForce (deathForce, ForceMode2D.Force);
			GetComponent<Rigidbody2D> ().gravityScale = 0f;
			*/
		}
			/*Debug.Log(startTime + lifetime - Time.time);
				if(startTime + lifetime - Time.time > 0) {
					if (startTime + flickrTime - Time.time <= 0 && Time.time > time) {
						sprite_renderer = GetComponent<SpriteRenderer>();
						time = Time.time + .3f;
						Debug.Log ("Have enterted this if statement");
						if (sprite_renderer.enabled){
							Debug.Log(sprite_renderer.enabled);
							sprite_renderer.enabled = false;
						}//if
						else if (!sprite_renderer.enabled) {
							sprite_renderer.enabled =true ;
							Debug.Log (sprite_renderer.enabled);
						}//else if
						
					}//if
				}//if
				else 
					Destroy(gameObject);
		}//if
*/
			//NPC Attacking Script
			if (dist < 2f && npcCoins.aggressed) {
				if (keepCheckingForZero) {
					startTime2 = Time.time;
					keepCheckingForZero = false;
				}
				//Debug.Log (startTime2 + waitTime - Time.time );
			
				if (startTime2 + waitTime - Time.time <= 0 && Time.time > time2 && !hitStun) {
				
					punchArm.enabled = true;
					NPC1anim.SetTrigger ("NPC1punch");
					time2 = Time.time + punchSpeed;
				} else {
					punchArm.enabled = false;
					//NPC1anim.SetBool ("NPC1punching",false);
				}
			//punchArm.enabled = false;
			if (superAggressive) {
				npcCoins.aggressed = true;
			}

			}

		}
	//void update
	
	void Flip(){
		// Switch the way the player is labelled as facing
		right = !right;
		//Debug.Log ("Facing is: " + facingRight);
		
		transform.Rotate (0f, 180f, 0f);
	}
}
