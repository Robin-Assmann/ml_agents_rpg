using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

	public float value;

	public void ResetValue(){

		this.value = 1000.0f;
		GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		transform.GetChild (0).GetComponent<Text> ().text = value + "";
	}

	public void UpdateValue(){

		this.value = Random.value * 400.0f;
	}

	public void SetValue(int value){
	
		if (value > 0) {
			GetComponent<Image> ().color = new Color (0.0f, 1.0f, 0.0f, 0.5f);
			transform.GetChild (0).GetComponent<Text> ().text = value + "";
		}else {
			GetComponent<Image> ().color = new Color (1.0f, 0.0f, 0.0f, 0.5f);
		}
	}

}
