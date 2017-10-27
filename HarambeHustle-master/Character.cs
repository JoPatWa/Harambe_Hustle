using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Contains all the general functionality for all characters in the game
/// </summary>
public abstract class Character : MonoBehaviour {

    /// <summary>
    /// The position the knife will spawn on
    /// </summary>
    [SerializeField]
    protected Transform knifePos;

    /// <summary>
    /// The characters movement speed
    /// </summary>
    [SerializeField]
    protected float movementSpeed;

    /// <summary>
    /// Indicates if the character is facing right
    /// </summary>
    protected bool facingRight;

    /// <summary>
    /// The knife prefab, this is used for instantiating a knife
    /// </summary>
    [SerializeField]
    private GameObject knifePrefab;

    /// <summary>
    /// The character's health
    /// </summary>
    [SerializeField]
    protected int health;

    /// <summary>
    /// The character's sword collider
    /// </summary>
    [SerializeField]
    private EdgeCollider2D swordCollider;

    /// <summary>
    /// A list of damage sources (tags that can damage the character)
    /// </summary>
    [SerializeField]
    private List<string> damageSources;

    /// <summary>
    /// Indicates if the character is dead
    /// </summary>
    public abstract bool IsDead { get; }

    /// <summary>
    /// Indicates if the character can attack
    /// </summary>
    public bool Attack { get; set; }

    /// <summary>
    /// Indicates if the character is taking damage
    /// </summary>
    public bool TakingDamage { get; set; }

    /// <summary>
    /// A reference to the character's animator
    /// </summary>
    public Animator MyAnimator { get; private set; }

    /// <summary>
    /// Property for getting the swordCollider
    /// </summary>
    public EdgeCollider2D SwordCollider
    {
        get
        {
            return swordCollider;
        }
    }

    // Use this for initialization
    public virtual void Start ()
    {
        facingRight = true;

        MyAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    /// <summary>
    /// Makes the character take damage
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator TakeDamage();

    /// <summary>
    /// Handles the character's death
    /// </summary>
    public abstract void Death();

    /// <summary>
    /// Changes the charcters direction
    /// </summary>
    public void ChangeDirection()
    {
        //Changes the facingRight bool to its negative value
        facingRight = !facingRight;

        //Flips the character by changing the scale
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    /// <summary>
    /// Throws a knife
    /// </summary>
    /// <param name="value">0 = ground, 1 = layer</param>
    public virtual void ThrowKnife(int value)
    {
        if (facingRight) //If we are facing right then throw the knife to the right
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.right);
        }
        else //If we are facing to the lft then throw the knife to the left.
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.left);
        }
    }

    /// <summary>
    /// Thows a melee attack
    /// </summary>
    public void MeleeAttack()
    {
        SwordCollider.enabled = true;
        Vector3 tmpPos = swordCollider.transform.position;
        swordCollider.transform.position = new Vector3(swordCollider.transform.position.x + 0,01, swordCollider.transform.position.y);
        swordCollider.transform.position = tmpPos;
    }
 
    /// <summary>
    /// If the character collides with another object
    /// </summary>
    /// <param name="other">The collider of the other object</param>
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //If the object we hit is a damage source
        if (damageSources.Contains(other.tag))
        {
            //Run the take damage co routine
            StartCoroutine(TakeDamage());
        }
    }
}
