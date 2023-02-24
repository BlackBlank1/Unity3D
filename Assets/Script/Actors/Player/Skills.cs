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

        public event Action AddHp;

        private void Start()
        {
            addHp.onClick.AddListener(() =>
            {
                AddHp?.Invoke();
            });
        }
    }
}