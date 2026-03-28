using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyType : byte
{
    Unknown,
    Chaser
}

public class Enemy : MonoBehaviour
{
    protected Player player;

    // component variables
    protected new Rigidbody2D rigidbody;
    protected Animator animator;

    // movement variables
    protected Vector2 direction = Vector2.zero;
    protected float speed = 2.0f;

    protected Vector2 position;

    public float health;

    protected bool isHit = false;
    protected float hitTimer = 0.4f;

    public List<PickUp> pickUpList;

    // tier 1 is the lowest an enemy can be
    protected int tier = 1;

    public Vector2 setPosition
    {
        set
        {
            transform.position = value;
        }
    }

    public void setPlayer()
    {
        player = Object.FindFirstObjectByType<Player>();
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected void spawnPickUp()
    {
        //int random = Random.Range(2, pickUpList.Count);
        //var matchingPickUps = pickUpList.FindAll(p => p.spawnNumber == random);
        //
        //if (matchingPickUps.Count > 0)
        //{
        //    var pickUpToSpawn = matchingPickUps[Random.Range(0, matchingPickUps.Count)];
        //    Instantiate(pickUpToSpawn, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        //}

        int random = Random.Range(0, pickUpList.Count);
        Instantiate(pickUpList[random], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    protected void handleDamage(float damage)
    {
        health -= damage;
    }
}
