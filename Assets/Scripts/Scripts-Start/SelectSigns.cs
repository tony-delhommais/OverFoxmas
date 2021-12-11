using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSigns : MonoBehaviour
{
    /*[SerializeField]
    private Material hightlightMaterial;*/

    [SerializeField]
    private string selectableTag = "Selectable";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectPoint();
        }
    }
    private void selectPoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            //print(selection.gameObject);
            
            if (selection.CompareTag(selectableTag))
            { 
                var selectionRenderer = selection.GetComponent<ClickButtton>();
                selectionRenderer.OnClick();
                /*if(selectionRenderer != null)
                {
                    selectionRenderer.material = hightlightMaterial;
                    print(selection.gameObject);
                }*/
            
            }
        }
    }
}
