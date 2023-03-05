using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LowLevel : MonoBehaviour
{
    public UnityEvent OnViewportSizeChanges;


    // camera management
    [SerializeField]
    [HideInInspector]
    Rect currentViewportSize;


    private void Awake()
    {
        currentViewportSize = Camera.main.pixelRect;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckViewportSize();
    }

    private void CheckViewportSize()
    {
        if (currentViewportSize != Camera.main.pixelRect)
        {
            currentViewportSize = Camera.main.pixelRect;
            OnViewportSizeChanges.Invoke();
        }
    }
}
