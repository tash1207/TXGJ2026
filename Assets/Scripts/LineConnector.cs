using UnityEngine;

public class LineConnector : MonoBehaviour
{
    [SerializeField] Transform clawTop;
    [SerializeField] Transform clawBottom;

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, clawTop.position);
        lineRenderer.SetPosition(1, clawBottom.position);
    }
}
