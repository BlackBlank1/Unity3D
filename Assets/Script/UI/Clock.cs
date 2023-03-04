using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TS.UI
{
    public class Clock : MonoBehaviour
    {
        [SerializeField]
        private Image clock;

        [SerializeField]
        private TMP_Text time;

        [SerializeField]
        private float surviveTime;

        private float currentTime;
        private bool count = false;

        public event Action TimeIsArrive;

        private void Start()
        {
            clock.fillAmount = 1;
            time.text = surviveTime.ToString("F2");
            currentTime = surviveTime;
        }

        private void Update()
        {
            clock.fillAmount -= Time.deltaTime * (1 / surviveTime);
            currentTime -= Time.deltaTime;
            if (currentTime >= 0)
            {
                // time.text = currentTime.ToString("F2");
                int seconds = (int)currentTime;
                //一分钟为60秒 秒数对3600取余再对60取整即为分钟
                int minute = seconds % 3600 / 60;
                //对3600取余再对60取余即为秒数
                seconds = seconds % 3600 % 60;
                //返回00:00:00时间格式
                if (seconds <= 10 && minute == 0)
                {
                    time.color = new Color(104, 0, 0 , 255);
                }
                time.text = $"{minute:D2}:{seconds:D2}";
            }
            else
            {
                if (!count)
                {
                    TimeIsArrive?.Invoke();
                    count = true;
                }
                time.text = "00:00";
            }
        }
    }
}