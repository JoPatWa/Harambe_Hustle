using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the platform collision
/// </summary>
public class CollisionTrigger : MonoBehaviour {

    /// <summary>
    /// The player's collider
    /// </summary>
    private BoxCollider2D playerCollider;

    /// <summary>
    /// The platform collider
    /// </summary>
    [SerializeField]
    private BoxCollider2D platformCollider;

    /// <summary>
    /// The platform's trigger
    /// </summary>
    [SerializeField]
    private BoxCollider2D platformTrigger;

	// Use this for initialization
	void Start () {

        //Ignores collision with the player
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
	
	}

    /// <summary>
    /// If it collides with something
    /// </summary>
    /// <param name="other">The colliding object</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //If the player collides with the platform
        if (other.gameObject.name == "Player")
        {
            //Then ignore collision
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
        }
    }

    /// <summary>
    /// When a trigger collision stops
    /// </summary>
    /// <param name="other">The colliding object</param>
    void OnTriggerExit2D(Collider2D other)
    {
        //If the player stop colliding
        if (other.gameObject.name == "Player")
        {
            //Stop the collision from ignoring the player
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
        }
    }
	
}
