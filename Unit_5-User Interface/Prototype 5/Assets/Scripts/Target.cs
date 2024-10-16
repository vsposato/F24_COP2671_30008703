using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    public int pointValue;
    public ParticleSystem explosionParticle;
    private GameManager gameManager;

    private readonly float maxSpeed = 16.0f;

    private readonly float maxTorque = 10.0f;

    private readonly float minSpeed = 12.0f;
    private Rigidbody targetRb;

    private readonly float xRange = 4.0f;

    private readonly float ySpawnPos = -2.0f;

    // Start is called before the first frame update
    private void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position,
                explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad")) gameManager.GameOver();
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}