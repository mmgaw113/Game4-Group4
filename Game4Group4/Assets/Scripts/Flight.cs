using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flight : MonoBehaviour
{
    public float fuel;
    public float launchSpeed;
    public bool grounded = false;
    private Rigidbody rb;
    public float fallSpeed;
    public float rotationSpeed;
    private Transform mainCamera;
    public float speed;
    public float xMax;
    public float xMin;
    public float boostFuel;
    public float boostSpeed;
    private float lives;
    private bool dead;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grounded = true;
        fuel *= Manager.stats[UpgradeType.Fuel].Val;
        boostFuel *= Manager.stats[UpgradeType.BoostFuel].Val;
        mainCamera = transform.GetChild(0);
        lives = Manager.stats[UpgradeType.Lives].Val;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name[0] == 'A')
        {
            lives--;
            if(lives == 0)
            {
                dead = true;
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                Invoke("LoadUpgradeScene", 2f);
            }
        }
    }

    private void LoadUpgradeScene()
    {
        SceneManager.LoadScene("UpgradeScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
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
                    rb.velocity = transform.up * Manager.stats[UpgradeType.Speed].Val * speed;
                    if (Input.GetKey(KeyCode.S))
                    {
                        rb.velocity = rb.velocity * (1f - (Manager.stats[UpgradeType.ReverseSpeed].Val / Manager.stats[UpgradeType.ReverseSpeed].ValMax));
                    }
                }
                else
                {
                    rb.velocity = Vector3.zero;
                }
                if (boostFuel > 0f && Input.GetKey(KeyCode.LeftShift))
                {
                    boostFuel -= Time.deltaTime;
                    Vector3 tempVel = rb.velocity;
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (rb.velocity.y * Manager.stats[UpgradeType.BoostSpeed].Val * boostSpeed));
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(transform.forward, Time.deltaTime * Manager.stats[UpgradeType.Handling].Val * rotationSpeed);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(transform.forward, -Time.deltaTime * Manager.stats[UpgradeType.Handling].Val * rotationSpeed);
                }
            }
            if (transform.position.x > xMax)
            {
                transform.position = new Vector2(xMax, transform.position.y);
            }
            else if (transform.position.x < xMin)
            {
                transform.position = new Vector2(xMin, transform.position.y);
            }
            mainCamera.rotation = Quaternion.identity;
            mainCamera.position = new Vector3(0f, transform.position.y + 4f, transform.position.z - 8f);
        }
    }
    void Launch()
    {
        rb.AddForce(transform.up * launchSpeed * Manager.stats[UpgradeType.LaunchSpeed].Val, ForceMode.Impulse);
    }
}
