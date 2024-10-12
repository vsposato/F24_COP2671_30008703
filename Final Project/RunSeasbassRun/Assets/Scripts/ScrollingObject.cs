using System;
using UnityEngine;
using System.Collections;

public class ScrollingObject : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private float scrollSpeed = -5.0f;
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerControllerScript.GameActive())
        {
            transform.position += new Vector3(scrollSpeed, 0, 0) * Time.deltaTime;
        }
    }
}