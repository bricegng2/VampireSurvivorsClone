using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_HealthBoost : PickUp
{
    void Start()
    {
        spawnNumber = 0;
        setPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.ModHealth(1.1f);
            Destroy(gameObject);
        }
    }
}
