using UnityEngine;
using System.Collections;

public class ObjectControll : MonoBehaviour {

	public Camera DesignCamera;
	public GameObject ParentPlane;
	private Vector3 colliderPosition = new Vector3 (0, 0, 0);
	private Quaternion colliderRotation;
	private Quaternion startRotation;
	private Vector3 ScreenSpace, offset;
	private Vector3 previousPosition;

	public Shader SelectedShader;
	public Shader DefaultShader;

	// Use this for initialization
	void Start () {
		SelectedShader = Shader.Find ("Example/Rim");
		DefaultShader = Shader.Find ("Standard");
		colliderRotation = transform.rotation;
		startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

		SelectObject ();

		if (this.tag == "Selected") {
			switch (InteriorDesign.modevalue) {
			case 0:
				this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
				DragObject ();
				break;
			case 1:
				this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition ;
				//this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX;
				//this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationZ;
				RotateObject ();
				break;
			}
		}

	}

	void DragObject(){
		
		if (Input.GetMouseButtonDown (0)) {
			//将物体由世界坐标系转化为屏幕坐标系 ，由vector3 结构体变量ScreenSpace存储，以用来明确屏幕坐标系Z轴的位置  
			ScreenSpace = DesignCamera.WorldToScreenPoint (transform.position);  
			//完成了两个步骤，1由于鼠标的坐标系是2维的，需要转化成3维的世界坐标系，2只有三维的情况下才能来计算鼠标位置与物体的距离，offset即是距离  
			offset = transform.position - DesignCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));
		}

		//当鼠标左键按下时  
		if(Input.GetMouseButton(0))  
		{  			  
			Ray mRay = DesignCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit mHit;

			if (Physics.Raycast (mRay, out mHit)) 
			{
				GameObject obj = mHit.collider.gameObject;

				if (obj.tag == "Selected") {
					//得到现在鼠标的2维坐标系位置  
					Vector3 curScreenSpace =  new Vector3(Input.mousePosition.x,Input.mousePosition.y,ScreenSpace.z);     
					//将当前鼠标的2维位置转化成三维的位置，再加上鼠标的移动量  
					Vector3 CurPosition = DesignCamera.ScreenToWorldPoint(curScreenSpace)+offset;          
					//CurPosition就是物体应该的移动向量赋给transform的position属性        
					transform.position = CurPosition; 
				}
			}
		}  

		if (Input.GetMouseButtonUp (0)) {
			if (colliderPosition != new Vector3 (0, 0, 0)) {
				transform.position = colliderPosition;
			}
			colliderPosition = new Vector3 (0, 0, 0);
		}

	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag != "Plane") {
			colliderPosition = transform.position;
			colliderRotation = transform.rotation;
		}
	}

	void RotateObject(){
		
		if (Input.GetMouseButtonDown(0))  
		{  
			previousPosition = Input.mousePosition;  
		} 

		if (Input.GetMouseButton(0))  
		{  
			offset = Input.mousePosition - previousPosition;  

			if (offset.x > 0) {
				transform.Rotate (Vector3.up, offset.magnitude, Space.World);
			} else {
				transform.Rotate (Vector3.up, -offset.magnitude, Space.World);
			}

			previousPosition = Input.mousePosition;  
		}  

		if (Input.GetMouseButtonUp (0)) {
			if (colliderRotation != startRotation) {
				transform.rotation = colliderRotation;
			}
			colliderRotation = startRotation;
		}

	}

	void SelectObject(){
		if (Input.GetMouseButtonDown (1)) 
		{
			Ray mRay = DesignCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit mHit;

			if (Physics.Raycast (mRay, out mHit)) 
			{
				GameObject obj = mHit.collider.gameObject;

				if (obj.tag == "OperationEnabled") {
					GameObject preObj = GameObject.FindGameObjectWithTag ("Selected");
					if (preObj) {
						preObj.tag = "OperationEnabled";
						Renderer[] prerenderers = preObj.GetComponentsInChildren<MeshRenderer> ();
						foreach (Renderer prerenderer in prerenderers) {
							prerenderer.material.shader = DefaultShader;
						}
					}
					obj.tag = "Selected";
					Renderer[] renderers = obj.GetComponentsInChildren<MeshRenderer> ();
					foreach (Renderer renderer in renderers) {
						renderer.material.shader = SelectedShader;
					}
				}
			}
		}
			
	}

	public Vector3 GetOffset(){
		Vector3 offset;
		offset = ParentPlane.transform.InverseTransformDirection (transform.position - ParentPlane.transform.position);
		return offset;
	}
				
}
