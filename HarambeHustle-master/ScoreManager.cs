using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

	public static int score;

	Text text;

	void Start()
	{
		text = GetComponent<Text> ();
		score = 0;
	}

	void Update()
	{
		if (score < 0) {
			score = 0;
		}

		if (score == 39) {
			SceneManager.LoadScene ("QAScene");
			
		}
		if (score == 150) {
			SceneManager.LoadScene ("QAScene");
		}

		if (score == 171) {
			SceneManager.LoadScene ("QAScene");
		}
		text.text = "" + score;
	}

	public static void AddPoints(int pointsToAdd)
	{
		score += pointsToAdd;
	}

	public static void Reset()
	{
		score = 0;
	}
}
