using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public class TileController : MonoBehaviour
{
    public enum TileType
    {
        RedFloor,
        TopWall
    }

    public TileType tileType;

    // Start is called before the first frame update
    void Start()
    {
        switch(tileType)
        {
            case TileType.RedFloor:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Floor_01");
                break;
            case TileType.TopWall:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Floor_02");
                gameObject.AddComponent<BoxCollider2D>();
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Floor_01");
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
