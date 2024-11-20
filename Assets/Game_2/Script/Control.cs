using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.Card
{
    public class Control : MonoBehaviour
    {
        public static Control Instance { get; private set; }
        public CardSOData cardData;
        [SerializeField] Card[] cards;
        Card card_1;
        Card card_2;

        bool isCanSelect = true;

        private void Awake()
        {
            Instance = this;
            OnInit();
        }

        public void OnInit()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].OnInit((CardType)(i / 2));
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isCanSelect && Input.GetMouseButtonDown(0))
            {
                if (card_1 == null)
                {
                    card_1 = GetCard();
                    card_1.Select();
                }
                else if(card_1 != null)
                {
                    card_2 = GetCard();
                    if (card_1 != card_2)
                    {
                        isCanSelect = false;

                        card_2.Select();
                        if(card_1.CardType == card_2.CardType)
                        {

                        }
                        else
                        {
                            card_1 = null;
                            card_2 = null;
                        }
                    }
                    else
                    {
                        card_2 = null;
                    }

                }
            }
        }

        private Card GetCard()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                return hit.collider.GetComponent<Card>();
            }
            return null;
        }

        private IEnumerator IESelect(Card card_1, Card card_2)
        {

            yield return new WaitForSeconds(1f);
        }
    }
}
