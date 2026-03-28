using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum EPlayerState : byte
{
    Idle,
    Walking,
    Dodging,
    Floating,
    Dead
}

public class Player : MonoBehaviour
{
    public PlayerHealthManager playerHealthManager;

    private EPlayerState state = EPlayerState.Idle;

    // component variables
    private new Rigidbody2D rigidbody;
    private Animator animator;

    // movement variables
    private Vector2 direction = Vector2.zero;
    private const float speed = 5.0f;
    public Vector2 position;

    private float health;

    // dodging variables
    private bool isDodging = false;
    private float dodgeTimer = Constants.dodgeTimer;

    // projectile variables
    public PlayerProjectile projectilePrefab;
    private PlayerProjectile projectile = null;
    private Vector2 projectileSpawnLocation = Vector2.zero;
    private float fireRate = Constants.shooterProjTimeLeft;

    // XP variables
    private int currentXP = 0;
    private int xpToNextLevel = 10;
    private int currentLevel = 1;

    public EPlayerState getState
    {
        get
        {
            return state;
        }
    }

    public float getHealth
    {
        get
        {
            return health;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        position = transform.position;

        health = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        if (direction == Vector2.zero && state != EPlayerState.Dead && state != EPlayerState.Dodging)
        {
            SetState(EPlayerState.Idle);
        }

        fireRate -= Time.deltaTime;
        if (fireRate < 0)
        {
            FireProjectile(true);
            fireRate = Constants.shooterProjTimeLeft;
        }

        DodgeManager(Time.deltaTime);

        Debug.Log(position);
    }

    public void FixedUpdate()
    {
        if (state != EPlayerState.Dead)
        {
            rigidbody.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
        }
    }

    public void SetState(EPlayerState setState)
    {
        if (state != setState)
        {
            state = setState;

            if (state == EPlayerState.Idle)
            {
                animator.Play("Idle");
            }
            else if (state == EPlayerState.Walking)
            {
                animator.Play("Walking");
            }
            else if (state == EPlayerState.Dodging)
            {
                animator.Play("Dodging");
                isDodging = true;
            }
        }
    }

    public void MoveUpDown(InputAction.CallbackContext context)
    {
        if (state == EPlayerState.Dead)
        {
            return;
        }
        direction.y = context.ReadValue<float>();

        if (state != EPlayerState.Dodging)
        {
            SetState(EPlayerState.Walking);
        }

    }

    public void MoveRightLeft(InputAction.CallbackContext context)
    {
        if (state == EPlayerState.Dead)
        {
            return;
        }
        direction.x = context.ReadValue<float>();

        if (state != EPlayerState.Dodging)
        {
            SetState(EPlayerState.Walking);
        }

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void DodgeInput(InputAction.CallbackContext context)
    {
        SetState(EPlayerState.Dodging);
    }

    public void DodgeManager(float deltaTime)
    {
        if (isDodging)
        {
            dodgeTimer -= deltaTime;

            if (dodgeTimer <= 0)
            {
                dodgeTimer = Constants.dodgeTimer;
                isDodging = false;
                SetState(EPlayerState.Walking);
            }
        }
    }

    public void HandleDamage(float damage)
    {
        if (isDodging == false)
        {
            health -= damage;

            //playerHealthManager.UpdateHealth(damage);

            if (health <= 0)
            {
                SetState(EPlayerState.Dead);
            }
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
        projectile.SetState(EProjectileState.Active);
    }

    public void ModHealth(float mult)
    {
        health *= mult;
        //playerHealthManager.Heal();
    }

    public void AddXP()
    {
        currentXP += 2;
        if (currentXP >= xpToNextLevel)
        {
            currentLevel += 1;
            currentXP = 0;
            xpToNextLevel = (int)(xpToNextLevel * 1.5f);
        }
    }
}
