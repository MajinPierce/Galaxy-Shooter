using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float fireRate = 0.25f;
    [SerializeField]
    private GameObject laserPrefab;
    private SpawnManager _spawnManager;
    private const float BoundX = 9.65f;
    private const float BoundY = 4.6f;
    private const float PlayerModelOffset = 1f;
    private float _canFire = -1f;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.FindGameObjectWithTag("Spawn Manager").transform.GetComponent<SpawnManager>();
        if (_spawnManager.IsUnityNull())
        {
            Debug.LogError("Spawn Manager is null");
        }
        transform.position = new Vector3(0, -4, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        CalculatePlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && (Time.time > _canFire))
        {
            ShootLaser();
        }
    }

    private void CalculatePlayerMovement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        
        // transform.Translate(Vector3.right * (_amplitude * Time.deltaTime * Mathf.Cos(Time.time)));
        var direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * (speed * Time.deltaTime));
        
        var position = transform.position;
        position = new Vector3(position.x, Mathf.Clamp(position.y, -BoundY, BoundY), 0);
        position = new Vector3(Mathf.Clamp(position.x, -BoundX, BoundX), position.y, 0);
        transform.position = position;
    }

    private void ShootLaser()
    {
        _canFire = Time.time + fireRate;
        Instantiate(laserPrefab, transform.position + new Vector3(0, PlayerModelOffset, 0), Quaternion.identity);
    }

    public void Damage()
    {
        lives--;
        if (lives > 0) return;
        _spawnManager.OnPlayerDeath();
        Destroy(gameObject);
    }
}
