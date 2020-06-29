using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour {
	public GameObject saw, slime, fish;
	[Range(0f, 1f)] public float sawSpawnPercent = 0.5f;
	public bool generateEnemies;

	public void PlaceEnemy(Vector2 coordinates){
		if (generateEnemies) {
			var rng = Random.value;

			if (rng < sawSpawnPercent) {
				Instantiate (saw, new Vector3 (coordinates.x, coordinates.y + 1, -1), Quaternion.identity);
			}

			if (rng > sawSpawnPercent) {
				Instantiate (slime, new Vector3 (coordinates.x, coordinates.y + 1, -1), Quaternion.identity);
			}
		}
	}

	public void PlaceFish(Vector2 cords){
		if (generateEnemies) {
			Instantiate (fish, new Vector3 (cords.x, cords.y, -1), Quaternion.identity);
		}
	}
}
