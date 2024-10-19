using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private float scrollSpeed = -5.0f;
    private GameManager _gameManager;
    private const float LeftBound = -12.5f;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

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

    public void SetScrollSpeed(float newScrollSpeed)
    {
        this.scrollSpeed = newScrollSpeed;
    }
}