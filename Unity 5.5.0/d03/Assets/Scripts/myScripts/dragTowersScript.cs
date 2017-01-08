using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class dragTowersScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public GameObject itemBeingDragged;
	public towerScript tower;
	public gameManager manager;
	public bool isDraggable = true;

	public void OnBeginDrag(PointerEventData eventData) {
		if (isDraggable) {
			itemBeingDragged = Instantiate (gameObject, transform);
			//		itemBeingDragged = gameObject;
		}
	}

	public void OnDrag(PointerEventData eventData) {
		if (isDraggable) {
			Debug.Log (itemBeingDragged.transform.position);
			itemBeingDragged.transform.position = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		}
	}

	public void OnEndDrag(PointerEventData eventData) {
		if (isDraggable) {
			if (itemBeingDragged != null) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit && hit.collider.transform.tag == "empty") {
					Debug.Log (hit.collider.gameObject.transform.tag);
					manager.playerEnergy -= tower.energy;
					Instantiate (tower, hit.collider.gameObject.transform.position, Quaternion.identity);			
				}
				Destroy (itemBeingDragged);
				itemBeingDragged = null;
			}
		}
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (manager.playerEnergy - tower.energy < 0) {
			isDraggable = false;
			GetComponent<Image> ().color = Color.gray;
		} else {
			isDraggable = true;
			GetComponent<Image> ().color = Color.white;
		}
	}
}
