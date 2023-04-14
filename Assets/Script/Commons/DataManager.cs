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
                //如果关卡数据为空，则通过文件level_data进行获取数据
                var textAsset = await AssetManager.LoadAssetAsync<TextAsset>("level_data");
                levelDatas = JsonConvert.DeserializeObject<LevelData[]>(textAsset.text);
            }
            for (int i = 0; i < levelDatas.Length; i++)
            {
                if (levelDatas[i].id == sceneName)
                {
                    //如果当前关卡名字和level_data中数据相匹配，则赋值到当前关卡数据
                    levelData = levelDatas[i];
                    return levelData;
                }
            }
            //如果levelDatas中找不到和当前关卡名字匹配的数据，则直接拿levelDatas中第一个作为当前关卡数据
            levelData = levelDatas[0];
            return levelData;
        }

        public void SavePlayerData()
        {
            //保存当前玩家的数据
            var json = JsonConvert.SerializeObject(playerData);
            PlayerPrefs.SetString(keyPlayerData, json);
            PlayerPrefs.Save();
        }

        public void LevelUP()
        {
            var exp = playerData.currentExp + levelData.exp;
            //如果exp大于当前玩家的最大exp
            if (exp >= playerData.maxExp)
            {
                var cur = exp - playerData.maxExp;
                //如果当前玩家等级超过了上限
                if (playerData.level > playerDatas.Length)
                {
                    //则无法超过最大等级和经验值
                    playerData = playerDatas[playerDatas.Length];
                    playerData.currentExp = playerDatas[playerDatas.Length].maxExp;
                }
                else
                {
                    //否则赋值为下一等级的数据
                    playerData = playerDatas[playerData.level];
                    playerData.currentExp = cur;
                }
            }
            else
            {
                playerData.currentExp = exp;
            }
            //保存玩家数据
            SavePlayerData();
        }
    }
}