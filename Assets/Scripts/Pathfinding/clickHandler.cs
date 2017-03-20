using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickHandler : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TilingSystem map;

	void OnMouseUp () {
        //map.MoveUnitTo (tileX, tileY);
        Debug.Log("click");
        Debug.Log("x is: " + this.GetComponent<Transform>().position.x +
            " & y is: " + this.GetComponent<Transform>().position.y);
        map.GeneratePathTo (tileX, tileY);
	}
}
