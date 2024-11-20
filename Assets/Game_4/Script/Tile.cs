using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.LineConnect
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] ColorData colorData;
        [SerializeField] SpriteRenderer headRenderer;
        private ColorType color;

        public void OnInit(ColorType colorType)
        {
            SetColor(colorType);
            headRenderer.gameObject.SetActive(colorType != ColorType.None);
            headRenderer.color = colorData.GetColor(colorType);
        }

        public void SetColor(ColorType colorType)
        {
            color = colorType;
        }
    }
}