using UnityEngine;

/// <summary>
/// This class handles repeating the background for perpetual horizontal running.
/// </summary>
public class RepeatingBackground : MonoBehaviour
{
    //A float to store the x-axis length of the collider2D attached to the Ground GameObject.
    private float _groundHorizontalLength;


    /// <summary>
    /// This function is responsible for initializing the ground horizontal length and storing a reference to the attached BoxCollider2D.
    /// </summary>
    private void Awake()
    {
        // Get and store a reference to the collider2D attached to Ground.
        var groundCollider = GetComponent<BoxCollider2D>();

        // Store the size of the collider along the x-axis (its length in units).
        _groundHorizontalLength = groundCollider.size.x;
    }

    /// <summary>
    /// This function is responsible for checking if the background object is no longer visible and repositioning it to create an infinite scrolling effect.
    /// </summary>
    private void Update()
    {
        // Check if the difference along the x-axis between the main Camera and the position of the object this is attached to is greater than groundHorizontalLength.
        if (transform.position.x < -_groundHorizontalLength)
        {
            // If true, this means this object is no longer visible, and we can safely move it forward to be re-used.
            RepositionBackground();
        }
    }

    /// <summary>
    /// This function is responsible for repositioning the background object to create an infinite scrolling effect.
    /// </summary>
    private void RepositionBackground()
    {
        // This is how far to the right we will move our background object, in this case, twice its length.
        // This will position it directly to the right of the currently visible background object.
        var groundOffSet = new Vector2(_groundHorizontalLength * 2f, 0);

        // Move this object from its position offscreen, behind the player, to the new position off-camera in front of the player.
        // This effectively creates an infinite scrolling background by reusing the same background object.
        transform.position = (Vector2)transform.position + groundOffSet;
    }
}