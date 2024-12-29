using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.Paint
{
    public class Tile : MonoBehaviour
    {
        ColorType colorType;

        public void OnInit(ColorType type)
        {
            this.colorType = type;
        }

        public void SetColor(ColorType type)
        {
            this.colorType = type;
        }

    }
}