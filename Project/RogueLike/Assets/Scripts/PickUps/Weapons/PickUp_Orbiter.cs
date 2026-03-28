using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Orbiter : PickUp
{
    public bool isActivated = false;

    private float radius = 2.5f;
    private float angle = 0.0f;
    private float speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        spawnNumber = 1;
        setPlayer();
    }

    void Update()
    {
        if (isActivated == true)
        {
            //transform.position = player.transform.position;
            angle += Time.deltaTime * speed;
            float x = player.transform.position.x + Mathf.Cos(angle) * radius;
            float y = player.transform.position.y + Mathf.Sin(angle) * radius;
            transform.position = new Vector2(x, y);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActivated = true;
        }
    }
}
