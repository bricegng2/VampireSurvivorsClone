using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    public Player player;

    private float health;

    [SerializeField] private TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        health = player.getHealth;

        healthText.text = "Health: " + ((int)health).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        health = player.getHealth;

        healthText.text = "Health: " + ((int)health).ToString();
    }
}
