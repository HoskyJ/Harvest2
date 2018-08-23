using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//Singleton class
public class Grid: MonoBehaviour {
	private static Grid instance;
	private int xSize = 12;
	private int zSize = 12;
	public float tileSize = 0.7337835f;
	Dictionary<Vector2, Block> gridData;

	//Create Grid on construction
	private Grid(){
		GenerateGrid(xSize, zSize);
	}

	//Only allow one isntance of this class to exist
	public static Grid Instance(){
		if(instance == null){
			instance = new Grid();
			return instance;
		}
		else{
			return instance;
		}
	}

	//Generate grid which stores Tile's and their position. CAN ONLY BE EVEN NUMBERS ATM, FIX
	private void GenerateGrid(int width, int height) {
		gridData = new Dictionary<Vector2, Block>();
		//heightBound = ((height-2) / 2) * tileSize; //First two deducts 2 by 2 default tile
		//widthBound = ((width-2) / 2) * tileSize; //First two deducts 2 by 2 default tile

		for(int y = height/2; y >= -height/2 + 1; y--){
			gridData.Add(new Vector3(0 - (tileSize/2), 0, y*-tileSize + (tileSize/2)), null); //Center
			for(int x = 1; x <= width/2; x++){
				gridData.Add(new Vector3(+tileSize * x + (-tileSize/2), 0, y*-tileSize + (tileSize/2)), null); //Right
			}
			for(int x = 1; x <= (width/2) - 1; x++){
				gridData.Add(new Vector3(-tileSize * x + (-tileSize/2), 0, y*-tileSize + (tileSize/2)), null); //Left
			}
		}
	}

	public Dictionary<Vector2, Block> grid(){
		return gridData;
	}

	//Add block to the grid matching with each positions.
	public void AddBlock(Block block){

	}

	//Determine if block can be block at current location
	public bool isPlaceable(Block block){
		return true;
	}

	//Return the boundaries of the grid. Useful for checking placement.
	public Vector3 Bounds(){
		return new Vector3(0,0,0);
	}

	//Used for setting gridUI rect size
	public Vector2 Dimensions(){
		float gridWidth = xSize * tileSize;
		float gridDepth = zSize * tileSize;
		return new Vector2(gridWidth, gridDepth);
	}

	public int width(){
		return xSize;
	}

	public int depth(){
		return zSize;
	}
}
