using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    protected Player player;

    protected new Rigidbody2D rigidbody;

    public int spawnNumber;

    public void setPlayer()
    {
        player = Object.FindFirstObjectByType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
}
