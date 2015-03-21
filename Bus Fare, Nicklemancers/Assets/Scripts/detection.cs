using UnityEngine;
using System.Collections;

public class detection : MonoBehaviour {

	private int money = 25;

	public Transform player;
	public float detect;
	public float escape;
	private float speed = .035f;

	public bool hitStun;
	private float stunTime = .15f;

	public bool chasing;

	//public GameObject manager;
	public Manager manager;

	void Start(){
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		player = GameObject.Find ("Player").transform;


	}
	
	// Update is called once per frame
	void Update () {

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

		if (money <= 0) {
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
		}

	}
}
