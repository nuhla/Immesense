using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class BallMoveController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool listed = false;

    [SerializeField]
    private float speed = 80;

    [SerializeField]
    private float offset = 2;
    private int index = -1;
    private int i = 0;

    private LineRenderer lineRenderer;
    public bool startMove = false;
    GameManager gameManager;

    private GameObject PreviuseItem;
    void Start()
    {
        gameManager = Camera.main.gameObject.GetComponent<GameManager>();


    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        if (!listed)
        {

            //-------------------- get the Path we want to move the ball on --------------------------//
            lineRenderer = gameManager.SelectedPath;

            //-------------------- get the Path we want to move the ball on --------------------------//
            lineRenderer.transform.gameObject.GetComponent<LineController>().objectsToMove.Add(this.gameObject);


            index = lineRenderer.transform.gameObject.GetComponent<LineController>().objectsToMove.Count - 1;
            Debug.Log(lineRenderer.transform.gameObject.GetComponent<LineController>().objectsToMove.Count);
            if (index > 0)
            {
                PreviuseItem = lineRenderer.transform.gameObject.GetComponent<LineController>().objectsToMove[index - 1];
            }
            listed = !listed;
            startMove = true;



        }
    }

    private void Update()
    {
        //-------------------------------------------------------------------------------------//
        //-------- Checking Movement and Distance between each ball and the Previous one ------//
        //-------------------------------------------------------------------------------------//
        if (lineRenderer != null)
        {
            if (index > 0 && i != lineRenderer?.positionCount - 1)
            {
                float distance2 = Vector3.Distance(PreviuseItem.transform.position, this.transform.position);
                if (distance2 < 20 && i > index)
                {
                    i--;
                    if (!PreviuseItem.transform.GetComponent<BallMoveController>().startMove)
                    {
                        startMove = false;
                    }
                }

            }
            //-------------------------------------------------------------------------------------//
            //--------------- Start The Movent After Selecting the Ball and The Path --------------//
            //-------------------------------------------------------------------------------------//

            if (startMove)
            {

                // -------------- get the point of the line to go to One By One ------------------//
                Vector3 destination = lineRenderer.GetPosition(i);

                Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                transform.position = newPos;

                float distan = Vector3.Distance(transform.position, destination);


                if (distan < 1.2f)
                {
                    if (i < lineRenderer.positionCount - 1) i++;

                    else startMove = false;

                }

            }


        }

    }

    Vector3 FindProperPoint()
    {
        float distnation = 20;
        Vector3 point = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        for (int i = lineRenderer.positionCount - 1; i > 0; i--)
        {

            float dis = Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(lineRenderer.positionCount - 1));

            if ((distnation * index) < dis) { point = lineRenderer.GetPosition(i); break; }


        }

        return point;
    }


}
