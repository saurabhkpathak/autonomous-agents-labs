using UnityEngine;

public class clickHandler : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TilingSystem map;

	void OnMouseUp () {
        //map.MoveUnitTo (tileX, tileY);
        Debug.Log("click");
		map.GeneratePathTo ((int)GetComponent<Transform>().position.x, (int)GetComponent<Transform>().position.y);
	}
}
