using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Flight : MonoBehaviour
{
    public float fuel;
    public float launchSpeed;
    public bool grounded = false;
    private Rigidbody rb;
    public float fallSpeed;
    public float parachuteFallSpeed;
    public float rotationSpeed;
    private Transform mainCamera;
    public float speed;
    public float xMax;
    public float xMin;
    public float boostFuel;
    public float boostSpeed;
    private float lives;
    private bool dead;
    public float parachuteMoveSpeed;
    private bool lostFuel;
    private bool lerping;
    private AsteroidManager asteroidManager;
    private Text heightText;
    private Text fuelText;
    private Text boostText;
    private float height;
    private bool launched;
    private float launchTimer;
    public GameObject rocketTrail;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grounded = true;
        fuel *= Manager.stats[UpgradeType.Fuel].Val;
        boostFuel *= Manager.stats[UpgradeType.BoostFuel].Val;
        mainCamera = transform.GetChild(0);
        lives = Manager.stats[UpgradeType.Lives].Val;
        asteroidManager = GameObject.Find("AsteroidManager").GetComponent<AsteroidManager>();
        GameObject canvas = GameObject.Find("Canvas");
        heightText = canvas.transform.GetChild(0).GetComponent<Text>();
        fuelText = canvas.transform.GetChild(1).GetComponent<Text>();
        boostText = canvas.transform.GetChild(2).GetComponent<Text>();
        launchTimer = 1f;
    }
    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name[0] == 'A')
        {
            Destroy(collision.gameObject);
            lives--;
            Manager.stats[UpgradeType.Lives].Val = lives;
            if(lives == 0)
            {
                Manager.stats[UpgradeType.Lives].Val = 1;
                dead = true;
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                int earnings = (int)(height / 20f) - 10;
                if (earnings < 0) earnings = 0;
                Manager.money += earnings;
                Invoke("LoadUpgradeScene", 2f);
            }
        }
        else if(launched)
        {
            Manager.money += (int)(height / 20f);
            Invoke("LoadUpgradeScene", 2f);
        }
    }

    private void LoadUpgradeScene()
    {
        Manager.stats[UpgradeType.Parachute].Val = 0.0f;
        SceneManager.LoadScene("UpgradeScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if(fuel > 0f)
            {
                fuelText.text = "Fuel: " + ((int)fuel).ToString();
            }
            if(boostFuel > 0f)
            {
                boostText.text = "Boost: " + ((int)boostFuel).ToString();
            }
            if(transform.position.y > height)
            {
                height = transform.position.y;
                heightText.text = "Height: " + ((int)transform.position.y).ToString();
            }
            if (lerping)
            {
                mainCamera.transform.position = new Vector3(0, mainCamera.position.y - (Time.deltaTime * 10f), -8f);
                if(mainCamera.position.y < transform.position.y - 2f)
                {
                    mainCamera.position = new Vector3(0, -2f, -8f);
                    lerping = false;
                    asteroidManager.SwitchDir();
                }
            }
            if(fuel <= 0)
            {
                if (!lostFuel)
                {
                    lostFuel = true;
                    lerping = true;
                    if (Manager.stats[UpgradeType.Parachute].Val != 0f)
                    {
                        Parachute();
                    }
                    else
                    {
                        Falling();
                        rocketTrail.SetActive(false);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.F) && grounded)
            {
                Launch();
                grounded = false;
            }
            if (!grounded)
            {
                if (launchTimer < 0f)
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
                    if (boostFuel > 0f && fuel > 0f && Input.GetKey(KeyCode.LeftShift))
                    {
                        boostFuel -= Time.deltaTime;
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (rb.velocity.y * Manager.stats[UpgradeType.BoostSpeed].Val * boostSpeed));
                    }
                }
                else
                {
                    launchTimer -= Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    if (fuel > 0)
                    {
                        transform.Rotate(transform.forward, Time.deltaTime * Manager.stats[UpgradeType.Handling].Val * rotationSpeed);
                    }
                    else if(Manager.stats[UpgradeType.Parachute].Val != 0f)
                    {
                        rb.velocity = new Vector2(-1f - Manager.stats[UpgradeType.Handling].Val * parachuteMoveSpeed, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-parachuteMoveSpeed, rb.velocity.y);
                    }
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    if (fuel > 0)
                    {
                        transform.Rotate(transform.forward, -Time.deltaTime * Manager.stats[UpgradeType.Handling].Val * rotationSpeed);
                    }
                    else if (Manager.stats[UpgradeType.Parachute].Val != 0f)
                    {
                        rb.velocity = new Vector2(1f + Manager.stats[UpgradeType.Handling].Val * parachuteMoveSpeed, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(parachuteMoveSpeed, rb.velocity.y);
                    }
                }
                else if(fuel <= 0)
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
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
            if (!lerping)
            {
                if (lostFuel)
                {
                    mainCamera.position = new Vector3(0f, transform.position.y - 2f, transform.position.z - 8f);
                }
                else
                {
                    mainCamera.position = new Vector3(0f, transform.position.y + 4f, transform.position.z - 8f);
                }
            }
            if(mainCamera.position.y < 5f)
            {
                mainCamera.position = new Vector3(0f, 5f, transform.position.z - 8f);
            }
        }
        else
        {
            rocketTrail.SetActive(false);
        }
    }
    void Launch()
    {
        rb.AddForce(transform.up * launchSpeed * Manager.stats[UpgradeType.LaunchSpeed].Val, ForceMode.Impulse);
        rocketTrail.SetActive(true);
        launched = true;
    }
    void Parachute()
    {
        //rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, parachuteFallSpeed * (3f - (Manager.stats[UpgradeType.ParachuteSize].Val / Manager.stats[UpgradeType.ParachuteSize].ValMax)), 0f);
    }
    void Falling()
    {
        //rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, fallSpeed, 0f);
    }
}
