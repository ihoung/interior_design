using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SizeSet : MonoBehaviour {

    public GameObject[] inputfields;
    public GameObject messagebox;
	public static int Long, Width, Height;

	public void OnSizeReset()
    {
		foreach (GameObject inputfield in inputfields) {
			inputfield.GetComponent<InputField> ().text = "";
		}
    }

    public void OnSizeLimit()
    {
        foreach (GameObject inputfield in inputfields)
        {
            if (inputfield.GetComponent<InputField>().text != "")
            {
                if (Convert.ToInt32(inputfield.GetComponent<InputField>().text) > 12)
                {
                    inputfield.GetComponent<InputField>().text = "12";
                }
            }            
        }
    }

    public void OnOKButtonClick()
    {
        bool check = true;
        for (int i = 0; i < inputfields.Length; i++)
        {
            if (inputfields[i].GetComponent<InputField>().text == "0" || inputfields[i].GetComponent<InputField>().text == "")
            {
                check = false;
                break;
            }
        }
        if (check == false)
        {
            messagebox.GetComponent<MessageBox>().MessageBoxShow("请输入限定范围的尺寸!");            
        }
        else
        {
            Long = Convert.ToInt32(inputfields[0].GetComponent<InputField>().text);
            Width = Convert.ToInt32(inputfields[1].GetComponent<InputField>().text);
            Height = Convert.ToInt32(inputfields[2].GetComponent<InputField>().text);

			this.GetComponent<GameObjectSwitch> ().OnObjectHide ();
			this.GetComponent<GameObjectSwitch> ().OnObjectShow ();
        }
    }

	public int GetLong(){
		return Long;
	}

	public int getWidth(){
		return Width;
	}

	public int GetHeight(){
		return Height;
	}

}
