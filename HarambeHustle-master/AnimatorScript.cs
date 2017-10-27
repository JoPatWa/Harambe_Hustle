using UnityEngine;
using System.Collections;

public class AnimatorScript : MonoBehaviour {
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame

	public bool IsOpen{
		get{return animator.GetBool("IsOpen");}
		set{animator.SetBool("IsOpen", value);}
	}


	public void animFinished(){
		IsOpen = false;
	}
}
