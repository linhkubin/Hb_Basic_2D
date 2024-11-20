using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Link.Card
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CardData", order = 1)]
    public class CardSOData : ScriptableObject
    {
        [SerializeField] Sprite[] sprites;
        public Sprite GetSprite(CardType cardType)
        {
            return sprites[(int)cardType];
        }
    }
}