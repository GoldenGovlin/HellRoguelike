using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEditor.Experimental.GraphView.Port;

public class FogController : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    Sprite FogTile;

    [SerializeField]
    [HideInInspector]
    Vector2 ScreenSizeInTiles;

    public int ViewSpaceRadiusInTiles;

    GameObject[,] FogTiles;

    GameObject FogGrid;

    private void Awake()
    {
        GameObject.FindWithTag("Player").GetComponent<LowLevel>().OnViewportSizeChanges.AddListener(UpdateFogMap);

        FogTile = Resources.Load<Sprite>("Sprites/Tiles/FogTile");

        CalculateMapSize();

        FogGrid = GameObject.Find("FogGrid");

    }
    // Start is called before the first frame update
    void Start()
    {
        // GenerateFogMap();

        //for(int i = 0; i < ScreenSizeInTiles.x; i++) 
        //{
        //    GameObject TempTile = Instantiate(Resources.Load<GameObject>("Prefabs/Tile"));
        //    SpriteRenderer renderer = TempTile.GetComponent<SpriteRenderer>();
        //    TileController tileScript = TempTile.GetComponent<TileController>();

        //    tileScript.tileType = TileController.TileType.FogTile;
        //    renderer.sortingOrder = 50;
        //    renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
        //    TempTile.transform.position = FogGrid.GetComponent<Grid>().GetCellCenterWorld(new Vector3Int(i, 0, 0));
        //    TempTile.transform.SetParent(GameObject.Find("FogGrid").transform);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateFogMap()
    {
        CalculateMapSize();
        print("UPDATEFOGMAP");
    }

    private void CalculateMapSize()
    {
        ScreenSizeInTiles.x = Mathf.FloorToInt(GameObject.FindWithTag("MainCamera").GetComponent<PixelPerfectCamera>().refResolutionX / 32);
        ScreenSizeInTiles.y = Mathf.FloorToInt(GameObject.FindWithTag("MainCamera").GetComponent<PixelPerfectCamera>().refResolutionX / 32);
    }

    private void GenerateFogMap()
    {
        for(int i = 0; i < ScreenSizeInTiles.y; i++)
        {
            for(int j = 0; j < ScreenSizeInTiles.x; j++)
            {
                Grid grid = FogGrid.GetComponent<Grid>();
                float radius = ViewSpaceRadiusInTiles * 32;
                float minradius = (ViewSpaceRadiusInTiles - 1) * 32;
                float cellVectorMagnitude = grid.CellToWorld(new Vector3Int(j, i, 0)).magnitude;

                Debug.Log($"cellVectorMagnitude: {cellVectorMagnitude}");

                if (cellVectorMagnitude < radius && cellVectorMagnitude > minradius)
                {
                    GenerateFogTile(j, i, 0.5f);

                } 
                else if (cellVectorMagnitude < minradius) 
                {
                    GenerateFogTile(j, i, 0.0f);
                }
                else
                {
                    FogTiles[j, i] = GenerateFogTile(j, i, 1);
                }
            }
        }
    }

    private GameObject GenerateFogTile(int i, int j, float opacity)
    {
        print("Generate tile");
        GameObject TempTile = Instantiate(Resources.Load<GameObject>("Prefabs/Tile"));
        SpriteRenderer renderer = TempTile.GetComponent<SpriteRenderer>();
        TileController tileScript = TempTile.GetComponent<TileController>();

        tileScript.tileType = TileController.TileType.FogTile;
        renderer.sortingOrder = 50;
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, opacity);
        TempTile.transform.position = FogGrid.GetComponent<Grid>().GetCellCenterWorld(new Vector3Int(i, j, 0));
        TempTile.transform.SetParent(GameObject.Find("FogGrid").transform);

        return TempTile;
    }
}
