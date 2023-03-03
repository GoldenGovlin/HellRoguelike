using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetermineDirection();

        UpdateAnimation();

        UpdatePosition();

    }

    private void UpdatePosition()
    {
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x + (PlayerVector.x * MoveSpeed * Time.deltaTime),
            gameObject.transform.position.y + (PlayerVector.y * MoveSpeed * Time.deltaTime),
            0.0f
            );
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
