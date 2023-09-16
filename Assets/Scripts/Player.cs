using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float speedBoostFactor = 5f;
    [SerializeField]
    private float fireRate = 0.25f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    private SpawnManager _spawnManager;
    private const float BoundX = 9.65f;
    private const float BoundY = 4.6f;
    private readonly Vector3 _laserOffset = new Vector3(0, 1f, 0);
    private readonly Vector3 _tripleShotOffset = new Vector3(-0.473f, 0, 0);
    private float _canFire = -1f;
    [SerializeField]
    private bool hasTripleShot = false;
    [SerializeField]
    private float powerupActivationTime = 5f;
    
    
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
        if (hasTripleShot)
        {
            Instantiate(tripleShotPrefab, transform.position + _tripleShotOffset, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + _laserOffset, Quaternion.identity);
        }
        
    }

    public void Damage()
    {
        lives--;
        if (lives > 0) return;
        _spawnManager.OnPlayerDeath();
        Destroy(gameObject);
    }

    public void ActivatePowerup(int powerupId)
    {
        switch (powerupId)
        {
            case 0:
                hasTripleShot = true;
                StartCoroutine(TripleShotRuntime());
                break;
            case 1:
                speed += speedBoostFactor;
                StartCoroutine(SpeedBoostRuntime());
                break;
        }
    }

    private IEnumerator TripleShotRuntime()
    {
        yield return new WaitForSeconds(powerupActivationTime);
        hasTripleShot = false;
    }
    
    private IEnumerator SpeedBoostRuntime()
    {
        yield return new WaitForSeconds(powerupActivationTime);
        speed -= speedBoostFactor;
    }
    
}
