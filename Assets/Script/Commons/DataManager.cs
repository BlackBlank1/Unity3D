using System;
using Newtonsoft.Json;
using UnityEngine;

namespace TS.Commons
{
    public class DataManager : Singleton<DataManager>
    {
        private static string keyPlayerData = "keyPlayerData";
        private static string keyLevelData = "keyLevelData";

        public PlayerData playerData;
        public LevelData levelData;
        
        public PlayerData ReadPlayerData()
        {
            var playerDataJson = PlayerPrefs.GetString(keyPlayerData);
            if (string.IsNullOrEmpty(playerDataJson))
            {
                //playerDataJson为空的话，则从玩家数据配置表读取等级为1时候的数据
                //TODO

                playerData = new PlayerData
                {
                    level = 1,
                    hp = 100,
                    maxHp = 100,
                    damage = 10,
                    maxExp = 100,
                    currentExp = 0
                };
            }
            else
            {
                playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataJson);
            }

            return playerData;
        }

        public void ReadLevelData()
        {
            var levelDataJson = PlayerPrefs.GetString(keyLevelData);
            if (string.IsNullOrEmpty(levelDataJson))
            {
                //playerDataJson为空的话，则从玩家数据配置表读取等级为1时候的数据
                //TODO

                levelData = new LevelData
                {
                    exp = 25
                };
            }
            else
            {
                levelData = JsonConvert.DeserializeObject<LevelData>(levelDataJson);
            }
        }

        public void SavePlayerData()
        {
            var json = JsonConvert.SerializeObject(playerData);
            PlayerPrefs.SetString(keyPlayerData, json);
            PlayerPrefs.Save();
        }

        public void LevelUP()
        {
            var exp = playerData.currentExp + levelData.exp;
            if (exp >= playerData.maxExp)
            {
                //TODO
                
                playerData.level += 1;
                playerData.currentExp = exp - playerData.maxExp;
            }
            else
            {
                playerData.currentExp = exp;
                
            }

            SavePlayerData();
        }
    }

    [Serializable]
    public class PlayerData
    {
        public int level;
        public float hp;
        public float maxHp;
        public float damage;
        public int maxExp;
        public int currentExp;
    }

    [Serializable]
    public class LevelData
    {
        public int exp;
        public int waves;
    }
}