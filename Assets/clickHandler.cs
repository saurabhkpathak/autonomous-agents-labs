using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickHandler : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TilingSystem map;

	// Use this for initialization
	void Start () {
		
	}

	void OnMouseDown() {
		Debug.Log ("clicked");
	}

	void OnMouseUp () {
		Debug.Log ("clicked");
		map.MoveUnitTo (tileX, tileY);
	}
		
	
	// Update is called once per frame
	void Update () {
		
	}
}
