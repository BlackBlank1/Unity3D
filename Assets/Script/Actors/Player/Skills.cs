using System;
using UnityEngine;
using UnityEngine.UI;

namespace TS.Actors.Player
{
    public class Skills : MonoBehaviour
    {
        [SerializeField]
        private Button addHp;

        [SerializeField]
        private Button damageButton;

        public float addHpTime;
        public float damageTime;

        public event Action AddHp;

        public event Action DamageFalling; 

        private void Start()
        {
            addHp.onClick.AddListener(() =>
            {
                if (addHp.image.fillAmount == 1)
                {
                    AddHp?.Invoke();
                    addHp.image.fillAmount = 0;
                }
            });
            
            damageButton.onClick.AddListener((() =>
            {
                if (damageButton.image.fillAmount == 1)
                {
                    DamageFalling?.Invoke();
                    damageButton.image.fillAmount = 0;
                }
            }));
        }

        private void Update()
        {
            addHp.image.fillAmount += Time.deltaTime * (1 / addHpTime);
            damageButton.image.fillAmount += Time.deltaTime * (1 / damageTime);
        }
    }
}