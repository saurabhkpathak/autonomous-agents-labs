using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traverseTile : MonoBehaviour {

	public float speed = 5f;

	// Use this for initialization
	void Start () {
		Debug.Log ("started");
	}

	void OnMouseDown() {
		Debug.Log ("clicked");
	}

	void OnMouseUp () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		GameObject go = GameObject.Find ("Unit");
//		go.transform.position = new Vector2 (-3, 3);
	}
}
