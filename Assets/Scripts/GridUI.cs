using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUI : MonoBehaviour {
	GameObject gridTileUI;
	Grid gridInstance;


	// Use this for initialization
	void Start () {
		gridTileUI = Resources.Load("UI/Components/UITile") as GameObject;
		Grid.Instance(); //Generate grid
		GetComponent<RectTransform>().sizeDelta = gridInstance.Dimensions(); //Set tranform of GridUI to fit grid

		//Generate grid UI tiles
		GenerateGrid();
	}
	
	//Fill GridUI canvas with grid tiles
	void GenerateGrid(){
		foreach(KeyValuePair<Vector2, Block> tile in gridInstance.grid()){
			GameObject tileInstance = Instantiate(gridTileUI, tile.Key, Quaternion.identity);
			tileInstance.transform.parent = this.transform;
		}
	}

	//Show GridUI
	public void Show(){
		gameObject.SetActive(true);
	}

	//Hide GridUI
	public void Hide(){
		gameObject.SetActive(false);
	}
}
