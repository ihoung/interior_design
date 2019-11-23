using UnityEngine;
using System.Collections;

public class ResourceOperation : MonoBehaviour {

	public GameObject linkedObj;
	public Texture texture;
	public GameObject gotoDesignPlane;
	public GameObject gotoImpressionPlane;
	public Camera DesignCamera;
	private float StartHeight;

	private GameObject designObject;
	private GameObject viewerObject;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnAddObjectToDesignPlane(){
		designObject = Instantiate (linkedObj);
		BoxCollider collider = designObject.GetComponent<BoxCollider> ();
		Vector3 localScale = designObject.transform.localScale;
		StartHeight = Mathf.Max (collider.size.x * localScale.x, collider.size.y * localScale.y, collider.size.z * localScale.z) / 2;
		designObject.transform.position = new Vector3 (gotoDesignPlane.transform.position.x, StartHeight, gotoDesignPlane.transform.position.z);
		designObject.AddComponent<Rigidbody> ();
		designObject.AddComponent<ObjectControll> ().DesignCamera = DesignCamera;
		designObject.GetComponent<ObjectControll> ().ParentPlane = gotoDesignPlane;
	}

	public void OnAddObjectToImpressionPlane(){
		viewerObject = Instantiate (linkedObj);
		viewerObject.AddComponent<ViewerObject> ().linkedObject = designObject;
		viewerObject.GetComponent<ViewerObject> ().parentPlane = gotoImpressionPlane;
	}

	public void OnPlaneMaterialSet(){
		Renderer[] renderers = gotoDesignPlane.GetComponentsInChildren<Renderer> ();
		foreach (Renderer renderer in renderers) {
			renderer.material.mainTexture = texture;
		}

		Renderer[] renderers_ = gotoImpressionPlane.GetComponentsInChildren<Renderer> ();
		foreach (Renderer renderer in renderers_) {
			renderer.material.mainTexture = texture;
		}

	}

}
