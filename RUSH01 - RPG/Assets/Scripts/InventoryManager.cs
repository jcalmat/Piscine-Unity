using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	public mayaScript player;
	private GameObject[] slots;
	private GameObject[] current_slots;
	private int current_index = 0;
	private Image[] uiSprites;
	public Sprite test;
	public GameObject prefab_sprite;
	private List<GameObject> stuff;

	private bool stop = false;
	private int slot_busy = 0;
	private float cad = 0.0f;

	// Use this for initialization
	void Start () {
		slots = GameObject.FindGameObjectsWithTag("Slot");
	}
	
	// Update is called once per frame
	void Update () {
		check_delete_drag();
		if (slot_busy < player.inventory.Count) {
			add_inventory();
		}
	}

	void add_inventory() {
		int i = 0;
		foreach (GameObject weapon in player.inventory) {
			if (i == slot_busy) {
				if (!look_slotempty(weapon))
					Debug.Log("inventory is full");
			}
			i++;
		}
	}

	bool look_slotempty(GameObject weapon) {
		for (int y = 0; y < slots.Length; y++) {
			if (slots[y].transform.childCount == 0) {
				prefab_sprite.GetComponent<Image>().sprite = weapon.GetComponent<Image>().sprite;
				GameObject ret = Instantiate(prefab_sprite, slots[y].transform, false);
				ret.name = weapon.GetInstanceID().ToString();
				// Debug.Log(ret.name);
				// current_slots[current_index] = ret;
				// current_index
				slot_busy += 1;
				return true;
			}
		}
		return false;
	}

	void check_delete_drag() {
		// int id = 0;

		for (int i = 0; i < slots.Length; i++) {
			if (slots[i].GetComponent<DragAndDropCell>().delete_one == true) {
				if (delete_this(slots[i].GetComponent<DragAndDropCell>().deleted_object.name) == false)
					Debug.Log("error system");
				slots[i].GetComponent<DragAndDropCell>().delete_one = false;
				slot_busy -= 1;
			}
		}
	}

	bool delete_this(string nametodelete) {

		foreach (GameObject weapon in player.inventory) {
			if (weapon.GetInstanceID().ToString() == nametodelete) {
				// Debug.Log("remove ok");
				player.inventory.Remove(weapon);
				return true;
			}
		}
		return false;
	}
}
