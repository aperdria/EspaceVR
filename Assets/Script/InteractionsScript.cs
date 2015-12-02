using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionsScript : MonoBehaviour
{
	
	public Text tips;
	private float distanceToObj;

	private Rigidbody rb;
	private bool canClimb = false;
	private bool canPush = false;
	private bool canOpen = false;
	private bool canOpenDoor = false;
	private GameObject objectToPush;
	private GameObject objectToOpen;
	private bool getKeys = false;
	
	void Start ()
	{
		distanceToObj = -1;	
		Cursor.visible = true;
		rb = GetComponent<Rigidbody> ();
		objectToPush = null;
		GameObject.Find("Door/Arch_C/Door_B").GetComponent<Animator> ().SetBool("open",false);
		tips.enabled = false;
	}

	void Update ()
	{
		if (canClimb == true) {
			if (Input.GetKey (KeyCode.B)) {
				climbLadder();
			}
			if (Input.GetKey (KeyCode.V)) {
				downLadder();
			}
		}
		
		if (canPush == true) {
			if (Input.GetKey (KeyCode.B)) {
				pushBarrel();
			}
		}

		if (canOpen == true) {
			if (Input.GetKey (KeyCode.B)) {
				openBox();
			}
			else if (Input.GetKey (KeyCode.V)){
				takeKeys();
			}
		}

		if (canOpenDoor == true) {
			if (Input.GetKey (KeyCode.V)) {
				openDoor();
			}
		}
	}

	void takeKeys () {
		Debug.Log ("Modification de la position des clés");
		GameObject keys = GameObject.Find ("Objects/tresure_box/keys");
		Debug.Log ("Position des clés : " + keys.transform.position.ToString());
		Vector3 posKeys = new Vector3 (0, 0.2f, 0);
		keys.transform.Translate (posKeys);
		getKeys = true;
	}

	void pushBarrel () {
		distanceToObj = distance (objectToPush.transform.position, transform.position);
		Debug.Log ("Distance : " + distanceToObj);
		Debug.Log ("Transform.forward : " + transform.forward.ToString ());
		if (distanceToObj < 2) {
			Vector3 newPos = new Vector3 (0, 2, 0) * Time.deltaTime;
			objectToPush.GetComponent<Rigidbody> ().transform.Translate(newPos);
		}
	}

	void openBox () {
			Debug.Log ("Appel de l'animation sur l'objet :"+objectToOpen.name.ToString());
			objectToOpen.gameObject.GetComponent<Animator>().SetBool("open",true);
	}

	void climbLadder () {
		rb.useGravity = false;
		Debug.Log ("Montée");
		Vector3 newPos = new Vector3 (0, 13, 0) * Time.deltaTime;
		transform.Translate (newPos);
		newPos = new Vector3 (0, 0, 3) * Time.deltaTime;
		transform.Translate (newPos);
	}

	void downLadder () {
		rb.useGravity = false;
		Debug.Log ("Descente");
		Vector3 newPos = new Vector3 (0, -13, 0) * Time.deltaTime;
		transform.Translate (newPos);
		newPos = new Vector3 (0, 0, -3) * Time.deltaTime;
		transform.Translate (newPos);
	}

	void openDoor () {
		Debug.Log ("Ouverture de la porte");
		GameObject.Find("Door/Arch_C/Door_B").GetComponent<Animator> ().SetBool("open",false);
	}

	
	void OnCollisionEnter (Collision other)
	{
		canClimb = false;
		canPush = false;
		canOpen = false;
		canOpenDoor = false;

		Debug.Log ("Collision avec " + other.gameObject.name);
		bool barrel = other.gameObject.CompareTag ("Barrel");
		bool ladder = other.gameObject.CompareTag ("Ladder");
		bool box = other.gameObject.CompareTag ("Box");
		bool door = other.gameObject.CompareTag ("Door");
		
		if (ladder) {
			Debug.Log ("Collision avec une échelle");
			showTip ("Pour grimper à l'échelle, monter vos mains devant vos yeux");
			canClimb = true;
			rb.useGravity = false;
		} else if (barrel) {
			Debug.Log ("Collision avec un tonneau");
			showTip ("Pour pousser le tonneau, pousser vos mains devant vous.");
			canPush = true;
			objectToPush = other.gameObject;
		} else if (box) {
			Debug.Log ("Collision avec un coffre");
			showTip ("Je crois que ce coffre n'est pas fermé à clef. Essayer de l'ouvrir");
			canOpen = true;
			objectToOpen = other.gameObject;
			
		} else if (door) {
			Debug.Log ("Collision avec une porte");
			showTip ("Vous avez la clé, tirer la poignée.");
			canOpenDoor = true;
		} else {
			canClimb = false;
			rb.useGravity = false;
			Debug.Log ("Plus de collision");
			tips.enabled = false;
		}
	}

	private void showTip (string message) {
		tips.text = message;
		tips.enabled = true;
	}
	
	private float distance (Vector3 v1, Vector3 v2)
	{
		return Mathf.Sqrt (Mathf.Pow ((v1.x - v2.x), 2) + Mathf.Pow ((v1.y - v2.y), 2) + Mathf.Pow ((v1.z - v2.z), 2));
	}
}