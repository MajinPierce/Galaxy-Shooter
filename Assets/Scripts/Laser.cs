using UnityEngine;
using UnityEngine.Serialization;

public class Laser : MonoBehaviour
{

    [FormerlySerializedAs("_speed")] [SerializeField]
    private float speed = 8f;
    private const float BoundY = 6.5f;
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    private void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));
        if (transform.position.y > BoundY)
        {
            Destroy(gameObject);
        }
    }
}
