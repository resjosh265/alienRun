using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour {

	public GameObject box, signRight, rock, cactus, coin, heart;
	public bool genObjects, genCoins;
	[Range (0.0f, 1.0f)]
	public float heartChance = 0.05f;

	public void PlaceObject(Vector2 cords, bool enemy){
		if (genObjects == true) {
			float rng = Random.value;


			if (rng < 0.2f) {
				if (enemy == false) {
					Instantiate (box, new Vector3 (cords.x, cords.y + 1, 1), Quaternion.identity);
				} else
					Instantiate (rock, new Vector3 (cords.x, cords.y + 1, 1), Quaternion.identity);
			}

			if (rng >= 0.2f && rng < 0.3f) {
				Instantiate (signRight, new Vector3 (cords.x, cords.y + 1, 1), Quaternion.identity);
			}

			if (rng >= 0.3f && rng < 0.7f) {
				Instantiate (rock, new Vector3 (cords.x, cords.y + 1, 1), Quaternion.identity);
			}

			if (rng >= 0.7f) {
				Instantiate (cactus, new Vector3 (cords.x, cords.y + 1.45f, 1), Quaternion.identity);
			}
		}
	}

	public void PlaceCoin(Vector2 cords, int ammount, int start){
		if (genCoins == true) {
			for (int a = 0; a < ammount; a++) {
				if (Random.value < heartChance) {
					Instantiate (heart, new Vector3 ((cords.x + (start - 1)) + a, cords.y + 1f, -1), Quaternion.identity);
				} else {
					Instantiate (coin, new Vector3 ((cords.x + (start - 1)) + a, cords.y + 1f, -1), Quaternion.identity);
				}
			}
		}

	}
}
