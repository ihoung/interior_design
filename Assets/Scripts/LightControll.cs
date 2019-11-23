using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightControll : MonoBehaviour {

	private bool lableshow;
	private List<GameObject> lights=new List<GameObject>();

	// Use this for initialization
	void Start () {
		lableshow = false;
		foreach (Transform child in transform) {
			if (child.tag == "Light")
				lights.Add (child.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (lableshow) 
		{
			if (Input.GetKeyDown (KeyCode.F)) 
			{
				foreach (GameObject light in lights) {
					if (light.activeSelf)
						light.SetActive (false);
					else
						light.SetActive (true);
					}
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
			GUI.Box (new Rect (Input.mousePosition.x, Input.mousePosition.y, 80, 30), "F 开/关灯");
		}
	}

}
