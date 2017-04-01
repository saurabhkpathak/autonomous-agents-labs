using UnityEngine;

public class clickHandler : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TilingSystem map;

	void OnMouseUp () {
        Debug.Log("click");
        map.MoveUnitTo((int)GetComponent<Transform>().position.x, (int)GetComponent<Transform>().position.y);
    }
}
