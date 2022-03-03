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
    [SerializeField] private float yDiff;
    [SerializeField] private float randomSpeedMin;
    [SerializeField] private float randomSpeedMax;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    public void SwitchDir()
    {
        yDiff *= -1;
        randomSpawnMin /= 4f;
        randomSpawnMax /= 4f;
        randomSpeedMin *= 4f;
        randomSpeedMax *= 4f;
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
            GameObject newAsteroid = Instantiate(asteroid, new Vector2(x, rocket.transform.position.y + yDiff), Quaternion.identity, transform);
            newAsteroid.GetComponent<Rigidbody>().velocity = new Vector2(xDir, Random.Range(-1f, 1f)) * Random.Range(randomSpeedMin, randomSpeedMax);
        }
    }
}
