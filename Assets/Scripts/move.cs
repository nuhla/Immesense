using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class move : MonoBehaviour
{

    public List<Vector3> waypoints; // Assign path points in the Inspector

    private Vector3[] positions = new Vector3[397];
    public List<GameObject> objectsToMove; // Assign objects that will move along the path
    // public float speed = 2f;
    // public float spacing = 2f; // Distance between objects

    // public List<int> currentWaypointIndices = new List<int>();
    // public List<float> distancesToNextWaypoint = new List<float>();
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        objectsToMove = new List<GameObject>();
        lineRenderer = GetComponent<LineRenderer>(); 
        GetLinePointsInWorldSpace();


    }

    Vector3[] GetLinePointsInWorldSpace()
    {
        //Get the positions which are shown in the inspector 
        lineRenderer.GetPositions(positions);

        //the points returned are in world space
        return positions;
    }


    // Update is called once per frame
    void Update()
    {
       // Debug.Log(objectsToMove.Count());

        // for (int i = 0; i < objectsToMove.Length; i++)
        // {
        //     if (currentWaypointIndices[i] < waypoints.Length - 1) // Check if there's a next waypoint
        //     {
        //         Transform currentWaypoint = waypoints[currentWaypointIndices[i]];
        //         Transform nextWaypoint = waypoints[currentWaypointIndices[i] + 1];

        //         Vector3 direction = (nextWaypoint.position - currentWaypoint.position).normalized;
        //         float step = speed * Time.deltaTime;

        //         // Move the object gradually towards the next waypoint
        //         objectsToMove[i].transform.position += direction * step;

        //         // Check if the object reached the next waypoint
        //         if (Vector3.Distance(objectsToMove[i].transform.position, nextWaypoint.position) < 0.1f)
        //         {
        //             currentWaypointIndices[i]++;
        //         }

        //         // Ensure objects maintain spacing by slowing down if they get too close
        //         if (i > 0)
        //         {
        //             float distanceToPrevious = Vector3.Distance(objectsToMove[i].transform.position, objectsToMove[i - 1].transform.position);
        //             if (distanceToPrevious < spacing)
        //             {
        //                 objectsToMove[i].transform.position -= direction * step * 0.5f; // Slightly slow down
        //             }
        //         }
        //     }
        // }
    }
}
