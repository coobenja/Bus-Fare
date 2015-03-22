using UnityEngine;
using System.Collections;

public class TitleAnimation : MonoBehaviour {

	public float newPositionX;
	public float newPositionY;
	public float newPositionZ;
	public float moveSpeed;
	public float startDelay;

	// Use this for initialization
	void Start () {
		//transform.position = new Vector3(transform.position.x, -13, transform.position.z);
	}
	
	void move()
	{
		Vector3 newPosition = transform.position;

		if (moveSpeed == 0)
			moveSpeed = 1;
		if (newPositionX != 0)
			newPosition = new Vector3 (newPositionX, newPosition.y, newPosition.z);
		if (newPositionY != 0)
			newPosition = new Vector3 (newPosition.x, newPositionY, newPosition.z);
		if (newPositionZ != 0)
			newPosition = new Vector3 (newPosition.x, newPosition.y, newPositionZ);
		if (startDelay <= 0) 
		{
			if (transform.position != newPosition)
			{
				transform.position = Vector3.Lerp (transform.position, newPosition, Time.deltaTime * moveSpeed);
			}
		}
		else
		{
			startDelay -= Time.deltaTime;
		}
	}
	
	// Update is called once per frame
	void Update () {
		move ();
	}
}
