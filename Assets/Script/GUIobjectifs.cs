using UnityEngine;
using System.Collections;

public class GUIobjectifs : MonoBehaviour {

	private bool displayObjectif = true;
	private string objectifText = "";
	private float secondsRemaining = 10;

	public void setObjectifText(string t) { objectifText = t; }
	public void setDisplayOnOff() { displayObjectif = !displayObjectif; }
	
	void OnGUI () {

		if (displayObjectif) {
			myTimer();

			GUI.BeginGroup (new Rect (Screen.width / 2 -150, 20, 300, 30));
			
			GUIStyle myStyle = new GUIStyle();
			myStyle.fontSize = 30;
			myStyle.normal.textColor = Color.red;
			myStyle.border.Add (new Rect (0,0,500,30));
			
			// Draw the background image
			GUI.Box (new Rect (0,0,300,30), "Objectif : Echappez-vous avec l'avion!");
			//GUI.Label(new Rect (0,0,500,30), "Objectif : Echappez-vous avec l'avion!",myStyle);
			
			GUI.EndGroup ();
		}
	}

	private void myTimer(){
		secondsRemaining -= Time.deltaTime;

		Debug.Log ("secondes restantes : " + secondsRemaining);

		if ( secondsRemaining < 0 )
		{
			setDisplayOnOff();
		}
	}

}
