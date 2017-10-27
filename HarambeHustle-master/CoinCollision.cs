using UnityEngine;
using System.Collections;

public class CoinCollision : MonoBehaviour {

	public int pointsToAdd;

	public AudioSource coinSoundEffect;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Player> () == null) {

			return;
		}
			

		ScoreManager.AddPoints (pointsToAdd);

		coinSoundEffect.Play ();
		Destroy (gameObject);
	}
}
