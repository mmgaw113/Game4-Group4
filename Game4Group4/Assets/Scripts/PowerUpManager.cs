using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public Transform rocket;
    public float timer;
    public GameObject powerUp;
    public float powerUpSpawnMinY;
    public float powerUpSpawnMaxY;
    public float powerUpSpawnMinX;
    public float powerUpSpawnMaxX;
    private void Awake()
    {
        rocket = GameObject.Find("Rocket").GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = 15;
    }

    // Update is called once per frame
    void Update()
    {
        powerUpSpawnMinY = rocket.transform.position.y + 20;
        powerUpSpawnMaxY = rocket.transform.position.y + 50;
        float powerUpSpawnY = Random.Range(powerUpSpawnMinY, powerUpSpawnMaxY);
        float powerUpSpawnX = Random.Range(powerUpSpawnMinX, powerUpSpawnMaxX);
        Vector3 powerUpSpawn = new Vector3(powerUpSpawnX, powerUpSpawnY, 0f);
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Instantiate(powerUp, powerUpSpawn, Quaternion.identity);
            ResetTimer();
        }
    }
    void ResetTimer()
    {
        timer = rocket.transform.position.y/10;
    }
}
