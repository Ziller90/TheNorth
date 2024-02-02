using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Route : MonoBehaviour
{
    [SerializeField] List<Vector3> corners;
    [SerializeField] bool looped;
    [SerializeField] float navMeshYOffset;

    [SerializeField] Color cornersColor = Color.black;
    [SerializeField] Color linesColor = Color.white;

    public Color CornersColor => cornersColor;
    public Color LinesColor => linesColor;

    public List<Vector3> Corners { get => corners; set => corners = value; }
    public bool Looped { get => looped; set => looped = value; }

    //Can't be used in locationPrefab, only on scene
    public void AttachCornersToNavMesh()
    {
        for (int i = 0; i < corners.Count; i++)
        {
            NavMeshHit hit;

            if (NavMesh.SamplePosition(corners[i], out hit, 3f, NavMesh.AllAreas))
                corners[i] = hit.position + Vector3.up * navMeshYOffset;
            else
                Debug.LogWarning($"Точка {corners[i]} не была пересечена с NavMesh. Проверьте её положение или высоту NavMesh.");
        }
    }

    //Can't be used in locationPrefab, only on scene
    public void AttachCornersToColliderBelow()
    {
        float rayStartHeight = 2f;
        float rayDistance = 5f;
        LayerMask layerMask = ~0;

        for (int i = 0; i < corners.Count; i++)
        {
            Vector3 rayStart = corners[i] + Vector3.up * rayStartHeight;
            RaycastHit hit;

            if (Physics.Raycast(rayStart, Vector3.down, out hit, rayDistance, layerMask))
                corners[i] = hit.point;
            else
                Debug.LogWarning($"Точка {corners[i]} не имеет под собой ни одного коллайдера.");
        }
    }
}