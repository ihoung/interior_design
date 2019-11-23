using UnityEngine;
using System.Collections;

public class OutdoorLightControll : MonoBehaviour {

	public Light outdoorLight;
	public GameObject scene;

	private float preValue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnOutdoorLightControll(float value){
		outdoorLight.gameObject.transform.RotateAround (scene.transform.position, scene.transform.forward, value - preValue);
		preValue = value;
	}

	public void OnOutdoorLightEnabled(bool flag){
		outdoorLight.gameObject.SetActive (flag);
	}

}
