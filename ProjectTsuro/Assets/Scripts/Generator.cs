using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour
{
    [Tooltip("How many rows in the grid?")]
    [SerializeField] int rows = 5;
    [Tooltip("How many columns in the grid?")]
    [SerializeField] int cols = 8;

    [Tooltip("Increase this number above 1 for gaps between tiles.")]
    [SerializeField] float tileSize = 1;

    [Tooltip("How much would you like the tiles to rotate?")]
    [SerializeField] float rotationAngle = 90;

    [Tooltip("Populate with all the prefabs used to generate map.")]
    [SerializeField] GameObject[] prefabTiles;

    // A list of all map tiles
    List<GameObject> mapList = new List<GameObject>();

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("A01"));

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int n = Random.Range(0, prefabTiles.Length);
                GameObject thePrefab = prefabTiles[n];

                GameObject tile = Instantiate(thePrefab, transform);
                tile.name = "Tile_" + col + "_" + row;

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.transform.position = new Vector2(posX, posY);

                Quaternion rotation = transform.rotation;
                rotation.z = Random.Range(0, rotationAngle);
                float finalRot = Mathf.Round(rotation.z / 90.0f) * 90.0f;

                tile.transform.eulerAngles = new Vector3(0, 0, finalRot);
            }
        }

        Destroy(referenceTile);

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;

        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
