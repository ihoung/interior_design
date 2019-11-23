using UnityEngine;
using System.Collections;

public class AudioControll : MonoBehaviour {

	public Texture2D playImage;
	public Texture2D stopImage;

	private bool lableshow;

	// Use this for initialization
	void Start () {
		lableshow = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (lableshow) 
		{
			if (Input.GetKeyDown (KeyCode.F)) 
			{
				if (GetComponent<AudioSource> ().isPlaying)
					GetComponent<AudioSource> ().Pause ();
				else
					GetComponent<AudioSource> ().Play ();
			}
			if (Input.GetKeyDown (KeyCode.G)) 
			{
				if (GetComponent<AudioSource> ().isPlaying)
					GetComponent<AudioSource> ().Stop ();
			}
		}
	}

	void OnCollisionStay(Collision go){
		if (go.gameObject.tag == "Detection") {
			lableshow = true;
		}
	}

	void OnCollisionExit(Collision go){
		if (go.gameObject.tag == "Detection") {
			lableshow = false;
		}
	}

	void OnGUI(){
		if (lableshow) {
			GUI.Box (new Rect (Input.mousePosition.x, Input.mousePosition.y, 100, 50), new GUIContent ("F", playImage));
			if (GetComponent<AudioSource> ().isPlaying) {
				GUI.Box (new Rect (Input.mousePosition.x, Input.mousePosition.y + 50, 100, 50), new GUIContent ("G", stopImage));
			}
		}
	}


}
