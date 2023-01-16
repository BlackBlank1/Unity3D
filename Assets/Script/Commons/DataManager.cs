using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TS.Entities;
using UnityEngine;

namespace TS.Commons
{
    public class DataManager : Singleton<DataManager>
    {
        private static string keyPlayerData = "keyPlayerData";
        private static string keyLevelData = "keyLevelData";

        public PlayerData playerData { get; private set; }
        public PlayerData[] playerDatas { get; private set; }
        public LevelData levelData { get; private set; }
        
        public LevelData[] levelDatas { get; private set; }


        public async Task<PlayerData> ReadPlayerData()
        {
            // 加载玩家所有等级数据
            if (playerDatas == null)
            {
                var textAsset = await AssetManager.LoadAssetAsync<TextAsset>("player_data");
                playerDatas = JsonConvert.DeserializeObject<PlayerData[]>(textAsset.text);
            }

            var playerDataJson = PlayerPrefs.GetString(keyPlayerData);
            if (string.IsNullOrEmpty(playerDataJson))
            {
                // playerDataJson为空的话，则从玩家数据配置表读取等级为1时候的数据

                playerData = playerDatas[0];
            }
            else
            {
                playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataJson);
            }

            return playerData;
        }

        public async Task<LevelData> ReadLevelData(String sceneName)
        {
            if (levelDatas == null)
            {
                var textAsset = await AssetManager.LoadAssetAsync<TextAsset>("level_data");
                levelDatas = JsonConvert.DeserializeObject<LevelData[]>(textAsset.text);
            }
            for (int i = 0; i < levelDatas.Length; i++)
            {
                if (levelDatas[i].id == sceneName)
                {
                    levelData = levelDatas[i];
                    return levelData;
                }
            }

            levelData = levelDatas[0];
            return levelData;
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
                playerData.currentExp = exp - playerData.maxExp;
                playerData = playerDatas[playerData.level];
            }
            else
            {
                playerData.currentExp = exp;
            }

            SavePlayerData();
        }
    }
}