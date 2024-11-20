using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.Card
{
    public enum CardType 
    { 
        Card_1, 
        Card_2, 
        Card_3,
        Card_4,
        Card_5,
        Card_6,
        Card_7,
        Card_8,
        Card_9,
        Card_10,
        Card_11,
        Card_12,
        Card_13,
        Card_14,
        Card_15,
    }

    public class Card : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;

        public CardType CardType { get; private set; }

        public void OnInit(CardType cardType)
        {
            this.CardType = cardType;
            spriteRenderer.sprite = Control.Instance.cardData.GetSprite(CardType);
        }

        internal void Select()
        {
            throw new NotImplementedException();
        }

        private void Reset()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}