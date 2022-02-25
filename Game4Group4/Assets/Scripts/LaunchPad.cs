using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public float fuel;
    public float launchSpeed;
    public bool grounded = false;
    private Rigidbody rb;
    public float fallSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && grounded)
        {
            Launch();
            grounded = false;
        }
    }
    void Launch()
    {
        rb.AddForce(transform.up * launchSpeed, ForceMode.Impulse);
    }
}
