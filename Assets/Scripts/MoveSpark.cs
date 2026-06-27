using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpark : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 targetDirection;
    private AudioSource sourceBoom;
    public AudioClip Boom;
    public AudioClip Spark;
    private EnemyAttack _enemyAttack;
    private float speed = 6f;
    private float topLimit = 7;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sourceBoom = GetComponent<AudioSource>();
        sourceBoom.PlayOneShot(Spark, 1f);
        Invoke("DestroySpark",3);
        
    }

    private void DestroySpark()
    {
        Destroy(this.gameObject);
    }
    public void DirectionBull(Vector3 mousePositionn)
    {
        targetDirection = (mousePositionn - transform.position).normalized;
        
        // Для 2D объектов вычисляем угол поворота
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward).normalized;
       // rb2d.AddForce(targetDirection * 20 * Time.deltaTime);
    }
}
