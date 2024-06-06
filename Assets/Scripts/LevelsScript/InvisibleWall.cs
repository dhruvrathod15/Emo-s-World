using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InvisibleWall : MonoBehaviour
{
    public Tilemap TileMap;
    private void Start()
    {
        TileMap = GetComponent<Tilemap>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            /* Color newColor = new Color(138f / 255f, 138f / 255f, 138f / 255f, 61f / 255f);*/
            Color newColor = new Color(1f,1f,1f,145f/255f);
            TileMap.color = newColor;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Color oldColor = new Color(1f,1f,1f,255f);
            TileMap.color = oldColor;
        }
    }
}
