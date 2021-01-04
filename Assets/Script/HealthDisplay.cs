using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI playerHealth;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.text = player.GetHealth().ToString();
    }
}
