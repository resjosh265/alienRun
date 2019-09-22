using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

	public static int score;
	public Image[] _score;
	public Sprite[] scoreImage;


	void Start(){
		score = 0;
	}
	// Update is called once per frame
	void FixedUpdate () {
		//get only the place of the ones, tens, hundreds or thousands
		int ones = score % 10;
		int tens = (score % 100) / 10;
		int hundreds = (score % 1000) / 100;
		int thousands = score / 1000;

		//constantly keep track of score and display on gui
		_score [0].sprite = ScoreTexturePlacement (ones);
		_score [1].sprite = ScoreTexturePlacement (tens);
		_score [2].sprite = ScoreTexturePlacement (hundreds);
		_score [3].sprite = ScoreTexturePlacement (thousands);
	}

	//place the sprite with the correct number
	public Sprite ScoreTexturePlacement(int unit) {

		if (unit == 0) {
			return scoreImage[0];
		}
		if (unit == 1) {
			return scoreImage[1];
		}
		if (unit == 2) {
			return scoreImage[2];
		}
		if (unit == 3) {
			return scoreImage[3];
		}
		if (unit == 4) {
			return scoreImage[4];
		}
		if (unit == 5) {
			return scoreImage[5];
		}
		if (unit == 6) {
			return scoreImage[6];
		}
		if (unit == 7) {
			return scoreImage[7];
		}
		if (unit == 8) {
			return scoreImage[8];
		}
		if (unit == 9) {
			return scoreImage[9];
        }

		return null;
	}
}
