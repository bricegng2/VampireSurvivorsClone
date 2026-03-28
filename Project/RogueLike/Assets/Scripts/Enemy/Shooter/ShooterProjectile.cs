using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EShooterProjectileState : byte
{
    Unknown,
    Active,
    Inactive
}
public class ShooterProjectile : MonoBehaviour
{
    private Player player;

    private EShooterProjectileState state = EShooterProjectileState.Unknown;

    private float speed = 6.0f;
    private Vector2 playerPos;
    private float angle;

    private float timeLeft = 3.0f;

    private float rotationSpeed = 200.0f;

    public Quaternion getAngle
    {
        get
        {
            return transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = EShooterProjectileState.Active;

        player = Object.FindFirstObjectByType<Player>();

        Vector2 direction = player.position - (Vector2)transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EShooterProjectileState.Active)
        {
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 3.0f;
                Destroy(gameObject);
            }
        }
    }

    public void SetState(EShooterProjectileState newState)
    {
        if (state != newState)
        {
            state = newState;

            if (state == EShooterProjectileState.Active)
            {
                //Instantiate(this, new Vector3(player.position.x, player.position.y), Quaternion.Euler(new Vector3(0, 0, angle)));
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.getState != EPlayerState.Dead)
            {
                player.HandleDamage(1.0f);
            }

            Destroy(gameObject);
        }
    }
}
