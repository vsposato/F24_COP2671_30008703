using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private static readonly int DeathB = Animator.StringToHash("Death_b");
    private static readonly int JumpTrig = Animator.StringToHash("Jump_trig");
    private static readonly int DeathTypeINT = Animator.StringToHash("DeathType_int");
    private Rigidbody _playerRb;
    private Animator _playerAnim;
    private AudioSource _playerAudio;
    private GameManager _gameManagerScript;

    [Header("Movement Settings")]
    [SerializeField]
    [Range(1, 10)]
    [Tooltip("The velocity added to the player when jumping.")]
    private float jumpVelocity = 2.0f;

    [Tooltip("Fall Multiplier")] [SerializeField]
    private float fallMultiplier = 2.5f;

    [Tooltip("Low Jump Multiplier")] [SerializeField]
    private float lowJumpMultiplier = 2f;

    [Header("Game Tracker")]
    [SerializeField]
    [Tooltip("Whether the player is currently on the ground.")]
    private bool isOnGround = true;

    [Header("Sound Effects")] [SerializeField] [Tooltip("Sound to for jump start")]
    private AudioClip jumpStartSound;

    [Tooltip("Sound to play for landing jump")] [SerializeField]
    private AudioClip jumpEndSound;

    [Tooltip("Sound to play for crashing")] [SerializeField]
    private AudioClip crashSound;

    [Tooltip("Sound to play for coin pickup")] [SerializeField]
    private AudioClip pickupSound;

    [Tooltip("Sound to for running")] [SerializeField]
    private AudioClip runSound;


    /// <summary>
    /// Initializes the necessary components and game objects for the player's functionality.
    /// </summary>
    private void Start()
    {
        // Get the Rigidbody component attached to the player game object
        _playerRb = GetComponent<Rigidbody>();

        // Get the Animator component attached to the player game object
        _playerAnim = GetComponent<Animator>();

        // Get the AudioSource component attached to the player game object
        _playerAudio = GetComponent<AudioSource>();

        // Find the "GameManager" game object and get its GameManager component
        _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        HandleJump();
    }

    /// <summary>
    /// Handles the player's jump functionality.
    /// </summary>
    private void HandleJump()
    {
        // Check if the player presses the space key, is on the ground, and the game is active
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && _gameManagerScript.IsGameActive())
        {
            // Set the player to not be on the ground
            isOnGround = false;

            // Apply upward velocity to the player's rigidbody
            _playerRb.velocity = Vector3.up * jumpVelocity;

            // Trigger the jump animation
            _playerAnim.SetTrigger(JumpTrig);

            // Play the jump start sound
            _playerAudio.PlayOneShot(jumpStartSound, 2.0f);
        }

        // Check the player's vertical velocity
        switch (_playerRb.velocity.y)
        {
            // If the player is falling
            case < 0:
                // Apply a stronger gravity force to the player's rigidbody
                _playerRb.velocity +=
                    Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
                break;

            // If the player is rising and not holding the space key
            case > 0 when !Input.GetKey(KeyCode.Space):
                // Apply a reduced gravity force to the player's rigidbody
                _playerRb.velocity +=
                    Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
                break;
        }
    }

    /// <summary>
    /// This function is called when the player game object collides with another object.
    /// </summary>
    /// <param name="collision">The collision data containing information about the collision.</param>
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object has the "Ground" tag
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Set the player to be on the ground
            isOnGround = true;

            // Play the jump end sound
            _playerAudio.PlayOneShot(jumpEndSound, 2.0f);
        }
        // Check if the collision object has the "Obstacle" tag and the game is active
        else if (collision.gameObject.CompareTag("Obstacle") && _gameManagerScript.IsGameActive())
        {
            // Set the player's death animation parameters
            _playerAnim.SetBool(DeathB, true);
            _playerAnim.SetInteger(DeathTypeINT, 1);

            // Play the crash sound
            _playerAudio.PlayOneShot(crashSound, 1.0f);

            // End the game
            _gameManagerScript.GameOver();
        }
    }

    /// <summary>
    /// This function is called when the player game object enters a trigger collider.
    /// </summary>
    /// <param name="collision">The collision data containing information about the collision.</param>
    private void OnTriggerEnter(Collider collision)
    {
        // Check if the collision object has the "Coin" tag
        if (collision.gameObject.CompareTag("Coin"))
        {
            // Update the game score by 1
            _gameManagerScript.UpdateScore(1);

            // Destroy the coin game object
            Destroy(collision.gameObject);

            // Play the coin pickup sound
            _playerAudio.PlayOneShot(pickupSound, 1.0f);
        }
    }
}