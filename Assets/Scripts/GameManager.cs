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
    public LineRenderer SelectedPath;


    void Start()
    {
        previousePosition = lineRenderer.transform.position;
        points = new List<Vector3>();

        Vector3 offset = new Vector3(mindistanceX, 0, mindistanceZ);

    }

    // Update is called once per frame
    void Update()
    {

        /// ---------------------- Cast Ray and see what it hits ----------------------//
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // --------------------- if it hits the terrain we will---------------//
                //-----------------------instantiate a new Line Instance -------------//
                //---------------------- and start drawing the line on the terian ----// 
                if (hit.collider.gameObject == terrain.gameObject)
                {
                    isDrwaing = true;
                    points = new List<Vector3>();

                    //----------------- istantiate the line (it should be a prefab but its okay now to keep it in the sceen )
                    CurrentLine = GameObject.Instantiate<LineRenderer>(lineRenderer);
                    //- ------ index here just for line naming 
                    index++;
                    CurrentLine.gameObject.transform.name = "Line" + index;
                }
            }


        }
        else if (Input.GetMouseButtonUp(0))
        {
            //---------------- when use un-press mouse drawing the line is done ------------------//
            isDrwaing = false;

        }

        if (Input.GetMouseButton(0) && isDrwaing)
        {

            //------------- the mouse is still pressed and drawing is working-----------------------//
            //------------ takes the hit point and saves it in the line renderer -------------------//

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
