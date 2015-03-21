using UnityEngine;
using System.Collections;

public class CoinVelocity : MonoBehaviour {

	public float speed;
	private float startHeight;
	
	//private float startHeight;
	
	void Start () {
		Vector2 coinspout = new Vector2 (.01f * Random.Range (-500f, 500f), 5.0f);
		//startHeight = transform.position.y;
		startHeight = transform.position.y;
		Rigidbody2D rigid;
		rigid = GetComponent<Rigidbody2D> ();
		rigid.AddForce (coinspout * speed, ForceMode2D.Force);
	}

	void Update() {
		if(transform.position.y <= (startHeight - 1f)) {
			Debug.Log ("doing shit");

			GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
			GetComponent<Rigidbody2D>().gravityScale = 0f;


		}
		
	}
	
}
