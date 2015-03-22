using UnityEngine;
using System.Collections;

public class NPCCoins : MonoBehaviour {

	public GameObject nickel;
	public GameObject dime;
	public GameObject quarter;

	public detection Detection;

	public bool aggressed = false;

	public float coins;
	private bool chance;
	// Use this for initialization
	void Start () {
		coins = Random.Range (2, 30);
		coins = coins * 5;
		Debug.Log (coins);
		Detection = gameObject.GetComponent<detection> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		chance = Random.value > .50;
		if (coins > 0) {
			//Hitstun
			Detection.hitStun = true;

			if (other.gameObject.tag == "Foot") {
				aggressed = true;
				coins = coins - 25;
				Rigidbody2D deathCash;
				if(chance) {
					deathCash = Instantiate (quarter, transform.position, transform.rotation) as Rigidbody2D;
				} else {
					deathCash = Instantiate (dime, transform.position, transform.rotation) as Rigidbody2D;
					deathCash = Instantiate (dime, transform.position, transform.rotation) as Rigidbody2D;
					deathCash = Instantiate (nickel, transform.position, transform.rotation) as Rigidbody2D;
				}

			}
			if (other.gameObject.tag == "Fist") {
				aggressed = true;
				if (chance) {
					coins = coins - 5;
					Rigidbody2D deathCash;
					deathCash = Instantiate (nickel, transform.position, transform.rotation) as Rigidbody2D;

				} else {
					coins = coins - 10;
					Rigidbody2D deathCash;
					deathCash = Instantiate (dime, transform.position, transform.rotation) as Rigidbody2D;

				}
			}
		

	}			
}
}