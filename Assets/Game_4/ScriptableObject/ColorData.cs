using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.LineConnect
{
    [CreateAssetMenu(menuName = "LineConnect/ColorData")]
    public class ColorData : ScriptableObject
    {
        [SerializeField] Color[] colors;

        public Color GetColor(ColorType colorType)
        {
            return colors[(int)colorType];
        }
    }
}