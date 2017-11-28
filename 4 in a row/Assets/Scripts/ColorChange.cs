using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {

	float a;
	float x = 1;
	bool goUp = true;
	Color test = new Color ();

	void Start () {
		test = gameObject.GetComponent<SpriteRenderer> ().color;
	}

	//Color change could also be achieved with Coroutine
	void Update () {
		test.a = (80-x) / 80;
		gameObject.GetComponent<SpriteRenderer> ().color = test;

		if (x <= 0)
			goUp = true;
		else if (x >= 80)
			goUp = false;

		if (goUp)
			x++;
		else
			x--;
	}
}
