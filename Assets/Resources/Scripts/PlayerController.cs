using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using System.Drawing;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum PlayerDirection
    {
        Down, DownRight, Right, TopRight, Top, TopLeft, Left, DownLeft
    }

    [SerializeField]
    [HideInInspector]
    Vector2 PlayerVector;

    public PlayerDirection playerDirection;

    public float MoveSpeed;

    public bool UseAnimation;

    // Sprites used when no animation
    public Sprite[] TemporalSprites;

    // References
    [SerializeField]
    [HideInInspector]
    GameObject LevelManager;

    [SerializeField]
    [HideInInspector]
    bool IsPlayerTurn;

    [SerializeField]
    [HideInInspector]
    private bool IsActiveTurn;

    public Button FinishTurnButton;

    private void Awake()
    {
        IsActiveTurn = false;
        IsPlayerTurn = false;
        LevelManager = GameObject.FindWithTag("LevelManager");
        GetComponent<GameActor>().OnTurnStart.AddListener(PlayerTurn);
        FinishTurnButton.GetComponent<Button>().onClick.AddListener(FinishPlayerTurn);

    }

    // Start is called before the first frame update
    void Start()
    {
       

        
    }

    // Update is called once per frame
    void Update()
    {
        // DetermineDirection();

        // UpdateAnimation();
        if(IsActiveTurn)
        {

        }

    }

    void PlayerTurn()
    {
        IsActiveTurn = true;
        print($"Turno de: {gameObject.GetComponent<PawnData>().Name}");
        GameObject.FindWithTag("GameManager").GetComponent<ClickSelector>().SetCanClick(true);
    }

    void FinishPlayerTurn()
    {
        if (IsActiveTurn)
        {
            print("Finish DoTurn");
            IsActiveTurn = false;
            GameObject.FindWithTag("GameManager").GetComponent<ClickSelector>().SetCanClick(false);
            GetComponent<GameActor>().FinishTurn();
        }
    }

    private void DetermineDirection()
    {
        PlayerVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (PlayerVector.x > 0)
        {
            if (PlayerVector.y > 0)
            {
                playerDirection = PlayerDirection.TopRight;
            }
            else if (PlayerVector.y < 0)
            {
                playerDirection = PlayerDirection.DownRight;
            }
            else
            {
                playerDirection = PlayerDirection.Right;
            }
        }
        else if (PlayerVector.x < 0)
        {
            if (PlayerVector.y > 0)
            {
                playerDirection = PlayerDirection.TopLeft;
            }
            else if (PlayerVector.y < 0)
            {
                playerDirection = PlayerDirection.DownLeft;
            }
            else
            {
                playerDirection = PlayerDirection.Left;
            }
        }
        else
        {
            if (PlayerVector.y > 0)
            {
                playerDirection = PlayerDirection.Top;
            }
            else
            {
                playerDirection = PlayerDirection.Down;
            }
        }
    }

    private void UpdateAnimation()
    {
        if(!UseAnimation)
        {
            GetComponent<SpriteRenderer>().sprite = TemporalSprites[((int)playerDirection)];
        }
    }
}
