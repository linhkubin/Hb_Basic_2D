using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.HouseStack
{
    public class House : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rb;

        public void SetPoisition(Vector2 point)
        {
            transform.position = point;
        }

        public void OnInit()
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        internal void SetStatic(bool v)
        {
            gameObject.isStatic = v;
        }

        public void OnDrop()
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}