using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // [HideInInspector]
    // public GameObject gameManager;

    // private GameManager collCheck;

    bool isCollided;

    // Start is called before the first frame update
    void Awake()
    {
        // collCheck = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollided)
        {
            ResetCollision();
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        print(isCollided);

        if (coll.gameObject.tag == "Map")
        {
            isCollided = true;
        }
    }

    public void ResetCollision()
    {
        isCollided = false;
    }
}
