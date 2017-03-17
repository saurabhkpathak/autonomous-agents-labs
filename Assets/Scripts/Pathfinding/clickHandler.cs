using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickHandler : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TilingSystem map;

	void OnMouseUp () {
		map.MoveUnitTo (tileX, tileY);
		//map.GeneratePathTo (tileX, tileY);
	}
}
