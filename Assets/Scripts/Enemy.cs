using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 3f;
    [SerializeField]
    private float speed = 3f;
    private const float BoundY = 7f;
    private const float BoundX = 9.6f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, BoundY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.transform.GetComponent<Player>();
            if (!player.IsUnityNull())
            {
                player.Damage();
            }
            Destroy(gameObject);
        }

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.right * (amplitude * Time.deltaTime * Mathf.Cos(Time.time)));
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
        if (transform.position.y <= -BoundY)
        {
            var randomX = Random.Range(-BoundX + 2 * amplitude, BoundX - 2 * amplitude);
            transform.position = new Vector3(randomX, BoundY, 0);
        }
    }
}
