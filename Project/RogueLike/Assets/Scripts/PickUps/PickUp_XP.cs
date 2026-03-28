using UnityEngine;

public class PickUp_XP : PickUp
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnNumber = 2;
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
            player.AddXP();
            Destroy(gameObject);
        }
    }
}
