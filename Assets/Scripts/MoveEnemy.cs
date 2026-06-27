using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject PrefabAttack;
    private GameManager gameManager;
    public GameObject particle;
    private float botLimit = -6f;
    private float intervalSpawnPrefab;
    private float delayPrefab;
    public int score;
    public int glasses;

    void Start()
    {
        score = 0;
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        intervalSpawnPrefab = Random.Range(2f, 4f);
        delayPrefab = Random.Range(2f, 3f);
        InvokeRepeating("AttackEnemy", intervalSpawnPrefab, delayPrefab);
        
    }

   
   
    void Update()
    {
        MoveEnemytrue();   
    }

    private void AttackEnemy()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Instantiate(PrefabAttack, position, PrefabAttack.transform.rotation);
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
        }
    }

    IEnumerator TimeDestroParticle()
    {
        yield return new WaitForSeconds(1);
        
        Destroy(particle);
    }


    private void MoveEnemytrue()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y < botLimit)
        {
            Destroy(gameObject);
        }
    }
}
