using UnityEngine;
using System.Collections;

public class NPCCoins : MonoBehaviour {

	public GameObject nickel;
	public GameObject dime;
	public GameObject quarter;

	private float coins;
	private bool chance;
	// Use this for initialization
	void Start () {
		coins = Random.Range (2, 30);
		coins = coins * 5;
		Debug.Log (coins);
	}

	void OnTriggerEnter2D(Collider2D other) {
		chance = Random.value > .50;
		if (other.gameObject.tag == "Foot") {
			coins = coins - 25;
			Rigidbody2D deathCash;
			deathCash = Instantiate (quarter, transform.position, transform.rotation) as Rigidbody2D;
			deathCash.velocity = transform.TransformDirection (Vector2.up * 10);
		}
		if (other.gameObject.tag == "Fist") {
			if (chance) {
				coins = coins - 5;
				Rigidbody2D deathCash;
				deathCash = Instantiate (nickel, transform.position, transform.rotation) as Rigidbody2D;
				deathCash.velocity = transform.TransformDirection (Vector2.up * 10);
			} 
			else {
				coins = coins - 10;
				Rigidbody2D deathCash;
				deathCash = Instantiate (dime, transform.position, transform.rotation) as Rigidbody2D;
				deathCash.velocity = transform.TransformDirection (Vector2.up * 10);
			}
		}
		Debug.Log (other.gameObject.tag == "Foot");

	}			
}