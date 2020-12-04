using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] public Transform point1;
    [SerializeField] public Transform point2;

    public void Configure(Vector3 pos1, Vector3 pos2)
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Set some positions
        Vector3[] positions = new Vector3[2];
        positions[0] = pos1;
        positions[1] = pos2;

        // lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }
}