using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Link.HouseStack
{
    public class StaticHouse : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            House house = collision.GetComponent<House>();
            if (house != null)
            {
                house.SetStatic(true);
            }
        }
    }
}