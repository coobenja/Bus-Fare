using UnityEngine;
using System.Collections;

public class CoinVelocity : MonoBehaviour {

	public float speed;
	private Vector2 coinspout = new Vector2 (0, 1.0f);
	//private float startHeight;

	void Start () {
		//startHeight = transform.position.y;

		Rigidbody2D rigid;
		rigid = GetComponent<Rigidbody2D>();
		rigid.AddForce(coinspout, ForceMode2D.Force);

	}
	
}
