using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CustomScript : MonoBehaviour
{
    [SerializeField]
    Material customMaterial;
    [SerializeField]
    Material notSelecte;

    GameObject parent;
    GameManager gameManager;


    private bool selected = false;
    // Start is called before the first frame update
    void Start()
    {

        gameManager = Camera.main.gameObject.GetComponent<GameManager>();
        parent = transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.SelectedPath != null)
        {
            
            if (parent.name == gameManager.SelectedPath.transform.gameObject.name) selected = true;
            else selected = false;

            SetMaterial(selected);
        }
    }

    private void OnMouseDown()
    {

        selected = true;
        gameManager.SelectedPath= parent.GetComponent<LineRenderer>();


    }


    private void SetMaterial(bool selected)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = selected ? customMaterial : notSelecte;
    }
}
