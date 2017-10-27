using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void playBtn(){
		PlayerPrefs.SetInt ("Level",1);
		Application.LoadLevel ("Level1");
	}
	public void HomeBtn(){
		Application.LoadLevel ("MainMenu");
	}
	public void qBtn(){
		GameObject.Find ("QSelectPanel").GetComponent<AnimatorScript>().IsOpen = true;

	}
	public void exitBtn(){
		Application.Quit ();
	}
	//QSelectPanel Btn
	public void backBtnQSPanel(){
		GameObject.Find ("QSelectPanel").GetComponent<AnimatorScript>().IsOpen = false;
	}
	public void mcqBtn(){
		GameObject.Find ("MCPanel").GetComponent<AnimatorScript>().IsOpen = true;
	}
	public void shortQBtn(){
		GameObject.Find ("ShortPanel").GetComponent<AnimatorScript>().IsOpen = true;
	}
	public void TFQBtn(){
		GameObject.Find ("TFPanel").GetComponent<AnimatorScript>().IsOpen = true;
	}

	//Listener for mcPanel 
	public void submitBtnMC(){
		int qNo = PlayerPrefs.GetInt ("MCQNo");
		if (qNo >= 5) {
			backBtnMCPanel();
		}
		GameObject mc = GameObject.Find ("MCPanel");
		InputField q = mc.transform.GetChild (0).GetComponent<InputField> ();//.GetChild(2).GetComponent<Text> ();
		InputField a = mc.transform.GetChild (1).GetComponent<InputField> ();//.GetChild(2).GetComponent<Text> ();
		InputField b = mc.transform.GetChild (2).GetComponent<InputField> ();//.GetChild(2).GetComponent<Text> ();
		InputField c = mc.transform.GetChild (3).GetComponent<InputField> ();//.GetChild(2).GetComponent<Text> ();
		InputField d = mc.transform.GetChild (4).GetComponent<InputField> ();//.GetChild(2).GetComponent<Text> ();
		InputField ans = mc.transform.GetChild (5).GetComponent<InputField> ();//.GetChild(2).GetComponent<Text> ();

		if (q.text !="" && a.text !="" && b.text != "" && c.text !="" && d.text != "" && ans.text != "" ) {
			
			PlayerPrefs.SetString ("MCQ"+qNo,q.text);
			PlayerPrefs.SetString ("MCA"+qNo,a.text);
			PlayerPrefs.SetString ("MCB"+qNo,b.text);
			PlayerPrefs.SetString ("MCC"+qNo,c.text);
			PlayerPrefs.SetString ("MCD"+qNo,d.text);
			PlayerPrefs.SetString ("MCANS"+qNo,ans.text);

			mc.transform.GetChild (0).GetComponent<InputField>().text = "";
			mc.transform.GetChild (1).GetComponent<InputField>().text = "";
			mc.transform.GetChild (2).GetComponent<InputField>().text = "";
			mc.transform.GetChild (3).GetComponent<InputField>().text = "";
			mc.transform.GetChild (4).GetComponent<InputField>().text = "";
			mc.transform.GetChild (5).GetComponent<InputField>().text = "";

			qNo++;
			PlayerPrefs.SetInt("MCQNo",qNo);

		}



	}
	public void deleteMCQ(){
		int qNo = PlayerPrefs.GetInt ("MCQNo");
		for (int i = 0; i < qNo; i++) {
			PlayerPrefs.DeleteKey ("MCQ" + i);
			PlayerPrefs.DeleteKey ("MCA" + i);
			PlayerPrefs.DeleteKey ("MCB" + i);
			PlayerPrefs.DeleteKey ("MCC" + i);
			PlayerPrefs.DeleteKey ("MCD" + i);
			PlayerPrefs.DeleteKey ("MCANS" + i);
		}
		PlayerPrefs.SetInt ("MCQNo",0);
	}
	public void backBtnMCPanel(){
		GameObject.Find ("MCPanel").GetComponent<AnimatorScript>().IsOpen = false;
	}
	//btn for short panel 
	public void submitBtnShort(){
		int qNo = PlayerPrefs.GetInt ("ShortQNo");
		if (qNo >= 5) {
			backBtnShortPanel ();
			return;
		}
		GameObject sp = GameObject.Find ("ShortPanel");
		InputField q = sp.transform.GetChild (0).GetComponent<InputField> ();
		InputField a = sp.transform.GetChild (1).GetComponent<InputField> ();
		if (q.text != "" && a.text != "") {
			PlayerPrefs.SetString ("ShortQ"+qNo,q.text);
			PlayerPrefs.SetString ("ShortA"+qNo,a.text.ToUpper());
			sp.transform.GetChild (0).GetComponent<InputField>().text = "";
			sp.transform.GetChild (1).GetComponent<InputField>().text = "";
			qNo++;
			PlayerPrefs.SetInt("ShortQNo",qNo);
		}

	}
	public void deleteShort(){
		int qNo = PlayerPrefs.GetInt ("ShortQNo");
		for (int i = 0; i < qNo; i++) {
			PlayerPrefs.DeleteKey ("ShortQ"+i);
			PlayerPrefs.DeleteKey("ShortA"+i);

		}
		PlayerPrefs.SetInt ("ShortQNo",0);
	}
	public void backBtnShortPanel(){
		GameObject.Find ("ShortPanel").GetComponent<AnimatorScript>().IsOpen = false;
	}

	//btn for true false panel
	public void submitBtnTF(){
		int qNo = PlayerPrefs.GetInt ("TFQNo");
		if (qNo >= 5) {
			backBtnTFPanel ();
			return;
		}
		GameObject sp = GameObject.Find ("TFPanel");
		InputField q = sp.transform.GetChild (0).GetComponent<InputField> ();
		Toggle a = sp.transform.GetChild (1).GetComponent<Toggle>();
		if (q.text != "" ) {
			PlayerPrefs.SetString ("TFQ"+qNo,q.text);
			PlayerPrefs.SetString ("TFA"+qNo,""+a.isOn);
			sp.transform.GetChild (0).GetComponent<InputField>().text = "";
			qNo++;
			PlayerPrefs.SetInt("TFQNo",qNo);
		}

	}
	public void deleteTF(){
		int qNo = PlayerPrefs.GetInt ("TFQNo");
		for (int i = 0; i < qNo; i++) {
			PlayerPrefs.DeleteKey ("TFQ"+i);
			PlayerPrefs.DeleteKey("TFA"+i);

		}
		PlayerPrefs.SetInt ("TFQNo",0);
	}
	public void backBtnTFPanel(){
		GameObject.Find ("TFPanel").GetComponent<AnimatorScript>().IsOpen = false;
	}
}
