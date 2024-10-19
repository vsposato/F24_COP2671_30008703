using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    //A float to store the x-axis length of the collider2D attached to the Ground GameObject.
    private float _groundHorizontalLength;


    //Awake is called before Start.
    private void Awake()
    {
        //Get and store a reference to the collider2D attached to Ground.
        var groundCollider = GetComponent<BoxCollider2D>();

        //Store the size of the collider along the x-axis (its length in units).
        _groundHorizontalLength = groundCollider.size.x;
    }

//Update runs once per frame
    private void Update()
    {
        //Check if the difference along the x-axis between the main Camera and the position of the object this is attached to is greater than groundHorizontalLength.
        if (transform.position.x < -_groundHorizontalLength)
        {
            //If true, this means this object is no longer visible, and we can safely move it forward to be re-used.
            RepositionBackground();
        }
    }

//Moves the object this script is attached to right in order to create our looping background effect.
    private void RepositionBackground()
    {
        //This is how far to the right we will move our background object, in this case, twice its length. This will position it directly to the right of the currently visible background object.
        var groundOffSet = new Vector2(_groundHorizontalLength * 2f, 0);

        //Move this object from its position offscreen, behind the player, to the new position off-camera in front of the player.
        transform.position = (Vector2)transform.position + groundOffSet;
    }
}