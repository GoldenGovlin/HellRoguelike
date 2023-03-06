using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class MoveManager : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    GameObject InRangeTile;

    [SerializeField]
    [HideInInspector]
    private int MoveRange;

    [SerializeField]
    [HideInInspector]
    Grid LocalGrid;

    [SerializeField]
    [HideInInspector]
    private GameObject[,] TileList;

    [SerializeField]
    [HideInInspector]
    private bool IsGenerated = false;


    private void Awake()
    {
        InRangeTile = Resources.Load<GameObject>("Prefabs/Grid/RangeTile");

        MoveRange = GetComponent<NPCData>().MoveRange;

        LocalGrid = GetComponentInChildren<Grid>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SelectableScript>().OnSelected.AddListener(OnPawnSelected);
        GetComponent<SelectableScript>().OnDeselected.AddListener(OnPawnDeselected);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPawnSelected()
    {
        if(!IsGenerated)
            GenerateRangeTiles();

        ShowMoveGrid();
    }

    void OnPawnDeselected()
    {
        HideMoveGrid();
    }

    void ShowMoveGrid()
    {
        foreach (GameObject Tile in TileList)
        {
            Color ShowColor = Tile.GetComponent<SpriteRenderer>().color;
            ShowColor.a = 0.5f;
            Tile.GetComponent<SpriteRenderer>().color = ShowColor;
        }

        print("ShowMoveGrid");
    }

    void HideMoveGrid()
    {
        foreach(GameObject Tile in TileList)
        {
            Color HiddenColor = Tile.GetComponent<SpriteRenderer>().color;
            HiddenColor.a = 0.0f;
            Tile.GetComponent<SpriteRenderer>().color = HiddenColor;
        }

        print("HideMoveGrid");
    }

    void GenerateRangeTiles()
    {
        int RectSideInTiles = (MoveRange * 2) + 1;
        Vector3 DeltaOrigin = LocalGrid.CellToLocal(new Vector3Int(MoveRange, MoveRange));

        TileList = new GameObject[RectSideInTiles, RectSideInTiles];

        for (int i = 0; i < RectSideInTiles; i++)
        {
            for(int j = 0; j < RectSideInTiles; j++)
            {
                GameObject RangeTile = Instantiate<GameObject>(InRangeTile);
                RangeTile.transform.position = LocalGrid.CellToLocal(new Vector3Int(j, i, 0));
                RangeTile.transform.SetParent(LocalGrid.gameObject.transform);
                RangeTile.transform.position = RangeTile.transform.position - DeltaOrigin;
                TileList[j, i] = RangeTile;
            }
        }

        GameObject CenterTile = TileList[MoveRange, MoveRange];
        CenterTile.GetComponent<SpriteRenderer>().enabled = false;
        IsGenerated = true;
    }
}
