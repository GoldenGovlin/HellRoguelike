using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    [HideInInspector]
    private Vector2Int Size;

    [SerializeField]
    [HideInInspector]
    GameObject[,] Tiles;

    public bool GenerateRandom = true;

    public int Widht, Height;

    private void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        Size.x = Widht;
        Size.y = Height;
    }
    void Start()
    {
        GenerateRoom();
        //for (int i = 0; i < 10; i++)
        //{
        //    Vector2 RandomPoint = RandomPointRect(new Vector2Int(30, 20));
        //    print(RandomPoint.ToString());
        //GameObject Test = new GameObject("Test" + i.ToString());
        //Test.transform.position = GetComponent<Grid>().GetCellCenterWorld(new Vector3Int(Mathf.RoundToInt(RandomPoint.x), Mathf.RoundToInt(RandomPoint.y), 0));

        //SpriteRenderer TSR = Test.AddComponent<SpriteRenderer>();
        //TSR.sprite = Resources.Load<Sprite>("PlayerBase");
        //}
    }

    private void GenerateRoom()
    {
        // Determinar anchura y altura
        if(GenerateRandom)
        {
            Size.x = Random.Range(10, 41);
            Size.y = Random.Range(10, 41);
        }

        Tiles = new GameObject[Size.x, Size.y];

        Debug.Log($"Room size: {Size}");

        // Seleccionar y posicionar tiles
        for (int i = 0; i < Size.y; i++)
        {
            for(int j = 0; j < Size.x;  j++)
            {
                if (i == 0 || i == Size.y - 1)
                {
                    GenerateTile(i, j, TileController.TileType.TopWall);
                }
                else
                {
                    if(j == 0 || j == Size.x - 1)
                    {
                        GenerateTile(i, j, TileController.TileType.TopWall);
                    }
                    else
                    {
                        GenerateTile(i, j, TileController.TileType.RedFloor);
                    }
                }
            }
        }

        Debug.Log($"TileList size: {Tiles.Length}");

        // Generar y situar los colliders externos

    }

    private void GenerateTile(int i, int j, TileController.TileType type)
    {
        GameObject Loaded = Resources.Load<GameObject>("Prefabs/Tile");
        Vector3 DeltaOrigin = GetComponent<Grid>().CellToLocal(new Vector3Int(Size.x / 2, Size.y / 2));


        GameObject TempTile = Instantiate(Loaded);
        TempTile.name = $"Tile[{j.ToString()}, {i.ToString()}]";
        TempTile.transform.position = GetComponent<Grid>().CellToLocal(new Vector3Int(j, i, 0)) - DeltaOrigin;
        TempTile.transform.SetParent(GameObject.Find("LevelTiles").transform);
        TileController tileScript = TempTile.GetComponent<TileController>();
        tileScript.tileType = type;
        Tiles[j, i] = TempTile;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector2 RandomPointRect(Vector2Int Rectangle)
    {
        Vector2 Point;

        Point.x = Random.Range(0, Rectangle.x);
        Point.y = Random.Range(0, Rectangle.y);

        return Point;
    }

    public Vector2Int GetCenter()
    {
        Vector2Int Center = new Vector2Int();
        Center.x = Mathf.FloorToInt(Size.x / 2);
        Center.y = Mathf.FloorToInt(Size.y / 2);
        return Center;
    }
}
