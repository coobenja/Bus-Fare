using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public int chaseMax = 2;
	public int numChasers = 0;

	public BoxCollider2D busColl;
	public PlayerController player;


	// Use this for initialization
	void Start () {
		//busColl = transform.Find ("Bus Stop").GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
