using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour {

	private float timer;
	private int minutes;
	private int seconds;
	public Text timerText;
	public Text gameOverText;

	// Use this for initialization
	void Start () {
		timer = 3600f;
		gameOverText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			minutes = (int) (timer / 60);
			seconds = (int) (timer % 60);
			timer -= Time.deltaTime;
			timerText.text = minutes + ":" + seconds;
		} else {
			gameOverText.enabled = true;
		}
	}
}
