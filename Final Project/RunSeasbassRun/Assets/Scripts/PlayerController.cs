using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int DeathB = Animator.StringToHash("Death_b");
    private static readonly int JumpTrig = Animator.StringToHash("Jump_trig");
    private static readonly int DeathTypeINT = Animator.StringToHash("DeathType_int");
    private Rigidbody _playerRb;
    private Animator _playerAnim;
    private AudioSource _playerAudio;

    [Header("Movement Settings")]
    [SerializeField]
    [Tooltip("The force added to the player when jumping.")]
    private float jumpForce = 700.0f;

    [Tooltip("A physics gravity modifier.")] [SerializeField]
    private float gravityModifier = 1.5f;

    [Header("Game Tracker")]
    [SerializeField]
    [Tooltip("Whether the player is currently on the ground.")]
    private bool isOnGround = true;

    [Tooltip("Game status")] [SerializeField]
    private bool gameOver = false;

    [Header("Sound Effects")] [SerializeField] [Tooltip("Sound to for jump start")]
    private AudioClip jumpStartSound;

    [Tooltip("Sound to for landing jump")] [SerializeField]
    private AudioClip jumpEndSound;

    [Tooltip("Sound to for crashing")] [SerializeField]
    private AudioClip crashSound;

    [Tooltip("Sound to for running")] [SerializeField]
    private AudioClip runSound;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();

        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && GameActive())
        {
            isOnGround = false;
            _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _playerAnim.SetTrigger(JumpTrig);
            _playerAudio.PlayOneShot(jumpStartSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground collision");
            isOnGround = true;
            _playerAudio.PlayOneShot(jumpEndSound, 1.0f);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            _playerAnim.SetBool(DeathB, true);
            _playerAnim.SetInteger(DeathTypeINT, 1);
            _playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }

    public bool GameActive()
    {
        return !gameOver;
    }
}