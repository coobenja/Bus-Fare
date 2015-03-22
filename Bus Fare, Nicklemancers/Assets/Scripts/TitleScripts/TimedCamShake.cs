using UnityEngine;
using System.Collections;

public class TimedCamShake : MonoBehaviour 
{
	
	Vector3 originalCameraPosition;
	private bool isShaking;

	public float shakeSpeed;
	public float shakeAmt = 0;
	public Camera mainCamera;
	public float startDelay;
	public float shakeDuration;

	
	public PlayerController playerController;
	
	void Start(){
		//originalCameraPosition = mainCamera.transform.position;
		playerController = GetComponent<PlayerController> ();
		isShaking = false;
	}
	
	void Update(){
		if (startDelay <= 0 && !isShaking) 
		{
			//shakeAmt = .05f;
			InvokeRepeating ("CameraShake", 0, .01f);
			Invoke ("StopShaking", shakeDuration);
			isShaking = true;
		}
		else if (startDelay > 0)
		{
			startDelay -= Time.deltaTime;
		}
	}
	
	/*void OnCollisionEnter2D(Collision2D coll) 
	{
		
		shakeAmt = coll.relativeVelocity.magnitude * .0025f;
		InvokeRepeating("CameraShake", 0, .01f);
		Invoke("StopShaking", 0.3f);
		
	}*/
	
	void CameraShake()
	{
		if(shakeAmt>0) 
		{
			float quakeAmt = Random.value*shakeAmt*2 - shakeAmt;
			Vector3 pp = mainCamera.transform.position;
			pp.y+= quakeAmt; // can also add to x and/or z
			pp.z+= quakeAmt;
			mainCamera.transform.position = pp;
		}
	}
	
	void StopShaking()
	{
		CancelInvoke("CameraShake");
		//mainCamera.transform.position = originalCameraPosition;
		isShaking = false;
	}
	
}