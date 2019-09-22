using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour {
	public GameObject saw, slime, fish;
	public bool genEnemies;

	public void PlaceEnemy(Vector2 cords){
		if (genEnemies == true) {
			float rng = Random.value;

			if (rng < 0.5f) {
				Instantiate (saw, new Vector3 (cords.x, cords.y + 1, -1), Quaternion.identity);
			}

			if (rng > 0.5f) {
				Instantiate (slime, new Vector3 (cords.x, cords.y + 1, -1), Quaternion.identity);
			}
		}

	}

	public void PlaceFish(Vector2 cords){
		if (genEnemies == true) {
			Instantiate (fish, new Vector3 (cords.x, cords.y, -1), Quaternion.identity);
		}
	}
}
