using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    /// <summary>
    /// The maximum x position of the camera
    /// </summary>
    [SerializeField]
    private float xMax;

    /// <summary>
    /// The maximum y position of the camera
    /// </summary>
    [SerializeField]
    private float yMax;

    /// <summary>
    /// The minimum x position of the camera
    /// </summary>
    [SerializeField]
    private float xMin;

    /// <summary>
    /// The minimum y positoin of the amera
    /// </summary>
    [SerializeField]
    private float yMin;

    /// <summary>
    /// The target that the camera will follow
    /// </summary>
    private Transform target;

	// Use this for initialization
	void Start ()
    {
        //Sets the cameras target as the player
        target = GameObject.Find("Player").transform;
	}

    // Update is called once per frame
    void LateUpdate()
    {
        //Moves the camera to the target's position, while calamping it inside the level
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
    }
}
