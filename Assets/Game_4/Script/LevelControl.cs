using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Link.LineConnect
{
    public enum ColorType
    {
        None = 0,
        Red = 1,
        Blue = 2,
        Green = 3, 
        Yellow = 4,
        Orange = 5,
        Violet = 6,
        Brown = 7,
    }

    public class LevelControl : MonoBehaviour
    {
        public static LevelControl Ins { get; private set; }

        [SerializeField] Transform tileRoot;
        [SerializeField] Tile tilePrefab;
        [SerializeField] Line linePrefab;

        [SerializeField] private Tile[,] tiles = new Tile[5, 8];
        Vector3 point;
        Line lineSelecting;
        Tile tile;

        private void Awake()
        {
            Ins = this;
        }

        private void Start()
        {
            OnInitTile(5, 8);
            tiles[0, 0].OnInit(ColorType.Red, new Vector2Int(0, 0));
            tiles[4, 7].OnInit(ColorType.Red, new Vector2Int(4, 7));     
            
            tiles[1, 0].OnInit(ColorType.Blue, new Vector2Int(1, 0));
            tiles[4, 6].OnInit(ColorType.Blue, new Vector2Int(4, 6));
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                tile = GetTile(GetPoint());
                if (tile != null)
                {
                    lineSelecting = tile.Line;
                    SelectTile(tile);
                }
                if (lineSelecting == null && tile.IsRoot)
                {
                    lineSelecting = Instantiate(linePrefab);
                    tile.SetLine(lineSelecting);
                    lineSelecting.OnInit(tile.Color, tile.Index);
                }
                if (lineSelecting != null)
                {
                    lineSelecting.SetSelectTile(tile);
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (lineSelecting != null)
                {
                    Tile tile = GetTile(GetPoint());
                    if (tile != null && IsNearTile(this.tile, tile))
                    {
                        //chia ra các truong hop

                        if (tile.IsEmpty && this.tile != tile && tile.Line != lineSelecting)
                        {
                            //tile k mau
                            SelectTile(tile);

                            tile.SetLine(lineSelecting);
                            lineSelecting.AddIndex(tile.Index);
                        }
                        else
                        if (this.tile != tile && tile.Line == lineSelecting && lineSelecting.IsTileLast(this.tile, 1) && lineSelecting.IsTileLast(tile, 2))
                        {
                            //tile cung line
                            lineSelecting.RemoveIndex(this.tile.Index);

                            SelectTile(tile);

                            tile.SetLine(lineSelecting);
                            lineSelecting.AddIndex(tile.Index);
                        }

                        //tile khac line

                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                SelectTile(null);
            }
        }

        private void SelectTile(Tile tile)
        {
            if (this.tile != null)
            {
                this.tile.Select(false);
            }
            this.tile = tile;
            if (this.tile != null)
            {
                this.tile.Select(true);
            }
        }

        public Vector3 GetPoint()
        {
            point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0;
            return point;
        }

        public void OnInitTile(int x, int y)
        {
            Vector2 startPoint = new Vector2( -(x - 1)* 0.5f , -(y - 1) * 0.5f);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    tiles[i, j] = Instantiate(tilePrefab, startPoint + new Vector2(i, j), Quaternion.identity);
                    tiles[i, j].OnInit(ColorType.None, new Vector2Int(i, j));
                    tiles[i, j].name = $"Tile {x}-{y}";
                }
            }
        }

        public Vector3 GetTilePoint(Vector2Int index)
        {
            return tiles[index.x, index.y].transform.position;
        }

        public Tile GetTile(Vector2Int index)
        {
            return tiles[index.x, index.y];
        }

        public Tile GetTile(Vector3 point)
        {
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            if (hit.collider != null)
            {
                return hit.collider.GetComponent<Tile>();
            }
            else
            {
                return null;
            }
        }

        private bool IsNearTile(Tile tile_1, Tile tile_2)
        {
            return (tile_1.Index.x == tile_2.Index.x && Mathf.Abs(tile_1.Index.y - tile_2.Index.y) == 1) || (tile_1.Index.y == tile_2.Index.y && Mathf.Abs(tile_1.Index.x - tile_2.Index.x) == 1);
        }
    }
}