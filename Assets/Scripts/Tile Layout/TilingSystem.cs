using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Lean; //from Unity asset "LeanPool" - freely available in the Asset Store; used here for object pooling

public class TilingSystem : MonoBehaviour {

	public TileSprite[] TileSprites;
	public Vector2 MapSize;
	private TileSprite[,] _map;

	Node[,] graph;

	public GameObject selectedUnit;

	void GeneratePathfindingGraph() {
		graph = new Node[(int)MapSize.x, (int)MapSize.y];

		for (int i = 0; i < MapSize.x; i++) {
			for (int j = 0; j < MapSize.y; j++) {
				if (i > 0)
					graph [i, j].neighbours.Add (graph [i - 1, j]);
				if (i < MapSize.x - 1)
					graph [i, j].neighbours.Add (graph [i + 1, j]);
				if (j > 0)
					graph [i, j].neighbours.Add (graph [i, j - 1]);
				if (j < MapSize.y - 1)
					graph [i, j].neighbours.Add (graph [i, j + 1]);
			}
		}
	}

	public float getTileSize() {
		return TileSprites[0].tilePrefab.GetComponent<SpriteRenderer> ().sprite.bounds.size.x;
	}

//	public Sprite DefaultImage;
//	public GameObject TileContainerPrefab;
//	public Vector2 CurrentPosition;
//	public Vector2 ViewPortSize;
//	private GameObject _tileContainer;
//	private List<GameObject> _tiles = new List<GameObject>();

	//create a map of size MapSize of unset tiles
	private void DefaultTiles() {
		for (int i = 0; i < MapSize.x; i++) {
			for (int j = 0; j < MapSize.y; j++) {
				_map [i,j] = new TileSprite ();
			}
		}
	}

	private TileSprite getTile(Tiles tile) {
		foreach(TileSprite tileSprite in TileSprites) {
			if (tileSprite.tileType == tile)
				return tileSprite;
		}
		return TileSprites [0];
	}

	//set the tiles of the map to what is specified in TileSprites
	private void SetTiles() {
		_map [0, 1] = new TileSprite (0, Tiles.Mountains);
		_map [0, 4] = new TileSprite (0, Tiles.Mountains);
		_map [1, 2] = new TileSprite (0, Tiles.Mountains);
		_map [1, 1] = new TileSprite (0, Tiles.Mountains);
		_map [2, 5] = new TileSprite (0, Tiles.Mountains);
		_map [2, 1] = new TileSprite (0, Tiles.Mountains);
		_map [3, 3] = new TileSprite (0, Tiles.Mountains);
		_map [3, 5] = new TileSprite (0, Tiles.Mountains);
		_map [4, 1] = new TileSprite (0, Tiles.Mountains);
		_map [4, 4] = new TileSprite (0, Tiles.Mountains);
	}

	private void PlaceTile(TileSprite tileSprite, int x, int y, Vector3 gridStart) {
		GameObject newTile = Instantiate (getTile (tileSprite.tileType).tilePrefab);
		newTile.transform.position = new Vector3 (gridStart.x + (getTileSize() * x), gridStart.y - (getTileSize() * y), 0);
		if (tileSprite.tileType == Tiles.Plains) {
			clickHandler ch = newTile.GetComponent<clickHandler> ();
			ch.tileX = x;
			ch.tileY = y;
			ch.map = this;
		}
	}

	private void AddTilesToMap() {
		Vector3 gridStart = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height));

		for (int y = 0; y < MapSize.y; y++) {
			for (int x = 0; x < MapSize.x; x++) {
				PlaceTile (_map[x, y], x, y, gridStart);
			}
		}
	}

	public void MoveUnitTo(int x, int y) {
		selectedUnit.transform.position = new Vector2 (x, y);
	}

	public void Start() {
		_map = new TileSprite[(int)MapSize.x, (int)MapSize.y];
		DefaultTiles ();
		SetTiles ();
		AddTilesToMap ();
	}

}
