using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public class TileController : MonoBehaviour
{
    public enum TileType
    {
        RedFloor,
        TopWall,
        FogTile
    }

    public TileType tileType;

    // Start is called before the first frame update
    void Start()
    {
        switch(tileType)
        {
            case TileType.RedFloor:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tiles/Floor_01");
                break;
            case TileType.TopWall:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tiles/Floor_02");
                gameObject.AddComponent<BoxCollider2D>();
                break;
            case TileType.FogTile:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tiles/FogTile");
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tiles/Floor_01");
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
