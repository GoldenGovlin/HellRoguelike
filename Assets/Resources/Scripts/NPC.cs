using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    NPCInfo NPCInfoView;

    [SerializeField]
    [HideInInspector]
    private bool IsActiveTurn;

    private void Awake()
    {
        IsActiveTurn = false;
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

        GetComponent<GameActor>().OnTurnStart.AddListener(NPCTurn);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsActiveTurn)
        {
            print($"Turno de: {gameObject.GetComponent<PawnData>().Name}");
            FinishNPCTurn();
        }
    }

    void OnSelectedNPC()
    {
        NPCInfoView.InitializeData(GetComponent<PawnData>());
        NPCInfoView.Show();
    }

    void OnDeselectedNPC()
    {
        NPCInfoView.Hide();
    }

    void NPCTurn()
    {
        IsActiveTurn = true;
    }

    void FinishNPCTurn()
    {
        IsActiveTurn = false;
        StartCoroutine(CoUpdate());
    }

    IEnumerator CoUpdate()
    {
        yield return new WaitForSeconds(2);
        GetComponent<GameActor>().FinishTurn();
        // Very important, t$$anonymous$$s tells Unity to move onto next frame. Everyt$$anonymous$$ng crashes without t$$anonymous$$s
        yield return null;
    }
}
