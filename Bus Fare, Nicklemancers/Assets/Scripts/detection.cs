using UnityEngine;
using System.Collections;

public class detection : MonoBehaviour {
	public Transform player;
	public float detect;
	public float escape;
	private float speed = .05f;

	public bool hitStun;

	public bool chasing;

	//public GameObject manager;
	public Manager manager;

	void Start(){
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

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

	}
}
