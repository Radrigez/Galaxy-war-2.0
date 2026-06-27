using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    private GameObject Player; 
    private Vector2 movementVector;
    private Transform enemyTrans;
    private Rigidbody2D rb;
    private Vector3 movement;
    public GameObject PrefabAttack;
    public Transform bulletSpawn;
    private float intervalSpawnPrefab;
    private float delayPrefab;
    private GameManager gameManager;
    public GameObject particle;
    public int score;
    public int glasses;
    private AudioSource audioSource;
    public AudioClip clipBoomsEnemy;
    public AllShip enemyShip = new AllShip(1,3,3);
    void Start()
    {
        score = 0;
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        movementVector = transform.position;
        enemyTrans = GetComponent<Transform>();
        intervalSpawnPrefab = Random.Range(1f, 3f);
        delayPrefab = Random.Range(1f, 3f);
        InvokeRepeating("AttackEnemy", intervalSpawnPrefab, delayPrefab);
        StartCoroutine(TimeDestroParticle());
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        MoveEnemy();
    }
    IEnumerator TimeDestroParticle()
    {
        yield return new WaitForSeconds(1);
        
        Destroy(particle);
    }
    private void MoveEnemy()
    {
        movement = (Player.transform.position - transform.position).normalized;
        rb.AddForce(movement * enemyShip.speed);
        if (Player.transform.position.x > enemyTrans.position.x)
        {
            enemyTrans.localScale = new Vector3(.3f, .3f, .3f);
        }
        else
        {
            enemyTrans.localScale = new Vector3(-.3f, .3f, .3f);
        }
        
        movement.z = 0;
        Vector3 direction = (movement - transform.position).normalized;
        transform.right = direction; 
        transform.up = direction;
    }
    private void AttackEnemy()
    {
        FireProjectile();
    }
    private void FireProjectile()
    {
        Vector3 playerPos = Camera.main.ScreenToWorldPoint(Player.transform.position);
        playerPos.z = 0;
        GameObject bull = Instantiate(PrefabAttack, bulletSpawn.transform.position, Quaternion.identity);
        bull.GetComponent<EnemyAttack>().DirectionBull(playerPos.normalized);
        bull.GetComponent<Rigidbody2D>().AddForce(movement * enemyShip.speed ,ForceMode2D.Impulse );
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spark"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            gameManager.UpdateScore(glasses);
            Instantiate(particle, transform.position, particle.transform.rotation);
            StartCoroutine(TimeDestroParticle());
            gameManager.AudioSoursBoom();
        }
    }
}