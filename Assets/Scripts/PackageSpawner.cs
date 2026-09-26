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
            int index = Random.Range(0, packages.Length);
            GameObject packagePrefab = packages[index];
            // Spawn packages off screen to the left and have them move right with the rest.
            Vector2 location = new Vector2(i - 25, -1.8f);
            Instantiate(packagePrefab, location, Quaternion.identity);
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
