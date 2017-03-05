using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Lean; //from Unity asset "LeanPool" - freely available in the Asset Store; used here for object pooling

public class TilingSystem : MonoBehaviour {

	[SerializeField]
	private GameObject[] TilePrefabs;

	public float getTileSize() {
		return TilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x;
	}

//	public List<TileSprite> TileSprites;
//	public Vector2 MapSize;
//	public Sprite DefaultImage;
//	public GameObject TileContainerPrefab;
//	public Vector2 CurrentPosition;
//	public Vector2 ViewPortSize;
//
//	private TileSprite[,] _map;
//	private GameObject _tileContainer;
//	private List<GameObject> _tiles = new List<GameObject>();

	//create a map of size MapSize of unset tiles
	private void DefaultTiles() {
		
		//....
		
	}

	//set the tiles of the map to what is specified in TileSprites
	private void SetTiles() {

		string[] gridData = new string[] {
			"0101100010101",
			"0000010101010",
			"0001000101001",
			"0100110011000",
			"1000100000010",
			"0101010100101"
		};

		int mapX = gridData [0].ToCharArray ().Length;
		int mapY = gridData.Length;

		Vector3 gridStart = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height));

		for (int y = 0; y < mapY; y++) {
			char[] rowTiles = gridData [y].ToCharArray ();
			for (int x = 0; x < mapX; x++) {
				PlaceTile (rowTiles[x].ToString(), x, y, gridStart);
			}
		}
	}

	private void PlaceTile(string tileType, int x, int y, Vector3 gridStart) {
		GameObject newTile = Instantiate (TilePrefabs[int.Parse(tileType)]);
		newTile.transform.position = new Vector3 (gridStart.x + (getTileSize() * x), gridStart.y - (getTileSize() * y), 0);
	}

//	private void AddTilesToMap() {
//
//		_tileContainer = LeanPool.Spawn (TileContainerPrefab);
//
//		var tileSize = 2f;
//		var viewOffsetX = ViewPortSize.x;
//		var viewOffsetY = ViewPortSize.y;
//
//		for (var y = -viewOffsetY; y < viewOffsetY; y++) {
//			for (var x = -viewOffsetX; x < viewOffsetX; x++) {
//				var tX = x * tileSize;
//				var tY = y * tileSize;
//
//				var iX = x + CurrentPosition.x;
//				var iY = y + CurrentPosition.y;
//
//				if (iX < 0)
//					continue;
//				if (iY < 0)
//					continue;
//				if (iX > MapSize.x - 2)
//					continue;
//				if (iY > MapSize.y - 2)
//					continue;
//
//				var tile = LeanPool.Spawn (TilePrefab);
//				tile.transform.position = new Vector3 (tX, tY, 0);
//				tile.transform.SetParent (_tileContainer.transform);
//				
//				//set an image for the tile - do this when you create your tile prefabs (for shack, mountains, ...)
//				var renderer = tile.GetComponent<SpriteRenderer> ();
//				/renderer.sprite = _map [(int)x + (int)CurrentPosition.x, (int)y + (int)CurrentPosition.y].TileImage;
//				
//				_tiles.Add (tile);
//			}
//		}
//	}

	public void Start() {
		//_map = new TileSprite[(int)MapSize.x, (int)MapSize.y];
		SetTiles ();
		//AddTilesToMap ();
	}

}
