using UnityEngine;

public class PackageChute : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Package"))
        {
            HeldPackage packageComponent = collision.gameObject.GetComponent<HeldPackage>();

            if (packageComponent != null)
            {
                ScoreManager.Instance.AddPoints(packageComponent.pointValue);
                Debug.Log($"Package was collected! Player earned {packageComponent.pointValue} points.");
            }

            Destroy(collision.gameObject);
        }
    }
}
