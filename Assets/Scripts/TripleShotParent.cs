using UnityEngine;

public class TripleShotParent : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
