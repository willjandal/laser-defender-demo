using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float fxDuration = 1f;

    [Header("GameObjects")]
    [SerializeField] GameObject enemeyProjectilePrefab;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip enemyFireSFX;


    // Start is called before the first frame update
    void Start()
    {

        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        
    }

    // Update is called once per frame
    void Update()
    {

        CountDownAndShoot();
        
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyProjectile = Instantiate(enemeyProjectilePrefab, 
            transform.position, 
            Quaternion.identity) as GameObject;
        EnemyFireSFX(); //SFX for enemy projectile

        enemyProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        Destroy(enemyProjectile, 2);
    }

    /// <summary>
    /// Initiates the damage to the Gameobject on Trigger Collision
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
            PlayExplosionSFX();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject ShowExplosionVFX = 
            Instantiate(explosionVFX, 
            transform.position, 
            Quaternion.identity);
        Destroy(ShowExplosionVFX, fxDuration);
    }

    private void PlayExplosionSFX()
    {
        AudioSource.PlayClipAtPoint
            (explosionSFX, Camera.main.transform.position);
    }

    private void EnemyFireSFX()
    {
        AudioSource.PlayClipAtPoint
            (enemyFireSFX, Camera.main.transform.position);

    }

}
