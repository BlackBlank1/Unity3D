using System;
using System.Threading.Tasks;
using TS.Actors.Enemies;
using TS.Entities;
using TS.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TS.Commons
{
    public class Director : MonoBehaviour
    {
        private float count;

        public int levelExp;

        private bool win = false;

        public event Action OnGameWin;
        private void Awake()
        {
            var enemyGenerators = FindObjectsOfType<EnemyGenerator>();
            count = enemyGenerators.Length;
            foreach (var i in enemyGenerators)
            {
                i.OnFinished += OnFinished;
            }

        }

        private async void Start()
        {
            var scene = SceneManager.GetActiveScene();
            var sceneName = scene.name;
            LevelData levelData;
            levelData = await DataManager.Instance.ReadLevelData(sceneName);
            levelExp = levelData.exp;
            var clock = FindObjectOfType<Clock>();
            clock.TimeIsArrive += TimeIsArrive;
        }
        
        private void TimeIsArrive()
        {
            count = 1;
            OnFinished();
        }

        private void OnFinished()
        {
            count -= 1;
            if (count == 0)
            {
                DataManager.Instance.LevelUP();
                OnGameWin?.Invoke();
            }
        }
        
    }
}