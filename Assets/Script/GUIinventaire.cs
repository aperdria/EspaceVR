using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIinventaire : MonoBehaviour {
	
	//Nombre d'éléments dans l'inventaire
	private int nbElementsInventaire = 1;
	public void incrementInventaire() { nbElementsInventaire++; }
	public void decrementInventaire() { nbElementsInventaire--; }

	//Images des éléments de l'inventaire
	public Texture2D pelleTexture; 
	public Texture2D jericanTexture; 
	
	//Elements de l'inventaire
	private bool pelle = true;
	private bool jerican = false;
	public void setPelle() { pelle = true; }
	public void setJerican() { jerican = true; }
	
	//Liste des textures actuellement présente dans l'inventaire
	public List<Texture2D> elements;
	
	void OnGUI () {

		GUI.BeginGroup (new Rect (3,3,80,100+30*nbElementsInventaire+3*nbElementsInventaire));

		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 30;

		// Draw the background image
		GUI.Box (new Rect (0,0,80,20+30*nbElementsInventaire+3*nbElementsInventaire), "Inventaire");

		//Création de la liste des textures
		getElementInventaire ();

		for (int i = 0; i<nbElementsInventaire; i++) {

			GUI.BeginGroup (new Rect (5,20+30*i+3*i,70,30));
			GUI.Box (new Rect (0,0,70,30), elements[i]);
			GUI.EndGroup ();
		}

		GUI.EndGroup ();
	}

	private void getElementInventaire(){
		elements = new List<Texture2D>();
		//Modifier pour avoir l'ordre correct du scenario du jeu
		if (pelle)
			elements.Add (pelleTexture);

		if (jerican)
			elements.Add (jericanTexture);
	}

}
