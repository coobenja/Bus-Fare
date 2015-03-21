using UnityEngine;
using System.Collections;

public class CoinVelocity : MonoBehaviour {

	public float speed;
	private Vector2 coinspout = new Vector2 (.01f * Random.Range (-500, 500), 5.0f);

	//private float startHeight;

	void Start () {
		//startHeight = transform.position.y;
		float startHeight = transform.position.y;
		Rigidbody2D rigid;
		rigid = GetComponent<Rigidbody2D>();
		rigid.AddForce(coinspout * speed, ForceMode2D.Force);
		if(transform.position.y <= (startHeight - 2f)) {
			
			GetComponent<Rigidbody2D>().gravityScale = 0f;

		}

	}
	
}
