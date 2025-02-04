using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Terrain terrain;
    [SerializeField]
    LineRenderer lineRenderer;
    [SerializeField]
    Material selectedMaterial;
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private GameObject spher;
    private List<Vector3> points;
    Vector3 previousePosition;
    private float offset;


    private bool isDrwaing = false;
    private float mindistanceX = 0.1f;
    private float mindistanceZ = 0.1f;

  

    private int index;
    private LineRenderer CurrentLine;
    public  LineRenderer SelectedPath;




    // Start is called before the first frame update
    void Start()
    {
        previousePosition = lineRenderer.transform.position;
        points = new List<Vector3>();

        Vector3 offset = new Vector3(mindistanceX, 0, mindistanceZ);

    }

    // Update is called once per frame
    void Update()
    {
        

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == terrain.gameObject)
                    {
                        isDrwaing = true;
                        points = new List<Vector3>();
                        CurrentLine = GameObject.Instantiate<LineRenderer>(lineRenderer);
                        index++;
                        CurrentLine.gameObject.transform.name = "Line" + index;
                    }
                }
                /// ---------------- Start Selection Mode to Move the Balls -----------------//
               


            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDrwaing = false;

            }

            if (Input.GetMouseButton(0) && isDrwaing)
            {
                

                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == terrain.gameObject)
                    {

                        DrawLineAndUpdateTerrian(CurrentLine, currentPosition, hit);
                    }
                    if (hit.collider.gameObject.name == "SelectItem")
                    {
                        //isDrwaing = false;
                        SelectedPath = hit.collider.gameObject.transform.parent.GetComponent<LineRenderer>();
                        SelectedPath.material = selectedMaterial;
                      
                    }

                }

            }
     

    }

    void DrawLineAndUpdateTerrian(LineRenderer CurrentLine, Vector3 currentPosition, RaycastHit hit)
    {
        
        Transform startPoint = CurrentLine.transform.GetChild(0);

        float terrainHeight = terrain.SampleHeight(hit.point);

        Vector3 curreentpoint = hit.point;
        curreentpoint.y = terrainHeight + 0.5f;
        if (points.Count == 0)
        {
            startPoint.position = curreentpoint;
        }

        points.Add(curreentpoint);

        CurrentLine.positionCount = points.Count;
        CurrentLine.SetPosition(CurrentLine.positionCount - 1, curreentpoint);


        previousePosition = currentPosition;


        Vector3 offset = new Vector3(curreentpoint.x + mindistanceX, curreentpoint.y, curreentpoint.z + mindistanceZ);
       

    }


}
