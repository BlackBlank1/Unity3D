using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Slider playerHpSlider;

    private void Awake()
    {
        var playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController.OnHpChanged += OnPlayerHpChanged;
    }

    private void OnPlayerHpChanged(float hp, float maxHp)
    {
        playerHpSlider.value = hp / maxHp;
    }
}