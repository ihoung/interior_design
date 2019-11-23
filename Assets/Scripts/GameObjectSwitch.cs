using UnityEngine;
using System.Collections;

public class GameObjectSwitch : MonoBehaviour {

    public GameObject[] objs_hide;
    public GameObject[] objs_show;

    public void OnObjectHide()
    {
		foreach (GameObject obj in objs_hide)
			obj.SetActive (false);
    }

    public void OnObjectShow()
    {
		foreach (GameObject obj in objs_show)
			obj.SetActive (true);
    }

	public void OnHideAndShow(bool flag){
		this.gameObject.SetActive (flag);
	}

}
