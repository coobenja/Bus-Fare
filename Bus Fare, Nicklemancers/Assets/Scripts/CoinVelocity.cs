using UnityEngine;
using System.Collections;

public class CoinVelocity : MonoBehaviour {

	public float speed;
	public float time;
	private float startTime;
	private float startHeight;
	private SpriteRenderer sprite_renderer;
	private float lifetime = 5f;
	private float flickrTime = 3f;
	
	//private float startHeight;
	
	void Start () {
		Vector2 coinspout = new Vector2 (.01f * Random.Range (-500f, 500f), 5.0f);
		//startHeight = transform.position.y;
		startHeight = transform.position.y;
		Rigidbody2D rigid;
		rigid = GetComponent<Rigidbody2D> ();
		rigid.AddForce (coinspout * speed, ForceMode2D.Force);
		GetComponent<CircleCollider2D> ().enabled = false;
		startTime = Time.time;

	}

	void Update() {
		if(transform.position.y <= (startHeight - 1f)) {
			Debug.Log ("doing shit");

			GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
			GetComponent<Rigidbody2D>().gravityScale = 0f;
			GetComponent<CircleCollider2D>().enabled = true;

			if(startTime + lifetime - Time.time > 0) {
				if (startTime + flickrTime - Time.time <= 0 && Time.time > time) {
					sprite_renderer = GetComponent<SpriteRenderer> ();
					time = Time.time + .2f;

					if (sprite_renderer.enabled){
						sprite_renderer.enabled = false;
					}
					else if (!sprite_renderer.enabled) {
						sprite_renderer.enabled = true;
					}

				}
			}
			else
				Destroy(gameObject);
		}
	}
}
	


