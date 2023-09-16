using System;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    private const float BoundY = 7.5f;
    [SerializeField]
    private int powerupId;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
        if (transform.position.y < -BoundY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        var player = other.transform.GetComponent<Player>();
        if (!player.IsUnityNull())
        {
            player.ActivatePowerup(powerupId);
        }
        Destroy(gameObject);
    }
}
