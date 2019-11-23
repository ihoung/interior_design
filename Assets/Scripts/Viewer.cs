using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Viewer : MonoBehaviour {

	private int Long, Width, Height; 
	public GameObject scene;
	public GameObject[] targetPlanes;

	private float preHorizontalValue;
	private float preVerticalValue;
	private float preFocusValue;
	private float maxDistance;

	const int pieceSize = 10;

	// Use this for initialization
	void Start () {
		preHorizontalValue = 0f;
		preVerticalValue = 0f;
		preFocusValue = 0f;
		maxDistance = (transform.position - scene.transform.position).magnitude * 0.9f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSize(){
		Long = SizeSet.Long;
		Width = SizeSet.Width;
		Height = SizeSet.Height;
		scene.transform.localScale = new Vector3 (Long, Height, Width);

		for (int i = 0; i < targetPlanes.Length; i++) {
			switch (i) {
			case 0:
			case 1:
				PlaneArray (Long, Width, targetPlanes[i]);
				break;
			case 2:
			case 3:
				PlaneArray (Width, Height, targetPlanes[i]);
				break;
			case 4:
			case 5:
				PlaneArray (Long, Height, targetPlanes[i]);
				break;
			}
		}
			
	}

	void PlaneArray(int width,int height,GameObject targetPlane){
		List<Vector3> points = new List<Vector3> ();
		Vector3 startPoint = new Vector3 (-(width - 1) * pieceSize / 2, 0, -(height - 1) * pieceSize / 2);
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				points.Add (new Vector3 (startPoint.x + i * pieceSize, 0, startPoint.z + j * pieceSize));
			}
		}

		foreach (Vector3 point in points) {
			GameObject obj = GameObject.CreatePrimitive (PrimitiveType.Plane);
			Quaternion startRotation = obj.transform.rotation;
			obj.transform.position = targetPlane.transform.TransformPoint (new Vector3 (point.x / width, 0, point.z / height));
			obj.transform.rotation = targetPlane.transform.rotation * startRotation;
			obj.transform.parent = targetPlane.transform;
		}
	}


	public void OnHorizontalSliderValueChanged(float value){
		transform.RotateAround (scene.transform.position, scene.transform.up, value - preHorizontalValue);
		preHorizontalValue = value;
	}

	public void OnVerticalSliderValueChanged(float value){
		transform.RotateAround (scene.transform.position, transform.right, preVerticalValue - value);
		preVerticalValue = value;
	}

	public void OnFocusSliderValueChanged(float value){
		float distance = maxDistance * (value - preFocusValue);
		transform.position = Vector3.MoveTowards (transform.position, scene.transform.position, distance);
		preFocusValue = value;
	}

}
