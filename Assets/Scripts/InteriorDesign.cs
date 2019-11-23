using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InteriorDesign : MonoBehaviour {

	public Camera[] planecameras;
	public GameObject[] planes;
	public GameObject[] addobjectpanels;
	private int dropdownvalue;
	private int Long,Width,Height;
	public static int modevalue;
	public Camera viewerCamera;

	const int pieceSize = 10;

	void OnEnable () {
		GameObject.Find ("PlaneDropdown").GetComponent<Dropdown> ().value = 0;
		GameObject.Find ("ModeDropdown").GetComponent<Dropdown> ().value = 0;
		dropdownvalue = 0;
		modevalue = 0;
		Long = SizeSet.Long;
		Width = SizeSet.Width;
		Height = SizeSet.Height;
		planecameras [0].gameObject.SetActive (true);
		viewerCamera.gameObject.SetActive (true);
		for (int i = 0; i < planes.Length; i++)
			OnSetSize (i);
	}

	void Start(){
		modevalue = 0;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void OnPlaneSwitch (int value){
		dropdownvalue = value;
		foreach (Camera planecamera in planecameras)
			planecamera.gameObject.SetActive (false);
		switch (dropdownvalue) {
		case 0:
			planecameras [0].gameObject.SetActive (true);
			break;
		case 1:
			planecameras [1].gameObject.SetActive (true);
			break;
		case 2:
			planecameras [2].gameObject.SetActive (true);
			break;
		case 3:
			planecameras [3].gameObject.SetActive (true);
			break;
		case 4:
			planecameras [4].gameObject.SetActive (true);
			break;
		case 5:
			planecameras [5].gameObject.SetActive (true);
			break;
		}
	}

	public void OnSizeSetButtonClick(){
		foreach (Camera planecamera in planecameras) {
			planecamera.gameObject.SetActive (false);
		}
		viewerCamera.gameObject.SetActive (false);
		this.GetComponent<GameObjectSwitch> ().OnObjectHide ();
		this.GetComponent<GameObjectSwitch> ().OnObjectShow ();
	}

	public void OnSetSize(int i){
		GameObject targetPlane = planes [i].transform.FindChild ("Plane").gameObject;
		switch (i) {
		case 0:
		case 1:
			planes [i].transform.localScale = new Vector3 (Long, 1, Width);
			PlaneArray (Long, Width, targetPlane);
			break;
		case 2:
		case 3:
			planes [i].transform.localScale = new Vector3 (Width, 1, Height);
			PlaneArray (Width, Height, targetPlane);
			break;
		case 4:
		case 5:
			planes [i].transform.localScale = new Vector3 (Long, 1, Height);
			PlaneArray (Long, Height, targetPlane);
			break;
		}
	}

	void PlaneArray(int width,int height,GameObject targetPlane){
		List<Vector3> points = new List<Vector3> ();
		Vector3 startPoint = new Vector3 (targetPlane.transform.position.x - (width - 1) * pieceSize / 2, targetPlane.transform.position.y, targetPlane.transform.position.z - (height - 1) * pieceSize / 2);
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				points.Add (new Vector3 (startPoint.x + i * pieceSize, targetPlane.transform.position.y, startPoint.z + j * pieceSize));
			}
		}

		foreach (Vector3 point in points) {
			GameObject obj = GameObject.CreatePrimitive (PrimitiveType.Plane);
			obj.tag = "Plane";
			obj.transform.position = point;
			obj.transform.parent = targetPlane.transform;
		}
	}

	public void OnAddButtonClick(){
		foreach (GameObject addobjectpanel in addobjectpanels)
			addobjectpanel.SetActive (false);
		addobjectpanels [dropdownvalue].SetActive (true);
	}

	public void OnModeChange(int value){
		modevalue = value;
	}

	public void OnDeleteButtonClick(){
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Selected");
		foreach (GameObject obj in objs)
			Destroy (obj);
	}

}