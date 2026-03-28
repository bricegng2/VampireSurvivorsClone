using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public Vector2 position = Vector2.zero;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
}
