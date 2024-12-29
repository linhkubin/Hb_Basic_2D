using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Link.Paint
{
    public class Control : MonoBehaviour
    {
        private const int MAX_X = 8, MAX_Y = 12;

        [SerializeField] Tile tilePrefab;
        [SerializeField] Transform parent;
        [SerializeField] Vector2 space;
        Tile[,] tiles = new Tile[MAX_X, MAX_Y];

        // Start is called before the first frame update
        void Start()
        {
            Vector2 startPoint = -Vector2.right * space.x * (MAX_X - 1) * 0.5f - Vector2.up * space.y * (MAX_Y - 1) * 0.5f;

            for (int i = 0; i < MAX_X; i++) 
            {
                for(int j = 0; j < MAX_Y; j++)
                {
                    Vector2 point = Vector2.right * space.x * i + Vector2.up * space.y * j + startPoint;
                    tiles[i,j] = Instantiate(tilePrefab, point, Quaternion.identity, parent);
                }
            }


        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

            }
        }

    }
}