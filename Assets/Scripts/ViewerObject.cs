using UnityEngine;
using System.Collections;

public class ViewerObject : MonoBehaviour {

	public GameObject linkedObject;
	public GameObject parentPlane;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ObjectFollow ();
	}

	void ObjectFollow(){
		if (linkedObject.tag == "Selected") {
			this.tag = "Selected";
		} else {
			this.tag = "OperationEnabled";
		}

		Vector3 offset = linkedObject.GetComponent<ObjectControll> ().GetOffset ();
		transform.position = parentPlane.transform.position + parentPlane.transform.TransformDirection (offset);
		transform.rotation = parentPlane.transform.rotation * linkedObject.transform.rotation;
	}

}
