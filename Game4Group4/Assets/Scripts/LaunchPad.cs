using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public float fuel;
    public float launchSpeed;
    public bool goingUp = false;
    private Rigidbody rb;
    public float fallSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goingUp)
        {
            fuel -= 10 * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            rb.AddForce(transform.up * launchSpeed, ForceMode.Impulse);
            goingUp = true;
        }
        else if(fuel <= 0)
        {
            goingUp = false;
        }
    }
}
