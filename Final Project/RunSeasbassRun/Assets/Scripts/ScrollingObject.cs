using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private float scrollSpeed = -5.0f;
    private PlayerController _playerControllerScript;
    private const float LeftBound = -12.5f;

    void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_playerControllerScript.GameActive())
        {
            transform.position += new Vector3(scrollSpeed, 0, 0) * Time.deltaTime;
        }
        if (transform.position.x < LeftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    public void SetScrollSpeed(float newScrollSpeed)
    {
        this.scrollSpeed = newScrollSpeed;
    }
}