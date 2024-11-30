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
        public bool IsEmpty => line == null;

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
            if (this.line != null)
            {
                //line.AddIndex(Index);
                //SetColor(line.Color);
            }
        }

        public void ResetLine()
        {
            this.line = null;
        }

        public void Select(bool select)
        {
            highlight.SetActive(select);
        }

    }
}