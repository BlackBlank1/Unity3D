using CartoonFX;
using TS.Commons;
using UnityEngine;

namespace TS.Battle
{

    public class DamageText : MonoBehaviour
    {
        [SerializeField]
        private float releaseDelay = 2f;

        [SerializeField]
        private float minSize = .8f;

        [SerializeField]
        private float maxSize = 1.3f;

        [SerializeField]
        private float damageScale = 0.001f;

        private ParticleSystem particle;
        private CFXR_ParticleText_Runtime textRuntime;

        private void Awake()
        {
            particle = GetComponent<ParticleSystem>();
            textRuntime = GetComponent<CFXR_ParticleText_Runtime>();
        }

        public void Show(float damage)
        {
            var sign = damage > 0 ? "+" : "";
            damage = Mathf.Abs(damage);
            textRuntime.size = Mathf.Lerp(minSize, maxSize, damage * damageScale);

            string text = sign + Mathf.RoundToInt(damage).ToString();
            textRuntime.GenerateText(text);

            particle.Play(true);
            StartCoroutine(Util.Delay(releaseDelay, () => Destroy(gameObject)));
        }
    }
}