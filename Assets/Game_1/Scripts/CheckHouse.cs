using Link.HouseStack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.HouseStack
{
    public class CheckHouse : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            House house = collision.GetComponent<House>();
            if (house != null)
            {
                Control.Instance.CheckHouseBuilding(house);
            }
        }
    }
}