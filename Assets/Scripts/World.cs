using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NorskaLib.Utilities;

public class World : MonoBehaviour
{
    public static World Instance { get; private set; }
    [SerializeField] Transform startpoint;
    [SerializeField] Transform endpoint;

    public const float radius = 3f;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDrawGizmos()
    {
        if (startpoint == null || endpoint == null)
            return;

        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(startpoint.position, 0.3f);
        Gizmos.DrawSphere(endpoint.position, 0.3f);

        Gizmos.DrawLine(startpoint.position, endpoint.position);
    }

    public Vector3 Project(Vector3 point)
    {
        var trajectory = endpoint.position - startpoint.position;
        var direction = point - startpoint.position;

        return Vector3.Project(direction, trajectory) + startpoint.position;
    }

    public Vector3 GetNormal(Vector3 point)
    {
        return point - Project(point);
    }
}
