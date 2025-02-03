using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class SelectTove : MonoBehaviour
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
    private bool startMove = false;
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

            //transform.SetParent(gameManager.SelectedPath.transform.GetChild(1), true);
            lineRenderer = gameManager.SelectedPath;
            lineRenderer.transform.gameObject.GetComponent<move>().objectsToMove.Add(this.gameObject);
            index = lineRenderer.transform.gameObject.GetComponent<move>().objectsToMove.Count - 1;
            Debug.Log(lineRenderer.transform.gameObject.GetComponent<move>().objectsToMove.Count);
            if(index> 0){
            PreviuseItem = lineRenderer.transform.gameObject.GetComponent<move>().objectsToMove[index - 1];
            }
            listed = !listed;
            startMove = true;



        }
    }

    private void Update()
    {
        if (lineRenderer != null)
        {
            if (index > 0 && i != lineRenderer?.positionCount - 1)
            {
                float distance2 = Vector3.Distance(PreviuseItem.transform.position, this.transform.position);
                if (distance2 < 20)
                {
                    startMove = false;
                }
            }
            else
            {
                startMove = true;
            }
      

        if (startMove)
        {


            Vector3 destination = lineRenderer.GetPosition(i);


            if (i == lineRenderer.positionCount - 1 && index != 0)
            {
                speed *= .7f;
                destination = FindProperPoint();

            }
            Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            transform.position = newPos;

            float distan = Vector3.Distance(transform.position, destination);


            if (distan < 1.2f)
            {
                if (i < lineRenderer.positionCount - 1)
                {

                    i++;

                }
                else
                {
                    startMove = false;
                }
            }

            }


        }

        // CheckDistanse();

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
