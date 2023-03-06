using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent OnDeselect;

    [SerializeField]
    [HideInInspector]
    private bool CanUseClickSelector = true;
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
                    // Se ha detectado un objeto, hacer algo con él.
                    SelectedScript.Select();
                }
            }
            else
            {
                OnDeselect.Invoke();
            }
        }
    }

    private void FixedUpdate()
    {
        //if (CanUseClickSelector && Input.GetMouseButtonDown(0))
        //{
        //    print("Click");

        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    print(ray.ToString());
        //    RaycastHit hit;
            
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        print(hit.distance.ToString());
        //        // Se ha detectado un objeto, hacer algo con él.
        //        print(hit.transform.gameObject.name);
        //    }
        //}
    }
}
