using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GameActor : MonoBehaviour
{
    public UnityEvent OnTurnStart;
    public UnityEvent OnTurnEnd;

    [SerializeField]
    [HideInInspector]
    GameController GameController;

    public GameObject UnderTile;



    private void Awake()
    {
        GameController = GameObject.FindWithTag("GameManager").GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameController.JoinGame(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoTurn()
    {
        SetCameraFocusOnMe();

        UnderTile.SetActive(true);

        Debug.Log($"Turno de: {gameObject.GetComponent<PawnData>().Name}");
        OnTurnStart.Invoke();
    }

    public void FinishTurn()
    {
        UnderTile.SetActive(false);
        OnTurnEnd.Invoke();
    }

    private void SetCameraFocusOnMe()
    {
        Camera.main.gameObject.transform.SetParent(gameObject.transform);
        Vector3 DeltaVector = new Vector3(
            Camera.main.gameObject.transform.position.x - gameObject.transform.position.x,
            Camera.main.gameObject.transform.position.y - gameObject.transform.position.y,
            0.0f
            );


        Camera.main.gameObject.transform.position = Camera.main.gameObject.transform.position - DeltaVector;
    }
}
