using TS.Actors;
using TS.Actors.Player;
using UnityEngine;
using UnityEngine.UI;

namespace TS.UI
{

    public class InGameUI : MonoBehaviour
    {
        public Slider playerHpSlider;

        private void Awake()
        {
            var playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.OnHpChanged += OnPlayerHpChanged;
        }

        private void OnPlayerHpChanged(float hp, float maxHp, float delta)
        {
            playerHpSlider.value = hp / maxHp;
        }
    }

}