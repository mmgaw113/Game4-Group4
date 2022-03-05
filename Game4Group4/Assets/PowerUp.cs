using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float reFuel;
    public Flight flight;
    private void Awake()
    {
        flight = GameObject.Find("Rocket").GetComponent<Flight>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (flight.fuel > 0)
        {
            if (other.gameObject.CompareTag("Rocket"))
            {
                Destroy(gameObject);
                flight.fuel += reFuel;
            }
        }
    }
}
