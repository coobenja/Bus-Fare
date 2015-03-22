using UnityEngine;
using System.Collections;

public class TitleFlash : MonoBehaviour {
	public float startDelay;
	public float flashSpeed;
	public bool repeat = false;

	private bool fadeIn = true;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = new Color (1f, 1f, 1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		flash ();
	}

	void flash()
	{
		Color on = new Color (1f, 1f, 1f, 0.95f);
		Color off = new Color (1f, 1f, 1f, 0f);

		if (flashSpeed == 0)
			flashSpeed = 1;

		if (startDelay <= 0) 
		{
			if (fadeIn)
			{
				GetComponent<Renderer>().material.color = Color.Lerp (GetComponent<Renderer>().material.color, on, Time.deltaTime * flashSpeed);
				if (GetComponent<Renderer>().material.color == on)
					fadeIn = false;
			}
			else
			{
				GetComponent<Renderer>().material.color = Color.Lerp (GetComponent<Renderer>().material.color, off, Time.deltaTime * flashSpeed);
				if (GetComponent<Renderer>().material.color == off && repeat)
				    fadeIn = true;
			}
		}
		else
		{
			startDelay -= Time.deltaTime;
		}
	}
}
