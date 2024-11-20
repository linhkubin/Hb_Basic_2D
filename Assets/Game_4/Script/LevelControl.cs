using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Link.LineConnect
{
    public enum ColorType
    {
        None = 0,
        Red = 1,
        Blue = 2,
        Green = 3, 
        Yellow = 4,
        Orange = 5,
        Violet = 6,
        Brown = 7,
    }

    public class LevelControl : MonoBehaviour
    {
        [SerializeField] Transform tileRoot;
        [SerializeField] Tile tilePrefab;

        private Tile[,] tiles = new Tile[5, 8];  

        private void Start()
        {
            OnInitTile(5, 8);
            tiles[0, 0].OnInit(ColorType.Red);
            tiles[4, 7].OnInit(ColorType.Red);
        }

        public void OnInitTile(int x, int y)
        {
            Vector2 startPoint = new Vector2( -(x - 1)* 0.5f , -(y - 1) * 0.5f);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    tiles[i, j] = Instantiate(tilePrefab, startPoint + new Vector2(i, j), Quaternion.identity);
                    tiles[i, j].OnInit(ColorType.None);
                }
            }
        }
    }
}