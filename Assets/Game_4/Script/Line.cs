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

        public ColorType Color { get; private set; }

        public void OnInit(ColorType colorType, Vector2Int index)
        {
            Color = colorType;
            lineRenderer.startColor = colorData.GetColor(colorType);
            lineRenderer.endColor = colorData.GetColor(colorType);

            indexs.Clear();
            indexs.Add(index);

        }

        public void AddIndex(Vector2Int index)
        {
            indexs.Add(index);
            lineRenderer.positionCount = indexs.Count;
            lineRenderer.SetPosition(indexs.Count - 1, LevelControl.Ins.GetTilePoint(index));
        }

        public void RemoveIndex(Vector2Int index)
        {
            if (IsTileRoot(indexs[^1]))
            {
                //split tile
            }
            else
            {
                //xoa toan bo tile cu di
                int id = indexs.IndexOf(index);

                for (int i = indexs.Count - 1; i >= id; i--)
                {
                    LevelControl.Ins.GetTile(index).ResetLine();
                }

                lineRenderer.positionCount = indexs.Count;
            }
        }


        public void RemoveIndexBehind(Vector2Int index)
        {
            //xoa toan bo tile cu di
            int id = indexs.IndexOf(index);

            for (int i = indexs.Count - 1; i > id; i--)
            {
                LevelControl.Ins.GetTile(index).ResetLine();
            }

            lineRenderer.positionCount = indexs.Count;
        }



        private bool IsTileRoot(Vector2Int index)
        {
            return LevelControl.Ins.GetTile(index).IsRoot;
        }
    }
}