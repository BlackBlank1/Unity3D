using System;
using TS.Commons;
using UnityEngine;

namespace TS.Battle
{

    public class DamageNumberManager : Singleton<DamageNumberManager>
    {
        public DamageText damageTextPrefab;

        public void ShowNumber(float damage, Vector3 position)
        {
            var damageText = Instantiate(damageTextPrefab);
            damageText.transform.position = position;
            damageText.Show(damage);
        }
    }

}