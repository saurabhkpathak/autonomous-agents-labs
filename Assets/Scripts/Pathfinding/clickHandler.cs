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
		map.GeneratePathTo ((int)this.GetComponent<Transform>().position.x, (int)this.GetComponent<Transform>().position.y);
	}
}
