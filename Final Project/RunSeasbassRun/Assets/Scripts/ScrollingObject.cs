using System;
using UnityEngine;
using System.Collections;

public class ScrollingObject : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private float scrollSpeed = -5.0f;
    private PlayerController _playerControllerScript;
    private float _leftBound = -12.5f;

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
        if (transform.position.x < _leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}