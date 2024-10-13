using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private readonly Vector3 _spawnPos = new Vector3(25, 0, -4);
    private readonly float _startDelay = 2.0f;
    private readonly float _repeatRate = 2.0f;
    private PlayerController _playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", _startDelay, _repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnObstacle()
    {
        if (!_playerControllerScript.GameActive())
        {
            return;
        }

        var obstacleScrollSpeed = Random.Range(-7.0f, -8.0f);
        var obstacleNumber = Random.Range(0, obstaclePrefab.Length);
        var obstacle = obstaclePrefab[obstacleNumber];
        obstacle.GetComponent<ScrollingObject>().SetScrollSpeed(obstacleScrollSpeed);
        Instantiate(obstacle, _spawnPos, obstacle.transform.rotation);
    }
}