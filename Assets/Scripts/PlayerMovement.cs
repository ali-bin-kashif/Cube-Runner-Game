using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public class PlayerMovement : MonoBehaviour
{
    [Range(0f, 100f)]
    public float MovingSensitivity, ForwardSpeed; // left,right and forward speed

    public float InAirSpeedDecrease = 5f;

    [Range(0f,100f)]
    public float JumpForce; 

    public float FallMultiplier; // Gravitational pull while falling 

    float movementInput;
    bool isJumping;

    Rigidbody playerBody;

    Animator cubeAnimation;
    public Animator CameraMovements;

    public int Coins;
    public int Fuel;

    int jumpsCount;

    public bool isAlive , isDead;


    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        cubeAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Fuel);
        movementInput = Input.GetAxis("Horizontal");

        //Use Mobile Accelerometer for right and left movement i.e tilt

        //Debug.Log(Input.acceleration.x);
        //playerBody.velocity = new Vector3(Input.acceleration.x * MovingSensitivity, playerBody.velocity.y, ForwardSpeed);
        if(isAlive)
        {
            playerBody.velocity = new Vector3(movementInput * MovingSensitivity, playerBody.velocity.y, ForwardSpeed);

            //Jump and ground check
            if (Input.GetMouseButtonDown(0) && (!isJumping || Fuel > 0) && !EventSystem.current.IsPointerOverGameObject())
            {
          
                playerBody.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
                cubeAnimation.SetTrigger("Jump");

                isJumping = true;
                jumpsCount++;

                if (jumpsCount == 1)
                {
                    ForwardSpeed -= InAirSpeedDecrease;
                }
                
                if(Fuel > 0 && isJumping && jumpsCount > 1)
                {
                    Fuel--;
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (playerBody.velocity.y < 0)
        {
            playerBody.velocity += Vector3.up * Physics.gravity.y * FallMultiplier * Time.fixedDeltaTime;
        }  

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
            jumpsCount = 0;
            ForwardSpeed += InAirSpeedDecrease;
            cubeAnimation.SetTrigger("Land");
            CameraMovements.SetTrigger("CameraBobY");
        }

        if(collision.gameObject.CompareTag("Obstacle"))
        {
            CameraMovements.SetTrigger("Death");
            StartCoroutine(Explode()); 
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            Collectibles collectible = other.GetComponent<Collectibles>();
            StartCoroutine(collectible.CollectibleParticles());
            Destroy(other.gameObject);
            Coins++;
        }

        if (other.gameObject.CompareTag("Fuel"))
        {
            Collectibles collectible = other.GetComponent<Collectibles>();
            StartCoroutine(collectible.CollectibleParticles());
            Destroy(other.gameObject);
            Fuel++;
        }

        if(other.gameObject.CompareTag("Void"))
        {
            CameraMovements.SetTrigger("Death");
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        isAlive = false;
        isDead = true;
        yield return new WaitForSeconds(0.07f);
        int cubePerAxis = 4;
        for (int x = 0; x < cubePerAxis; x++)
        {
            for (int y = 0; y < cubePerAxis; y++)
            {
                for (int z = 0; z < cubePerAxis; z++)
                {
                    CreateCube(new Vector3(x, y, z));
                }
            }
        }

        Destroy(gameObject);
    }

    void CreateCube(Vector3 _cordinates)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //cube.transform.SetParent(transform);

        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material = transform.GetChild(0).GetComponent<Renderer>().material;

        cube.transform.localScale = transform.localScale / 4;

        Vector3 firstCube = transform.position - transform.localScale / 2 + cube.transform.localScale/2;

        cube.transform.position = firstCube + Vector3.Scale(_cordinates, cube.transform.localScale);

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.AddExplosionForce(300f, transform.position, 5f);   
    }

}
