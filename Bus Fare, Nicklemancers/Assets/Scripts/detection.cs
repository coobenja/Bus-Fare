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
	private float stunTime = .15f;

	public bool chasing;

	public float time;
	private float startTime;
	private float lifetime = 6f;
	private float flickrTime = 1f;
	private SpriteRenderer sprite_renderer;
	private NPCCoins npcCoins;
	private bool keepCheckingForLTZero = true;



	//public GameObject manager;
	public Manager manager;

	void Start(){
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		player = GameObject.Find ("Player").transform;
		npcCoins = GetComponent<NPCCoins>();

		//Punching arm set up
		punchArm = transform.Find ("Arm").GetComponent<BoxCollider2D> ();
		punchArm.enabled = false;

	}

	// Update is called once per frame
	void FixedUpdate () {

		//Facing left and right
		if (player.position.x - transform.position.x > 0 && !right) {
			Flip ();
			//Debug.Log("Flip right");
		} else if (player.position.x - transform.position.x <= 0 && right) {
			Flip ();
			//Debug.Log("Flip left");
		}

		//Ignore Other Character Collisions
		Physics2D.IgnoreLayerCollision (8,8);

		//Movement/Chasing Logic///////////////////
		float dist = Vector2.Distance(player.position, transform.position);
		//Debug.Log (dist);
		 if (dist <= detect && manager.numChasers < manager.chaseMax) {
			chasing = true;
			manager.numChasers += 1;
		}

		if (dist >= escape && chasing) {
			chasing = false;
			manager.numChasers -= 1;
		}

		if (chasing && !hitStun && dist > .9f) {
			transform.position = Vector2.MoveTowards(transform.position, player.position, speed);
		}
		//////////////////////////////////////////

		//Winding up and hittin the player/////////



		///////////////////////////////////////////

		//Hitstun Logic///////////////////////////

		if (hitStun && stunTime > 0) {
			stunTime -= Time.deltaTime;
		} else if (hitStun && stunTime <= 0) {
			stunTime = .15f;
			hitStun = false;
		}


		if (npcCoins.coins <= 0 ){
			chasing = false;
			if (keepCheckingForLTZero) {
				startTime = Time.time;
				keepCheckingForLTZero = false;
			}
			Vector2 deathForce;
			if (right){
				deathForce = new Vector2 (-3f, 0f);
			}
			else 
				deathForce = new Vector2 (3f, 0f);

			GetComponent<Rigidbody2D>().AddForce(deathForce, ForceMode2D.Force);
			GetComponent<Rigidbody2D>().gravityScale = 0f;
				

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
	}//void update

	void Flip(){
		// Switch the way the player is labelled as facing
		right = !right;
		//Debug.Log ("Facing is: " + facingRight);
		
		transform.Rotate (0f, 180f, 0f);
	}
}
