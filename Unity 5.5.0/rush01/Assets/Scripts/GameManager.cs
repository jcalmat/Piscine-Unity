using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager gm;
	public mayaScript maya;
	public GameObject[] weapons;
	private bool open_inventory = false;
	public RectTransform inventory;
	public float transition;
	public GameObject Tooltip;
	public Image image_weapon;
	public Text name_weapon;
	public Text stat_1;
	public Text stat_2;
	public Text rare;
	public GameObject main_weapon;
	private float increase;
	public GameObject upgradeButton;
	public GameObject statsPanel;
	public GameObject skillTree;

	// Use this for initialization
	void Start () {
		if (gm == null)
			gm = this;
		Tooltip.SetActive(false);
		// main_weapon = GameObject.Find("Main_Weapon_Cell");
	}
	
	// Update is called once per frame
	void Update () {

		check_open_inventory();
		check_mouse_tooltip();

		maya_main_weapon();
		// Debug.Log("between : " + maya.minDamage + " and " + maya.maxDamage);

		if (Input.GetKeyDown (KeyCode.P)) {
			GameObject go = Instantiate (weapons [Random.Range (0, weapons.Length)], maya.transform.position, Quaternion.identity);
			go.GetComponent<items> ().DMG = Random.Range (3 * maya.GetComponent<mayaScript> ().Level - 2, 3 * maya.GetComponent<mayaScript> ().Level + 2);
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			maya.Level++;
			maya.spendPoints += 5;
		}

		if (maya.spendPoints > 0)
			upgradeButton.SetActive (true);
		else
			upgradeButton.SetActive (false);

		if (Input.GetKey (KeyCode.Tab))
			statsPanel.SetActive (true);
		else
			statsPanel.SetActive (false);

		if (Input.GetKeyDown (KeyCode.N))
			skillTree.SetActive (!skillTree.activeSelf);
	}

	public int getMayaLevel() {
		return maya.Level;
	}

	void maya_main_weapon () {
		if (main_weapon.transform.childCount > 0) {
			// Debug.Log(main_weapon.transform.GetChild(0));
			foreach (GameObject weapon in maya.inventory) {
				if (weapon.GetInstanceID().ToString() == main_weapon.transform.GetChild(0).name) {
					// Debug.Log(weapon.name);
					maya.minDamage = maya.minDamage_base + weapon.GetComponent<items>().DMG;
					maya.maxDamage = maya.maxDamage_base + weapon.GetComponent<items>().DMG;
				}
			}
			// maya.minDamage = maya.minDamage_base + main_weapon.transform.GetChild(0).GetComponent<items>().DMG;
			// maya.maxDamage = maya.maxDamage_base + main_weapon.transform.GetChild(0).GetComponent<items>().DMG;
			// boost maya rapidité
		}
		else {
			maya.minDamage = maya.minDamage_base;
			maya.maxDamage = maya.maxDamage_base;
		}
	}

	void check_open_inventory () {
		if (Input.GetKeyDown (KeyCode.B)) {
			transition = 1;
			if (open_inventory == false) {
				open_inventory = true;
			}
			else {
				open_inventory = false;
			}
		}
		if (open_inventory == true)	
			inventory.gameObject.SetActive(true);
		else
			inventory.gameObject.SetActive(false);
	}

	void check_mouse_tooltip() {
		bool no_std_ray = false;

		// if (Input.GetMouseButtonDown(0) == false) //pas besoin de raycast quand on cours
		// {
			// if (open_inventory == true) {
				PointerEventData pointer = new PointerEventData(EventSystem.current);
				pointer.position = Input.mousePosition;
				List<RaycastResult> rayresult = new List<RaycastResult>();
				EventSystem.current.RaycastAll(pointer, rayresult);
				if (rayresult.Count > 0) {
					// no_std_ray = true;
					// Debug.Log(rayresult[0].gameObject.tag);
					if (rayresult[0].gameObject.tag == "weapon_sprite") {
						match_weapon(rayresult[0].gameObject.name);
						Tooltip.SetActive(true);
						no_std_ray = true;
					}
					else
						Tooltip.SetActive(false);
				}
			// }
			if (no_std_ray == false) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
					if (hit.collider.gameObject.tag == "weapon") {
						name_weapon.text = hit.collider.gameObject.name;
						stat_1.text = hit.collider.gameObject.GetComponent<items>().DMG.ToString();
						what_is_color(hit.collider.gameObject.GetComponent<items>().quality);
						stat_2.text = hit.collider.gameObject.GetComponent<items>().ATTACKSPEED.ToString();
						image_weapon.sprite = hit.collider.gameObject.GetComponent<Image>().sprite;
						Tooltip.SetActive(true);
					}
					else
						Tooltip.SetActive(false);
				}
			}
		// }
	}

	void match_weapon(string nameID) {

		foreach (GameObject weapon in maya.inventory) {
			if (weapon.GetInstanceID().ToString() == nameID) {
				name_weapon.text = weapon.name;
				stat_1.text = weapon.GetComponent<items>().DMG.ToString();
				what_is_color(weapon.GetComponent<items>().quality);
				stat_2.text = weapon.GetComponent<items>().ATTACKSPEED.ToString();
				image_weapon.sprite = weapon.GetComponent<Image>().sprite;
			}
		}

	}

	void what_is_color (int quality) {
		if (quality == 1) {
			 name_weapon.color = Color.red;
			 rare.text = "Legendary";
			 rare.color = Color.red;
		}
		else if (quality == 2) {
			name_weapon.color = Color.magenta;
			rare.text = "Epic";
			rare.color = Color.magenta;
		}
		else if (quality == 3) {
			name_weapon.color = Color.blue;
			rare.text = "Rare";
			rare.color = Color.blue;
		}
		else if (quality == 4) {
			name_weapon.color = Color.green;
			rare.text = "Superior";
			rare.color = Color.green;
		}
		else {name_weapon.color = Color.white;
			rare.text = "Commun";
			rare.color = Color.white;
		}
	}
}
