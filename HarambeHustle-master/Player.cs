using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public delegate void DeadEventHandler();

public class Player : Character
{

    private static Player instance;

    public event DeadEventHandler Dead;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    private bool immortal = false;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float immortalTime;

    public Rigidbody2D MyRigidbody { get; set; }

    public bool Slide { get; set; }

    public bool Jump { get; set; }

    public bool OnGround { get; set; }

    public override bool IsDead
    {
        get
        {
            if (health <= 0)
            {
                OnDead();
            }
          
            return health <= 0;
        }
    }

    private Vector2 startPos;

    private bool tmpJump = false;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyRigidbody = GetComponent<Rigidbody2D>();
    
    }
	private bool leftPressed, jumpPressed, rightPressed = false;
    void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -14f)
            {
                Death();

            }
            HandleInput();
        }

		if (leftPressed) {
			Left ();
		}else if (rightPressed) {
			Right ();
		}

		/*if (Input.touchCount > 0) {


			for (int i = 0; i < Input.touchCount; i++) {	
				Touch touch = Input.GetTouch (i);
				Vector2 ray = Camera.main.ScreenToWorldPoint (touch.position);

				RaycastHit2D hit = Physics2D.Raycast (ray, Vector2.zero);

				if (hit.collider != null && hit.collider.gameObject.tag == "Button") {
					string btn = "";
					if (touch.phase == TouchPhase.Began) {
						if (hit.collider.name == "LeftBtn") {
							btn = "LeftBtn";
							Left ();
							Debug.Log ("Hello Btn");
						}else if (hit.collider.name == "JumpBtn") {
							btn = "JumpBtn";
							jump ();
							Debug.Log ("Hello Btn");
						}else if (hit.collider.name == "RightBtn") {
							btn = "RightBtn";
							Right ();
							Debug.Log ("Hello Btn");
						}


					} else if (touch.phase == TouchPhase.Stationary) {

						if (hit.collider.name == "LeftBtn") {
							btn = "LeftBtn";
							Left ();
						}else if (hit.collider.name == "Jump") {
							btn = "JumpBtn";
						}else if (hit.collider.name == "Right") {
							btn = "RightBtn";
							Right ();
						}

					} else if (touch.phase == TouchPhase.Ended) {

						btn = "";

					}
				} else {
					break;
				}

			}


		}*/

    }

	public void pointerUpL(){
		leftPressed = false;
	}
	public void pointerDownL(){
		leftPressed = true;
	}
	public void pointerUpR(){
		rightPressed = false;
	}
	public void pointerDownR(){
		rightPressed = true;
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		if (!TakingDamage && !IsDead)
		{
			float horizontal = Input.GetAxis("Horizontal");

			OnGround = IsGrounded();
			//The only change made in this script
			//HandleMovement(horizontal);

			Flip(horizontal);

			HandleLayers();
		}

	}

	public void OnDead()
	{
		if (Dead != null)
		{
			Dead();
		}
	}

	private void HandleMovement(float horizontal)
	{
		Debug.Log ("horizontal "+horizontal);
		if (MyRigidbody.velocity.y < 0)
		{
			MyAnimator.SetBool("land", true);
		}
		if (!Attack && !Slide && (OnGround || airControl))
		{
			MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
		}
		if (Jump && MyRigidbody.velocity.y == 0)
		{
			MyRigidbody.AddForce(new Vector2(0, jumpForce));
		}

		MyAnimator.SetFloat("speed",Mathf.Abs(horizontal));
	}
	//Function for jump Button
	public void jump(){
		MyAnimator.SetTrigger("jump");
		Jump = true;
		if (facingRight) {
			HandleMovement (.5f);
		} else {
			HandleMovement (-.5f);
		}
		StopCoroutine ("setZeroSpeed");
		StartCoroutine ("setZeroSpeed");
	}

	//function for going left
	public void Left(){
		HandleMovement (-.5f);
		Flip(-.5f);

		HandleLayers();
		StopCoroutine ("setZeroSpeed");
		StartCoroutine ("setZeroSpeed");
	}
	//function for going right
	public void Right(){
		HandleMovement (+.5f);
		Flip(.5f);

		HandleLayers();
		StopCoroutine ("setZeroSpeed");
		StartCoroutine ("setZeroSpeed");
	}
	IEnumerator setZeroSpeed(){
		yield return new WaitForSeconds (1f);
		HandleMovement (0f);
	}
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump");
            Jump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            MyAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MyAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            MyAnimator.SetTrigger("throw");
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }

    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        if (tmpJump)
                        {
                            tmpJump = false;
                            Jump = false;
                        }
                     
                        return true;
                    }
                }

            }
        }
        tmpJump = true;
        return false;
    }



    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    public override void ThrowKnife(int value)
    {
        if (!OnGround && value == 1 || OnGround && value == 0)
        {
            base.ThrowKnife(value);
        }
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;

            yield return new WaitForSeconds(.1f);

            spriteRenderer.enabled = true;

            yield return new WaitForSeconds(.1f);
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            health -= 10;

            if (!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                immortal = true;

                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
            }

        }
    }

    public override void Death()
    {
        MyRigidbody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        health = 50;
        transform.position = startPos;
    }


	//Sets parent of the Player to MovingPlatform so when the player is on the MovingPlayform the Player
	//will stay on the playform correctly.
	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.tag == "Moving Platform") {
			transform.parent = other.transform;
		}
	}

	//Sets parent to null so the player does not follow movment of moving platform
	//once the player is not on the moving platform.
	void OnCollisionExit2D(Collision2D other) {
		if (other.transform.tag == "Moving Platform") {
			transform.parent = null;
		}
	}


}
