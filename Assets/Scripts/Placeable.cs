using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Placeable : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler {

	Grid grid;
	GridUI gridUI;
	Block block;

	public GameObject blockPrefab;
	GameObject draggedObject;
	GameObject iconPrefab;
	Sprite blockIcon;
	bool blockPrefabIsSpawned = false;

	private void Start() {
		grid = Grid.Instance();
		gridUI = GameObject.FindGameObjectWithTag("GridUI").GetComponent<GridUI>();
		blockIcon = gameObject.GetComponent<Image>().sprite;
		iconPrefab = Resources.Load("EmptyUIItem") as GameObject;
	}

	//Snap block placement to grid.
	void SnapToGrid(){

	}

	public void OnBeginDrag(PointerEventData eventData){
		gridUI.Show();
	}

	public void OnDrag(PointerEventData eventData){
		RectTransform invPanel = transform.parent as RectTransform;

		//Check whether mouse is outside of placement UI. If show, change drag from icon hover to a block/
		if(RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition)){
			//Mouse is inside of placement UI
			draggedObject = Instantiate(iconPrefab);
			draggedObject.GetComponent<Image>().sprite = blockIcon;
			draggedObject.transform.parent = this.transform;
		}
		else{
			//Mouse is outside of placement UI
			if(!blockPrefabIsSpawned){
				Destroy(draggedObject);
				draggedObject = Instantiate(blockPrefab, new Vector3(0,0,0), Quaternion.identity);
				block = blockPrefab.GetComponent<Block>();
				blockPrefabIsSpawned = true;
			}
		}

		//Move dragged object with mouse location
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		draggedObject.transform.position = pos;
	}

	public void OnEndDrag(PointerEventData eventData){
		if(grid.isPlaceable(block)){
			//Add to grid
			grid.AddBlock(block);
		}
		else{
			//Destroy gameObject
			Destroy(blockPrefab);
		}
		gridUI.Hide();
	}

}
