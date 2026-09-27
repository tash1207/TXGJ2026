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
        if (!collision.gameObject.CompareTag("HeldPackage")) return;
        
        Package packageComponent = collision.gameObject.GetComponent<Package>();

        if (packageComponent != null)
        {
            ScoreManager.Instance.AddPoints(packageComponent.pointValue);
            Debug.Log($"Package was collected! Player earned {packageComponent.pointValue} points.");
            if (ScoreManager.Instance.GetScore() >= 200)
            {
                GameManager.Instance.EndGameWin();
            }
        }

        Destroy(collision.gameObject);
    }
}
