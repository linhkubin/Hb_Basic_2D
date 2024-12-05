using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.LineConnect
{
    public class Line : MonoBehaviour
    {
        [SerializeField] ColorData colorData;
        [SerializeField] LineRenderer lineRenderer;
        List<Vector2Int> indexs = new List<Vector2Int>();
        Tile selectTile = null;

        public ColorType Color { get; private set; }

        public void OnInit(ColorType colorType, Vector2Int index)
        {
            Color = colorType;
            lineRenderer.startColor = colorData.GetColor(colorType);
            lineRenderer.endColor = colorData.GetColor(colorType);

            indexs.Clear();
            AddIndex(index);
        }

        public void AddIndex(Vector2Int index)
        {
            if (selectTile != null)
            {
                RemoveIndexBehind(selectTile.Index);
                selectTile = null;
            }

            indexs.Add(index);
            lineRenderer.positionCount = indexs.Count;
            //lineRenderer.widthMultiplier = 0.4f;
            lineRenderer.SetPosition(indexs.Count - 1, LevelControl.Ins.GetTilePoint(index));
        }

        public void RemoveIndex(Vector2Int index)
        {
            //if (IsTileRoot(indexs[^1]))
            //{
            //    //split tile
            //}
            //else
            {
                //xoa toan bo tile cu di
                int id = indexs.IndexOf(index);
                if (id >= 0)
                {
                    //Debug.Log(id);
                    for (int i = indexs.Count - 1; i >= id; i--)
                    {
                        LevelControl.Ins.GetTile(index).ResetLine();
                        indexs.RemoveAt(i);
                    }

                    lineRenderer.positionCount = indexs.Count;
                }
            }
        }
        public void RemoveIndexBehind(Vector2Int index)
        {
            //xoa toan bo tile cu di
            int id = indexs.IndexOf(index);

            for (int i = indexs.Count - 1; i > id; i--)
            {
                LevelControl.Ins.GetTile(indexs[i]).ResetLine();
                indexs.RemoveAt(i);
            }
            lineRenderer.positionCount = indexs.Count;
        }

        public void SetSelectTile(Tile tile)
        {
            this.selectTile = tile;
        }
        public bool IsTileLast(Tile tile, int lastIndex)
        {
            return indexs.Count > 1 && indexs.Count >= lastIndex ? indexs[^lastIndex] == tile.Index : false;
        }
        public Vector2Int IndexTileLast(int lastIndex)
        {
            return indexs[^lastIndex];
        }

        internal void AddLine(Line line)
        {
            line.indexs.Reverse();
            indexs.AddRange(line.indexs);
            LevelControl.Ins.UpdateTile(this, indexs);
            UpdateLineRender();
        }

        private void UpdateLineRender()
        {
            lineRenderer.positionCount = indexs.Count;
            for (int i = 0; i < indexs.Count; i++)
            {
                lineRenderer.SetPosition(i, LevelControl.Ins.GetTilePoint(indexs[i]));
            }
        }

        internal bool IsDone()
        {
            return LevelControl.Ins.GetTile(indexs[^1]).IsRoot;
        }
    }
}