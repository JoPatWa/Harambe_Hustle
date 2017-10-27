using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Question[] questions;
	private static List<Question> unansweredQuestions;

	private Question currentQuestion;

	[SerializeField]
	private Text factText;

	[SerializeField]
	private Text trueAnswerText;

	[SerializeField]
	private Text falseAnswerText;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private float timeBetweenQuestions = 1f;


	void Start()
	{
		if (unansweredQuestions == null || unansweredQuestions.Count == 0) {
			
			unansweredQuestions = questions.ToList<Question>();
		}

		SetCurrentQuestion();
	}

	void SetCurrentQuestion()
	{
		int randomQuestionIndex = Random.Range (0, unansweredQuestions.Count);
		currentQuestion = unansweredQuestions [randomQuestionIndex];

		factText.text = currentQuestion.fact;

		if (currentQuestion.isTrue) {
			trueAnswerText.text = "Right!";
			falseAnswerText.text = "Wrong!";
		} 
		else 
		{
			trueAnswerText.text = "Wrong!";
			falseAnswerText.text = "Right!";
		}

	}

	//Allows us to wait a few seconds before transitioning.
	IEnumerator TransitionToNextQuestion()
	{
		unansweredQuestions.Remove (currentQuestion);

		yield return new WaitForSeconds (timeBetweenQuestions);

		//Restarts the scene.
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void UserSelectTrue()
	{
		animator.SetTrigger ("True");
		if (currentQuestion.isTrue) 
		{
			Debug.Log ("Correct!");
		} 
		else 
		{
			Debug.Log ("Wrong!");
		}

		//When starting IEnumerators you call them differently using StartCoroutine().
		StartCoroutine(TransitionToNextQuestion ());
	}

	public void UserSelectFalse()
	{
		animator.SetTrigger ("False");
		if (!currentQuestion.isTrue) 
		{
			Debug.Log ("Correct!");
		} 
		else 
		{
			Debug.Log ("Wrong!");
		}

		//When starting IEnumerators you call them differently using StartCoroutine().
		StartCoroutine(TransitionToNextQuestion ());

	}

	public void nextLevel2(){
		
		//PlayerPrefs.SetInt ("Level",2);
		//SceneManager.LoadScene("Level2");
		Application.LoadLevel("QAScene");
	}

	public void nextLevel3() {
		
		//PlayerPrefs.SetInt ("Level",3);
		//SceneManager.LoadScene ("Level3");
		Application.LoadLevel("QAScene");


	}
		
		
}
