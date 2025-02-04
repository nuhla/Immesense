
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{


    public List<GameObject> objectsToMove; // Assign objects that will move along the path

    private LineRenderer lineRenderer;


    void Start()
    {
        //-------------------- Initialize GameObject --------------------//
        objectsToMove = new List<GameObject>();
        lineRenderer = GetComponent<LineRenderer>();


    }

    void Update()
    {

    }
}
