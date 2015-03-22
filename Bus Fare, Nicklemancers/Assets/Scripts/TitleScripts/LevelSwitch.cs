using UnityEngine;
using System.Collections;

public class LevelSwitch : MonoBehaviour {
//	public bool restartTitleScreen = false;
//	public float timeTilRestart;
	public string levelToLoad;
//	private float timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log ("START");
			Application.LoadLevel (levelToLoad);
		}
		//Debug.Log (Input.GetButtonDown (button));
		// Change this to change to a different level when start is pressed
	}
}
