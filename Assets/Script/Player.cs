using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config params
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float fireSpeed = 10f;
    [SerializeField] float firingInterval = 0.1f;

    //variables
    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;
    //private float movementSpeed = 10f;

    Coroutine firingCoroutine;

    float xMin, yMin;
    float xMax, yMax;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        SetUpMoveBounderies();
        
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        Fire();
        
    }


    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           firingCoroutine = StartCoroutine(ContinuousFire());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    //corouting code
    IEnumerator ContinuousFire()
    {

        while (true)
        {

            GameObject laser = Instantiate(laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, fireSpeed);
            Destroy(laser, 2);
            yield return new WaitForSeconds(firingInterval);

        }
        


    }

    /*    private void Fire()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject laser = Instantiate(laserPrefab, 
                    transform.position, 
                    Quaternion.identity) as GameObject;

                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, fireSpeed);

                Destroy(laser, 2);
            }
        }*/

    /// <summary>
    /// Move Player
    /// </summary>
    private void Move()
    {
        //keyboard controls
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);



        //movement for Mobile devices
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * movementSpeed;
            //InvokeRepeating("Fire", 0.1f, 0.3f);


            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
                StopCoroutine(firingCoroutine);
            }
        }

    }



    private void SetUpMoveBounderies()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
