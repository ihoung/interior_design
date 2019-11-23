using UnityEngine;
using System.Collections;

public class InteractionDetection : MonoBehaviour {

	public GameObject Character;
	private Quaternion startRotation;

	// Use this for initialization
	void Start () {
		startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Character.transform.TransformPoint (new Vector3 (0, 0, 9.0f));
		transform.rotation = Character.transform.rotation * startRotation;
	}

}
