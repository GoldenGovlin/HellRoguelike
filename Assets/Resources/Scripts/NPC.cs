using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    NPCInfo NPCInfoView;

    private void Awake()
    {
        NPCInfoView = GameObject.FindWithTag("NPCInfo").GetComponent<NPCInfo>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SelectableScript NPCSelectableScript;

        if (NPCSelectableScript = gameObject.GetComponent<SelectableScript>())
        {
            NPCSelectableScript.OnSelected.AddListener(OnSelectedNPC);
            NPCSelectableScript.OnDeselected.AddListener(OnDeselectedNPC);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelectedNPC()
    {
        // print(LocalizationSettings.StringDatabase.GetLocalizedString(GetComponent<NPCData>().Name));

        //var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("NPCStringTable", GetComponent<NPCData>().Name);
        //if (op.IsDone)
        //{
        //    var Texts = GameObject.Find("NPCInfo").GetComponentsInChildren<TextMeshProUGUI>();

        //    foreach (var text in Texts)
        //    {
        //        if (text.name == "NameValue")
        //        {
        //            text.text = op.Result;
        //        }
        //    }
        //}
        //else
        //    op.Completed += (op) => 
        //    {
        //        var Texts = GameObject.Find("NPCInfo").GetComponentsInChildren<TextMeshProUGUI>();

        //        foreach (var text in Texts)
        //        {
        //            if(text.name == "NameValue")
        //            {
        //                text.text = op.Result;
        //            }
        //        }
        //    };
        NPCInfoView.InitializeData(GetComponent<NPCData>());
        NPCInfoView.Show();
    }

    void OnDeselectedNPC()
    {
        NPCInfoView.Hide();
    }
}
