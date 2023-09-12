using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Health And Mana")]
    public HealthController PlayerHealth;
    public Image CurrentLife;
    public PlayerController playerController;
    public Image CurrentMana;

    private void Update()
    {
        CurrentLife.fillAmount = PlayerHealth._currentLife /PlayerHealth.MaxLife;
    }
}
