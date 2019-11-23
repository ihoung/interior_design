using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageBox : MonoBehaviour {

    public  GameObject textfield;
	public void MessageBoxShow(string text)
    {
        this.gameObject.SetActive(true);
        textfield.GetComponent<Text>().text = text;
    }

    public void MessageBoxClose()
    {
        this.gameObject.SetActive(false);
    }
}
