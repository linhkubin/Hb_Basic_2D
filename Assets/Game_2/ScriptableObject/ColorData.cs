using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ColorType
{
    None = 0,
    Red = 1,
    Blue = 2,
    Green = 3,
    Orange = 4,
}

[CreateAssetMenu(menuName = "ColorData")]
public class ColorData : ScriptableObject
{
    //theo tha material theo dung thu tu ColorType
    [SerializeField] Color[] colors;

    //lay material theo mau tuong ung
    public Color GetMat(ColorType colorType)
    {
        return colors[(int)colorType];
    }
}