using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config params
    [Header("Player")]
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header("Projectiles")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float fireSpeed = 10f;
    [SerializeField] float firingInterval = 0.1f;

    [Header("SFX")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip explodingSFX;
    [SerializeField] AudioClip playerProjectileSFX;
    [SerializeField] float vfxDuration = 1f;
    [SerializeField] [Range(0,1)] float playerProjectileSfxVolume = 0.75f;

    //variables
    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;
    //private float movementSpeed = 10f;

    Coroutine firingCoroutine;

    float xMin, yMin;
    float xMax, yMax;

    //SceneLoader Hook
    LevelController sceneLoader;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        SetUpMoveBounderies();
        sceneLoader = FindObjectOfType<LevelController>();
        
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
            //AudioSource.PlayClipAtPoint(playerProjectileSFX, Camera.main.transform.position, playerProjectileSfxVolume);
            Destroy(laser, 2);
            yield return new WaitForSeconds(firingInterval);

        }
    }

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

    /// <summary>
    /// Process Damage when Enemy Projectile Hits the Player GameObject
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = 
            other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    /// <summary>
    /// Process Damage by reducing Health with Damage preference
    /// </summary>
    /// <param name="damageDealer"></param>
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            PlayerDie();
            Destroy(gameObject);
            sceneLoader.LoadGameOver();
        }
    }

    private void PlayerDie()
    {
        GameObject ShowExplosionVFX = 
            Instantiate(explosionVFX, 
            transform.position, 
            Quaternion.identity);

        AudioSource.
            PlayClipAtPoint(explodingSFX, 
            Camera.main.transform.position);

        Destroy(ShowExplosionVFX, vfxDuration);
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
