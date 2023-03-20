using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCInfo : MonoBehaviour
{
 
    public TextMeshProUGUI NameValueField;

    [SerializeField]
    [HideInInspector]
    PawnData data;

    [SerializeField]
    [HideInInspector]
    CanvasGroup group;

    private void Awake()
    {
        var TextChilds = gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var child in TextChilds)
        {
            if(child.gameObject.name == "NPCName")
            {
                NameValueField = child;
            }
        }

        group = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeData(PawnData npcdata)
    {
        data = npcdata;

        UpdateView();
    }

    private void UpdateView()
    {
        NameValueField.text = data.Name;
    }

    public void Show()
    {
        group.alpha = 1.0f;
        print("Show");
    }

    public void Hide()
    {
        group.alpha = 0.0f;
        print("Hide");
    }
}
