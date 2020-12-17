using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DragFingerMovement : MonoBehaviour
{

    [SerializeField] GameObject laserPrefab;
    [SerializeField] float fireSpeed = 10f;

    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;
    private float movementSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        Fire();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * movementSpeed;
            

            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
            }
        }
        
    }


    private void Fire()
    {
            GameObject laser = Instantiate(laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, fireSpeed);
            Destroy(laser, 2);
    }
}
