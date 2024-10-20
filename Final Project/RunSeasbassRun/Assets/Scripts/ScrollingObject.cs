using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    // Use this for initialization
    [SerializeField]
    private float scrollSpeed = -5.0f;

    private GameManager _gameManager;
    private const float LeftBound = -12.5f;

    /// <summary>
    /// Initializes the ScrollingObject by finding the GameManager and setting the initial scroll speed.
    /// </summary>
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Updates the position of the ScrollingObject based on the game's state and scroll speed.
    /// Destroys the object if it reaches the left boundary and has the "Obstacle" tag.
    /// </summary>
    private void Update()
    {
        if (_gameManager.IsGameActive())
        {
            transform.position += new Vector3(scrollSpeed, 0, 0) * Time.deltaTime;
        }

        if (transform.position.x < LeftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            _gameManager.UpdateScore(1);
        }
    }

    /// <summary>
    /// Sets the new scroll speed for the ScrollingObject.
    /// </summary>
    /// <param name="newScrollSpeed">The new scroll speed to be applied.</param>
    public void SetScrollSpeed(float newScrollSpeed)
    {
        this.scrollSpeed = newScrollSpeed;
    }
}