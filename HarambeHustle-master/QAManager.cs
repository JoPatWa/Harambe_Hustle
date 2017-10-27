using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class QAManager : MonoBehaviour {
	private string ans = "";
	int num = 0;
	public Sprite[] resultSprites = new Sprite[2];
	// Use this for initialization
	void Start () {
		
		StartCoroutine("hudai");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void gotoNext(){
		int level = PlayerPrefs.GetInt ("Level");
		if (level == 1) {
			PlayerPrefs.SetInt ("Level", 2);
			Application.LoadLevel ("Level2");
		} else if (level == 2) {
			PlayerPrefs.SetInt ("Level", 3);
			Application.LoadLevel ("Level3");
		} else {
			Application.LoadLevel ("EndDemoScene");
		}
	}
	IEnumerator hudai(){
		yield return new WaitForEndOfFrame ();
		//shortQ ();
		int rand = Random.Range(0,3);
		if (rand == 0) {
			mcq ();
		} else if (rand == 1) {
			shortQ ();
		} else {
			TFQ ();
		}

	}
	void mcq(){
		GameObject.Find ("MCPanel").GetComponent<AnimatorScript> ().IsOpen = true;
		int qNo = PlayerPrefs.GetInt ("MCQNo",0);
		if (qNo == 0) {
			gotoNext ();
			return;
		}
		if (num >= qNo) {
			num = 0;
			gotoNext ();
			return;
		}
		//Debug.Log ("qno "+qNo);
		//MCQ"+qNo
		GameObject mc = GameObject.Find ("MCPanel");
		Text q = mc.transform.GetChild (0).GetComponent<Text> ();
		Text a = mc.transform.GetChild (1).GetChild(0).GetComponent<Text> ();
		Text b = mc.transform.GetChild (2).GetChild(0).GetComponent<Text> ();
		Text c = mc.transform.GetChild (3).GetChild(0).GetComponent<Text> ();
		Text d = mc.transform.GetChild (4).GetChild(0).GetComponent<Text> ();
		q.text = PlayerPrefs.GetString ("MCQ"+ num);
		a.text = PlayerPrefs.GetString ("MCA"+ num);
		b.text = PlayerPrefs.GetString ("MCB"+ num);
		c.text = PlayerPrefs.GetString ("MCC"+ num);
		d.text = PlayerPrefs.GetString ("MCD"+ num);

		ans = PlayerPrefs.GetString ("MCANS"+num).ToUpper(); 
		num++;
	}

	public void ABtn(){
		if (ans == "A") {
			mcq ();
			correctAns ();
			Debug.Log ("correctAns");
		}else
			wrongAns ();
	}
	public void BBtn(){
		if (ans == "B") {
			mcq ();
			correctAns ();
			Debug.Log ("correctAns");
		}else
			wrongAns ();
	}
	void correctAns(){
		Image rightImg =  GameObject.Find ("ResultImage").GetComponent<Image>();
		rightImg.sprite = resultSprites [1];
		rightImg.GetComponent<AnimatorScript> ().IsOpen = true;
	}
	void wrongAns(){
		Image rightImg =  GameObject.Find ("ResultImage").GetComponent<Image>();
		rightImg.sprite = resultSprites [0];
		rightImg.GetComponent<AnimatorScript> ().IsOpen = true;
	}
	public void CBtn(){
		if (ans == "C") {
			mcq ();
			correctAns ();
			Debug.Log ("correctAns");
		}else
			wrongAns ();
	}
	public void DBtn(){
		if (ans == "D") {
			mcq ();
			correctAns ();
			Debug.Log ("correctAns");
		} else
			wrongAns ();
	}
	//short ans panel 

	void shortQ(){
		GameObject.Find ("ShortPanel").GetComponent<AnimatorScript> ().IsOpen = true;
		int qNo = PlayerPrefs.GetInt ("ShortQNo",0);
		if (qNo == 0) {
			
			gotoNext ();
			return;
		}
		if (num >= qNo) {
			
			num = 0;
			gotoNext ();
			return;
		}
		//Debug.Log ("qno "+qNo);
		//MCQ"+qNo
		GameObject mc = GameObject.Find ("ShortPanel");
		Text q = mc.transform.GetChild (0).GetComponent<Text> ();
		q.text = PlayerPrefs.GetString ("ShortQ"+num);
		ans = PlayerPrefs.GetString ("ShortA"+num).ToUpper();
		num++;
	}

	public void submitBtnShort(){
		GameObject mc = GameObject.Find ("ShortPanel");
		InputField b = mc.transform.GetChild (1).GetComponent<InputField> ();
		if (b.text.ToUpper() == ans) {
			correctAns ();
			shortQ ();
		}else
			wrongAns ();
	}
	void TFQ(){
		GameObject.Find ("TFPanel").GetComponent<AnimatorScript> ().IsOpen = true;
		int qNo = PlayerPrefs.GetInt ("TFQNo",0);
		if (qNo == 0) {
			gotoNext ();
			return;
		}
		if (num >= qNo) {

			num = 0;
			gotoNext ();
			return;
		}
		//Debug.Log ("qno "+qNo);
		//MCQ"+qNo
		GameObject mc = GameObject.Find ("TFPanel");
		Text q = mc.transform.GetChild (0).GetComponent<Text> ();
		q.text = PlayerPrefs.GetString ("TFQ"+num);
		ans = PlayerPrefs.GetString ("TFA"+num).ToUpper();
		num++;
	}
		
	public void trueBtn(){
		if ("TRUE" == ans) {
			correctAns ();
			TFQ ();
		} else
			wrongAns ();
	}
	public void falseBtn(){

		if ("FALSE" == ans) {
			correctAns ();
			TFQ ();
		} else
			wrongAns ();
	}
}
