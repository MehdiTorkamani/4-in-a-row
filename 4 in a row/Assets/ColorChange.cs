using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {

	public float a = 0.05f;
	public bool goUp;
	Color test = new Color ();
	public float x = 1;

	// Use this for initialization
	void Start () {
		goUp = true;
		test = gameObject.GetComponent<SpriteRenderer> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		test.a = (80-x) / 80;
		gameObject.GetComponent<SpriteRenderer> ().color = test;

	
		if (x <= 0) {
			goUp = true;
		}

		if(goUp)
		{
			x++;
		}
			
		if (x >= 80) {
			goUp = false;
		}

		if(goUp == false){
			x--;
		}
	}
}
