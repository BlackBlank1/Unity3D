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
        }

        private void Update()
        {
            addHp.image.fillAmount += Time.deltaTime * (1 / addHpTime);
        }
    }
}