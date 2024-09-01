using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float timer = 0f;
    private float coolDownTimer = 0.75f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= coolDownTimer)
        {
            // On spacebar press, send dog
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                timer = 0f;
            }
        
        }
    }
}
