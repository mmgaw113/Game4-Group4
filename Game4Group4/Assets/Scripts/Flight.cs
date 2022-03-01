using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    public float fuel;
    public float launchSpeed;
    public bool grounded = false;
    private Rigidbody rb;
    public float fallSpeed;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grounded = true;
        fuel *= Manager.stats[UpgradeType.Fuel].Val;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && grounded)
        {
            Launch();
            grounded = false;
        }
        if (!grounded)
        {
            if (fuel > 0f)
            {
                fuel -= Time.deltaTime;
                rb.velocity = transform.up * Manager.stats[UpgradeType.Speed].Val;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.forward, Time.deltaTime * Manager.stats[UpgradeType.Handling].Val * rotationSpeed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.forward, -Time.deltaTime * Manager.stats[UpgradeType.Handling].Val * rotationSpeed);
            }
            Transform camera = transform.GetChild(0);
            camera.rotation = Quaternion.identity;
            camera.position = new Vector3(0f, transform.position.y + 4f, transform.position.z - 8f);
        }
    }
    void Launch()
    {
        rb.AddForce(transform.up * launchSpeed * Manager.stats[UpgradeType.LaunchSpeed].Val, ForceMode.Impulse);
    }
}
