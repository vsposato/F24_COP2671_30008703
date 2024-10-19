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


    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void Update()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && _gameManagerScript.IsGameActive())
        {
            isOnGround = false;
            _playerRb.velocity = Vector3.up * jumpVelocity;
            _playerAnim.SetTrigger(JumpTrig);
            _playerAudio.PlayOneShot(jumpStartSound, 2.0f);
        }

        switch (_playerRb.velocity.y)
        {
            case < 0:
                _playerRb.velocity +=
                    Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
                break;
            case > 0 when !Input.GetKey(KeyCode.Space):
                _playerRb.velocity +=
                    Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            _playerAudio.PlayOneShot(jumpEndSound, 2.0f);
        }
        else if (collision.gameObject.CompareTag("Obstacle") && _gameManagerScript.IsGameActive())
        {
            _playerAnim.SetBool(DeathB, true);
            _playerAnim.SetInteger(DeathTypeINT, 1);
            _playerAudio.PlayOneShot(crashSound, 1.0f);
            _gameManagerScript.GameOver();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            _gameManagerScript.UpdateScore(1);
            Destroy(collision.gameObject);
            _playerAudio.PlayOneShot(pickupSound, 1.0f);
        }
    }
}