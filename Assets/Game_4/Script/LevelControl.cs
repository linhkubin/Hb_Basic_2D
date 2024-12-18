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
        Tile tileSelect, tileNew;

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
            tiles[1, 1].OnInit(ColorType.Blue, new Vector2Int(1, 1));
            tiles[3, 6].OnInit(ColorType.Blue, new Vector2Int(3, 6));
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                tileSelect = GetTile(GetPoint());
                if (tileSelect != null)
                {
                    lineSelecting = tileSelect.Line;
                    SelectTile(tileSelect);
                }
                if (lineSelecting == null && tileSelect.IsRoot)
                {
                    lineSelecting = Instantiate(linePrefab);
                    tileSelect.SetLine(lineSelecting);
                    lineSelecting.OnInit(tileSelect.Color, tileSelect.Index);
                }
                if (lineSelecting != null)
                {
                    lineSelecting.SetSelectTile(tileSelect);
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (lineSelecting != null)
                {
                    tileNew = GetTile(GetPoint());
                    if (tileNew != null && tileSelect != tileNew && IsNearTile(this.tileSelect, tileNew))
                    {
                        //chia ra cac truong hop
                        //if (tile_2 != null && tile_2.IsRoot && lineSelecting.IsTileLast(this.tile_2, 1) && lineSelecting.IsTileLast(tile_1, 2))
                        //{
                        //    //tile la root thi chi xoa di duoc 
                        //    lineSelecting.RemoveIndex(this.tileSelect.Index);
                        //    SelectTile(tile_1);
                        //    Debug.Log(2);
                        //}
                        //else
                        //if (tile_1.IsEmpty && this.tileSelect != tile_1 && tile_1.Line != lineSelecting)
                        //{
                        //    //tile k mau
                        //    SelectTile(tile_1);

                        //    tile_1.SetLine(lineSelecting);
                        //    lineSelecting.AddIndex(tile_1.Index);
                        //    Debug.Log(1);
                        //}
                        //else
                        //if (this.tileSelect != tile_1 && tile_1.Line == lineSelecting && lineSelecting.IsTileLast(this.tileSelect, 1) && lineSelecting.IsTileLast(tile_1, 2))
                        //{
                        //    //tile cung line
                        //    lineSelecting.RemoveIndex(this.tileSelect.Index);
                        //    SelectTile(tile_1);
                        //    Debug.Log(3);
                        //}
                        if (tileNew.IsEmpty)
                        {
                            if (tileSelect.IsRoot && tileSelect.IsLast())
                            {
                                
                            }
                            else
                            {
                                Debug.Log(3);
                                //tile k mau
                                SelectTile(tileNew);
                                tileNew.SetLine(lineSelecting);
                                lineSelecting.AddIndex(tileNew.Index);
                            }
                        }
                        else
                        if (!tileNew.IsRoot)
                        {
                            if (tileSelect.IsSameLine(tileNew))
                            {
                                Debug.Log(2);
                                if (tileNew.IsPreLast() && tileSelect.IsLast())
                                {
                                    lineSelecting.RemoveIndex(this.tileSelect.Index);
                                    SelectTile(tileNew);
                                }
                            }
                            else
                            if (tileSelect.IsSameColor(tileNew))
                            {
                                Debug.Log(1);
                                tileNew.Line.RemoveIndexBehind(tileNew.Index);
                                tileSelect.Line.RemoveIndexBehind(tileSelect.Index);
                                MergeLine(tileSelect.Line, tileNew.Line);
                                SelectTile(null);
                                lineSelecting = null;
                            }
                        }
                        else
                        if (tileNew.IsRoot && tileSelect.IsSameColor(tileNew))
                        {
                            SelectTile(tileNew);
                            tileNew.SetLine(lineSelecting);
                            lineSelecting.AddIndex(tileNew.Index);
                        }

                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                SelectTile(null);
            }
        }

        internal void UpdateTile(Line line, List<Vector2Int> indexs)
        {
            for (int i = 0; i < indexs.Count; i++)
            {
                tiles[indexs[i].x, indexs[i].y].UpdateLine(line);
            }
        }

        private void SelectTile(Tile tile)
        {
            if (this.tileSelect != null)
            {
                this.tileSelect.Select(false);
            }
            this.tileSelect = tile;
            if (this.tileSelect != null)
            {
                this.tileSelect.Select(true);
            }
        }

        private void MergeLine(Line line_1, Line line_2)
        {
            line_1.AddLine(line_2);
            Destroy(line_2.gameObject);
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