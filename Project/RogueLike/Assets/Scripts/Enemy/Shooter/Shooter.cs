using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EShooterState : byte
{
    Idle,
    Walking,
    Dead
}

public class Shooter : Enemy
{
    private EShooterState state = EShooterState.Idle;

    private Vector2 projectileSpawnLocation = Vector2.zero;
    private ShooterProjectile projectile = null;
    public ShooterProjectile projectilePrefab;
    private float fireRate = Constants.projTimeLeft;

    // Start is called before the first frame update
    void Start()
    {
        setPlayer();
        animator = GetComponent<Animator>();
        state = EShooterState.Walking;
        speed = 1.2f;

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

        if (player.position.x > position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (player.position.x < position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (isHit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer < 0)
            {
                animator.Play("Walking");
                isHit = false;
                hitTimer = 0.8f;
            }
        }

        fireRate -= Time.deltaTime;
        if (fireRate < 0)
        {
            FireProjectile(true);
            fireRate = Constants.projTimeLeft;
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
            state = EShooterState.Dead;

            spawnPickUp();

            Destroy(gameObject);
        }
    }

    public void FireProjectile(bool spawnProjectile)
    {
        if (spawnProjectile)
        {
            projectileSpawnLocation = new Vector2(transform.position.x, transform.position.y);
            SpawnFireBall(projectileSpawnLocation);
        }
    }

    public void SpawnFireBall(Vector2 location)
    {
        projectile = Instantiate(projectilePrefab, new Vector3(location.x, location.y), Quaternion.Euler(0, 0, projectilePrefab.getAngle.eulerAngles.z));
        projectile.SetState(EShooterProjectileState.Active);
    }
}
