using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour {
	public class ExampleClass : MonoBehaviour {

		public void Exit(){
			Debug.Log ("MenuSceneLoaded");
			SceneManager.LoadScene ("MenuScene");

		}
	}
	
}
