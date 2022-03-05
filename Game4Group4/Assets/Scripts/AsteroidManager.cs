using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject rocket;
    [SerializeField] private float randomSpawnMin;
    [SerializeField] private float randomSpawnMax;
    private float timer;
    [SerializeField] private float xRight;
    [SerializeField] private float xLeft;
    [SerializeField] private float yDiffMin;
    [SerializeField] private float yDiffMax;
    [SerializeField] private float randomSpeedMin;
    [SerializeField] private float randomSpeedMax;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    public void SwitchDir()
    {
        yDiffMin *= -1f;
        yDiffMax *= -1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            timer = Random.Range(randomSpawnMin, randomSpawnMax);
            float xDir = -1f;
            float x = xRight;
            if(Random.value < .5f)
            {
                x = xLeft;
                xDir = 1f;
            }
            GameObject newAsteroid = Instantiate(asteroid, new Vector2(x, rocket.transform.position.y + Random.Range(yDiffMin, yDiffMax)), Quaternion.identity, transform);
            newAsteroid.GetComponent<Rigidbody>().velocity = new Vector2(xDir, Random.Range(-1f, 1f)) * Random.Range(randomSpeedMin, randomSpeedMax);
        }
    }
}
