using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public PlayerShip instance{ get; private set; }
    private Vector2 inputVector;
    private Vector3 inputMousedirection;
    private Rigidbody2D rb2D;
    public GameObject bullet;
    public Transform bulletSpawn;
    public AllShip playerShip = new AllShip(1,10,10);
    private Vector3 direction;
    private GameManager gameManager;

    void Start()
    {
        instance = this;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputVector = GameInput.instance.GetMovementVector(); 
        inputMousedirection = GameInput.instance.GetMousePosition();
        Shot();
        Direction();
    }

    private void FixedUpdate()
    {
        inputVector = inputVector.normalized;
        rb2D.MovePosition(rb2D.position + inputVector * (playerShip.speed * Time.fixedDeltaTime));
    }


    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 vectorPlayerPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        return vectorPlayerPosition;
    }

    private void Direction()
    {
       Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       mousePosition.z = 0;
       direction = (mousePosition - bulletSpawn.transform.position).normalized;
       transform.right = direction; 
       transform.up = direction;
    }
    private void Shot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        GameObject bull = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        bull.GetComponent<MoveSpark>().DirectionBull(direction);
        bull.GetComponent<MoveSpark>().DirectionBull(mousePosition - bulletSpawn.transform.position);
        bull.GetComponent<Rigidbody2D>().AddForce(direction * playerShip.speed * 3 ,ForceMode2D.Impulse );
    }
    public void OnTriggerEnter2D(BoxCollider2D other)
    {
        if (other.gameObject.CompareTag("EnemySpark") )
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameManager.AudioSoursBoom();
            gameManager.GameOver();
        }
    }
    
}
