using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;

public class ClickSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent OnDeselect;

    [SerializeField]
    [HideInInspector]
    private bool CanUseClickSelector = true;

    [SerializeField]
    [HideInInspector]
    GameObject ObjectSelected;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanUseClickSelector && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;

            if (hit = Physics2D.Raycast(ray.origin, ray.direction))
            {
                SelectableScript SelectedScript;

                if (SelectedScript = hit.transform.gameObject.GetComponent<SelectableScript>())
                {
                    if(ObjectSelected && !(SelectedScript.gameObject == ObjectSelected))
                    {
                        ObjectSelected.GetComponent<SelectableScript>().Deselect();
                        ObjectSelected = SelectedScript.gameObject;
                        // Se ha detectado un objeto, hacer algo con él.
                        SelectedScript.Select();
                    }
                    else
                    {
                        ObjectSelected = SelectedScript.gameObject;
                        SelectedScript.Select();
                    }
                }
            }
            else
            {
                OnDeselect.Invoke();
            }
        }
    }
}
