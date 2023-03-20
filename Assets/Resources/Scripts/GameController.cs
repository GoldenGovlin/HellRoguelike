using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameStart;
    public UnityEvent OnGameEnd;

    public int Cycle;

    [SerializeField]
    [HideInInspector]
    List<GameObject> InGameActors;

    [SerializeField]
    [HideInInspector]
    int CurrentGameActor;

    public bool CanProcessTurn;

    public Button FinishTurnButton;

    private void Awake()
    {
        CurrentGameActor = 0;
        CanProcessTurn = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoUpdate());
        FinishTurnButton.GetComponent<Button>().onClick.AddListener(FinishTurn);
    }

    // Update is called once per frame
    void Update()
    {
        if (CanProcessTurn)
        {
            CanProcessTurn = false;
            NextGameActorTurn();
        }
    }

    private void StartGame()
    {
        UpdateTurnOrder();
        PrintInGameActors();
        OnGameStart.Invoke();
    }

    private void EndGame()
    {
        OnGameEnd.Invoke();
    }

    public void JoinGame(GameObject pawn)
    {
        UnityEngine.Debug.Log($"Joined: {pawn.GetComponent<PawnData>().Name}");
        InGameActors.Add(pawn);
        pawn.GetComponent<GameActor>().OnTurnEnd.AddListener(FinishTurn);
    }


    private void FinishRound()
    {
        UpdateTurnOrder();
    }

    public void FinishTurn()
    {
        print("FinishTurn()");
        CanProcessTurn = true;
    }

    private void NextGameActorTurn()
    {
        InGameActors[CurrentGameActor].GetComponent<GameActor>().DoTurn();
        CurrentGameActor = (CurrentGameActor + 1) % InGameActors.Count;
    }



    private void UpdateTurnOrder()
    {
        for (int i = 0; i < InGameActors.Count; i++)
        {
            GameObject Pawn = InGameActors[i];

            for (int j = 0; j < InGameActors.Count; j++)
            {
                GameObject Other = InGameActors[j];

                if (Pawn.GetComponent<PawnData>().Initiative > Other.GetComponent<PawnData>().Initiative)
                {
                    InGameActors[j] = InGameActors[i];
                    InGameActors[i] = Other;
                }
            }
        }

        CanProcessTurn = true;
    }

    private void PrintInGameActors()
    {
        foreach (GameObject Pawn in InGameActors)
        {
            print(Pawn.GetComponent<PawnData>().Name);
        }
    }

    IEnumerator CoUpdate()
    {
        yield return new WaitForSeconds(1);
        StartGame();

        // Very important, t$$anonymous$$s tells Unity to move onto next frame. Everyt$$anonymous$$ng crashes without t$$anonymous$$s
        yield return null;
    }
}
