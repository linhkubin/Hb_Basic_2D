using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.LineConnect
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] ColorData colorData;
        [SerializeField] SpriteRenderer headRenderer;
        [SerializeField] GameObject highlight;
        public Vector2Int Index { get; private set; }
        public bool IsRoot => headRenderer.gameObject.activeSelf;
        public bool IsHaveLine => line;
        public Line Line => line;

        public ColorType Color => color;
        public bool IsEmpty => line == null && !IsRoot;

        private ColorType color;
        private Line line;

        public void OnInit(ColorType colorType, Vector2Int index)
        {
            line = null;
            this.Index = index;
            SetColor(colorType);
            headRenderer.gameObject.SetActive(colorType != ColorType.None);
            headRenderer.color = colorData.GetColor(colorType);
            Select(false);
        }

        public void SetColor(ColorType colorType)
        {
            color = colorType;
        }

        public void SetLine(Line line)
        {
            if (this.line != null)
            {
                line.RemoveIndex(Index);
            }
            this.line = line;
        }

        public void UpdateLine(Line line)
        {
            this.line = line;
        }

        public void ResetLine()
        {
            this.line = null;
        }

        public void Select(bool select)
        {
            highlight.SetActive(select);
        }

        internal bool IsSameLine(Tile tile)
        {
            return line != null && line == tile.line;
        }

        internal bool IsLast()
        {
            return line != null && line.IsTileLast(this, 1);
        }

        internal bool IsPreLast()
        {
            return line != null && line.IsTileLast(this, 2);
        }

        internal bool IsSameColor(Tile tile)
        {
            return line != null && tile.line != null && line.Color == tile.line.Color;
        }
    }
}