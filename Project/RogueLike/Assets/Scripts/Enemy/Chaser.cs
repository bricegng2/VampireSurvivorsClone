using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public enum EChaserState : byte
{
    Idle,
    Walking,
    Floating,
    Dead
}

public class Chaser : Enemy
{
    private EChaserState state = EChaserState.Idle;

    void Start()
    {
        setPlayer();
        animator = GetComponent<Animator>();
        state = EChaserState.Walking;

        tier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        Vector2 newPosition = player.position - position;
        newPosition.Normalize();

        Vector2 velocity = newPosition * speed;
        transform.position += (Vector3)(velocity * Time.deltaTime);

        if (isHit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer < 0)
            {
                animator.Play("Flying");
                isHit = false;
                hitTimer = 0.8f;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            // Check if there's a contact object, in the contacts array
            if (collision.contacts.Length > 0)
            {
                // Get the normal from the first contact object
                Vector2 normal = collision.contacts[0].normal;

                // Ensure the dry bones's state is walking
                if (state == EChaserState.Walking)
                {
                    if (player.getState != EPlayerState.Dead)
                    {
                        player.HandleDamage(0.2f);
                    }
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            handleDamage(Constants.projectileDamage);

            isHit = true;
            animator.Play("Hit");

            handleDeath();
        }

        if (collision.gameObject.CompareTag("Orbiter"))
        {
            handleDamage(Constants.orbiterDamage);

            isHit = true;
            animator.Play("Hit");

            handleDeath();
        }
    }

    private void handleDeath()
    {
        if (health <= 0)
        {
            state = EChaserState.Dead;

            spawnPickUp();

            Destroy(gameObject);
        }
    }
}