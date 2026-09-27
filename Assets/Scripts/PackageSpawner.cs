using System.Collections;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] packages;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float conveyorBeltTime;

    void Start()
    {
        StartCoroutine(MovementIntervalLoop());
    }

    void SpawnRandomPackages()
    {
        for (int i = 0; i < 13; i++)
        {
            int row1Index = Random.Range(0, packages.Length);
            GameObject packagePrefab1 = packages[row1Index];
            int row2Index = Random.Range(0, 7);
            GameObject packagePrefab2 = packages[row2Index];
            int row3Index = Random.Range(0, 7);
            GameObject packagePrefab3 = packages[row3Index];

            // Spawn packages off screen to the left and have them move right with the rest.
            Vector2 location1 = new Vector2(i - 25, -1.8f);
            Instantiate(packagePrefab1, location1, Quaternion.identity);
            Vector2 location2 = new Vector2(i - 25, -.8f);
            Instantiate(packagePrefab2, location2, Quaternion.identity);
            Vector2 location3 = new Vector2(i - 25, .2f);
            Instantiate(packagePrefab3, location3, Quaternion.identity);
        }
    }

    private IEnumerator MovementIntervalLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomPackages();
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Package");
            foreach (GameObject target in targets)
            {
                StartCoroutine(GradualMoveRight(target.transform));
            }
        }
    }

    private IEnumerator GradualMoveRight(Transform targetTransform)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = targetTransform.position;
        Vector3 endPosition = startPosition + Vector3.right * 20;

        // Smoothly interpolate position over moveDuration
        while (elapsedTime < conveyorBeltTime)
        {
            // Ensure the object wasn't destroyed mid-movement
            if (targetTransform == null) yield break; 

            targetTransform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / conveyorBeltTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Snap precisely to the final position
        if (targetTransform != null)
        {
            targetTransform.position = endPosition;
            if (targetTransform.position.x > 10f)
            {
                // Destroy objects moved off screen.
                Destroy(targetTransform.gameObject);
            }
        }
    }
}
