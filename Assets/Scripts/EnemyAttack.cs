using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Vector3 targetDirection;
    public GameObject player;   
    public GameObject effectBoom;
    public GameManager gameManager;
    private AudioSource souoom;
    public AudioClip soundAttack;
    private float speed = 4;
    private float destroyBoom = -6f;
    private int score = 2;
    void Start()
    {
        gameManager = GameManager.FindAnyObjectByType<GameManager>();   
        souoom = GetComponent<AudioSource>();
        souoom.PlayOneShot(soundAttack, 0.03f);
        Invoke("DestroyBolt", 3);
    }

    void Update()
    {
        MoveBolt();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Spark"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Vector2 positionEffect = new Vector2(transform.position.x, transform.position.y);
            Instantiate(effectBoom, positionEffect, effectBoom.transform.rotation);
            gameManager.AudioSoursBoom();
            if (other.gameObject.CompareTag("Player")) 
                gameManager.GameOver();
            if (other.gameObject.CompareTag("Spark"))
                gameManager.UpdateScore(score);
        }
    }

    public void DestroyBolt()
    {
        Destroy(gameObject);
    }
    private void MoveBolt()
    {
        Vector3 playerPos = Camera.main.ScreenToWorldPoint(player.transform.position);
        playerPos.z = 0;
        Vector3 direction = (playerPos - transform.position).normalized;
        transform.right = direction; 
    }
    public void DirectionBull(Vector3 mousePositionn)
    {
        targetDirection = (player.transform.position - transform.position).normalized;
        
        // Для 2D объектов вычисляем угол поворота
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward).normalized;
        // rb2d.AddForce(targetDirection * 20 * Time.deltaTime);
    }

}
