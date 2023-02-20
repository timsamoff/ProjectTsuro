using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minY = -10f;
    [SerializeField] private float maxY = 10f;
    [SerializeField] private List<Collider2D> colliders;
    [SerializeField] private float waitTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Find all tiles with composite colliders in the scene
        var tiles = GameObject.FindGameObjectsWithTag("Map");
        colliders = new List<Collider2D>();
        foreach (var tile in tiles)
        {
            var compositeCollider = tile.GetComponentInChildren<CompositeCollider2D>();
            if (compositeCollider != null)
            {
                colliders.Add(compositeCollider);
            }
        }

        StartCoroutine(PlacePlayerWithDelay());
    }

    private IEnumerator PlacePlayerWithDelay()
    {
        yield return new WaitForSeconds(waitTime);

        while (colliders == null)
        {
            yield return new WaitForSeconds(waitTime);
        }

        // Check if the space is free
        bool spaceIsFree = false;
        Vector2 randomPosition = Vector2.zero;
        while (!spaceIsFree)
        {
            randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            spaceIsFree = true;
            foreach (var collider in colliders)
            {
                if (collider != null && collider.bounds.Contains(randomPosition))
                {
                    // Get the alpha value of the sprite at the random position
                    SpriteRenderer spriteRenderer = collider.gameObject.GetComponentInChildren<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        Texture2D texture = spriteRenderer.sprite.texture;
                        Vector2 pixelUV = spriteRenderer.transform.InverseTransformPoint(randomPosition);
                        pixelUV += new Vector2(0.5f, 0.5f);
                        Color color = texture.GetPixel((int)(pixelUV.x * texture.width), (int)(pixelUV.y * texture.height));
                        if (color.a != 0)
                        {
                            spaceIsFree = false;
                            break;
                        }
                    }
                }
            }

            // Check if the player object is within the bounds of the tiles
            if (spaceIsFree && playerObject != null)
            {
                Collider2D playerCollider = playerObject.GetComponent<Collider2D>();
                if (playerCollider != null && !playerCollider.bounds.Intersects(GetComponent<Collider>().bounds))
                {
                    spaceIsFree = false;
                }
            }
        }

        // Place the player object
        Instantiate(playerObject, randomPosition, Quaternion.identity);
    }
}