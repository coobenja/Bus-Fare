using UnityEngine;
using System.Collections;

public class detection : MonoBehaviour {

	private int money = 25;

	private bool right = true;

	public Transform player;
	public Transform fist;
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


	}

	/*void OnCollisionStay2D( Collision2D other) {
		if(other.gameObject.tag == "NPC") {
			if (other.gameObjectameObject.Find("NPC").GetComponent<detection>().chasing) //will check if true
			other.gameObject.GetComponent("detection")();
			if(other.*/
	// Update is called once per frame
	void Update () {

		//Facing left and right
		if (player.position.x - transform.position.x > 0 && !right) {
			Flip ();
			Debug.Log("Flip right");
		} else if (player.position.x - transform.position.x <= 0 && right) {
			Flip ();
			Debug.Log("Flip left");
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

		//Hitstun Logic///////////////////////////

		if (hitStun && stunTime > 0) {
			stunTime -= Time.deltaTime;
		} else if (hitStun && stunTime <= 0) {
			stunTime = .15f;
			hitStun = false;
		}

		/////////////////////////////////////////

		//Death and Taxes////////////////////////

		/*if (money <= 0) {
			manager.numChasers -= 1;
			Destroy(this);
		}
		//Debug.Log (money);

		////////////////////////////////////////
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Fist") {
			hitStun = true;
			money -= 5;
		} else if (other.gameObject.tag == "Foot") {
			hitStun = true;
			money -= 10;
		}*/

	if (npcCoins.coins <= 0 ){
			chasing = false;
			if (keepCheckingForLTZero) {
				startTime = Time.time;
				keepCheckingForLTZero = false;
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
			GetComponent<Rigidbody2D>().gravityScale = 0f;
				

				if(startTime + lifetime - Time.time > 0) {
					if (startTime + flickrTime - Time.time <= 0 && Time.time > time) {
						sprite_renderer = GetComponent<SpriteRenderer>();
						time = Time.time + .3f;
						Debug.Log ("Have enterted this if statement");
						if (sprite_renderer.enabled){
							Debug.Log(sprite_renderer.enabled);
							sprite_renderer.enabled = false;
						}
						else if (!sprite_renderer.enabled) {
							sprite_renderer.enabled =true ;
							Debug.Log (sprite_renderer.enabled);
						}
						
					}
				}
				else {
					Destroy(gameObject);
			}

		}
	}

	void Flip(){
		// Switch the way the player is labelled as facing
		right = !right;
		//Debug.Log ("Facing is: " + facingRight);
		
		transform.Rotate (0f, 180f, 0f);
	}
}
