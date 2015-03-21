using UnityEngine;
using System.Collections;

public class detection : MonoBehaviour {
	public Transform player;
	public float detect;
	public float escape;
	private float speed = .1f;

	public bool chasing;
	// Update is called once per frame
	void Update () {


		float dist = Vector2.Distance(player.position, transform.position);
		Debug.Log (dist);
		 if (dist <= detect) {
			chasing = true;
		}

		if (dist >= escape) {
			chasing = false;
		}

		if (chasing) {
			transform.position = Vector2.MoveTowards(transform.position, player.position, speed);
		}


	
	}
}
