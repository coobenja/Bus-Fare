using UnityEngine;
using System.Collections;

public class NPCCoins : MonoBehaviour {

	private float coins;
	// Use this for initialization
	void Start () {
		coins = Random.Range (2, 30);
		coins = coins * 5;
		Debug.Log (coins);
	}


}
