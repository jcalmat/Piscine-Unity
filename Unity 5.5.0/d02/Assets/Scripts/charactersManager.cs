using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactersManager : MonoBehaviour {

	public List<character> charactersList = new List<character>();

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (Input.GetKey(KeyCode.LeftControl))
				return;
			foreach (character character in charactersList) {
				character.GetComponent<AudioSource>().clip = character.walkSounds[Random.Range(0, character.walkSounds.Length)];
				character.GetComponent<AudioSource>().Play();

				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit && (hit.collider.gameObject.transform.tag == "orc_town" || hit.collider.gameObject.transform.tag == "orc" || hit.collider.gameObject.transform.tag == "orc_townhall"))
					character.currentEnemyAttacked = hit.collider.gameObject;
				else
					character.currentEnemyAttacked = null;
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//				Vector3 newDirection = new Vector3 (mousePosition.x - character.transform.position.x, mousePosition.y - character.transform.position.y, 0);
//				if (newDirection.x == 0 && newDirection.y > 0) {
//					character.activateTrigger ("walk_up");
//					character.changeDirection (character.characterDirection.UP);
//				} else if (newDirection.x == 0 && newDirection.y < 0) {
//					character.activateTrigger ("walk_down");
//					character.changeDirection (character.characterDirection.DOWN);
//				} else if (newDirection.x < 0 && newDirection.y == 0) {
//					character.activateTrigger ("walk_left");
//					character.changeDirection (character.characterDirection.LEFT);
//				} else if (newDirection.x > 0 && newDirection.y == 0) {
//					character.activateTrigger ("walk_right");					
//					character.changeDirection (character.characterDirection.RIGHT);
//				} else if (newDirection.x < 0 && newDirection.y > 0) {
//					character.activateTrigger ("walk_up_left");
//					character.changeDirection (character.characterDirection.UP_LEFT);
//				} else if (newDirection.x > 0 && newDirection.y > 0) {
//					character.activateTrigger ("walk_up_right");
//					character.changeDirection (character.characterDirection.UP_RIGHT);
//				} else if (newDirection.x < 0 && newDirection.y < 0) {
//					character.activateTrigger ("walk_down_left");
//					character.changeDirection (character.characterDirection.DOWN_LEFT);
//				} else if (newDirection.x > 0 && newDirection.y < 0) {
//					character.activateTrigger ("walk_down_right");
//					character.changeDirection (character.characterDirection.DOWN_RIGHT);
//				}

				character.changeEndpoint (new Vector3 (mousePosition.x, mousePosition.y, 0));
			} 
		} else if (Input.GetMouseButtonDown(1)) {
			foreach (character character in charactersList) {
					if(character.isSelected)
						character.isSelected = false;
			}
				charactersList.Clear();
		}
	}
}
