using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(player.GetComponent<CamShakeSimple>().isShaking == false){
			gameObject.transform.position = new Vector3 
				(player.transform.position.x, player.transform.position.y, -10f);
		}

	}
}
