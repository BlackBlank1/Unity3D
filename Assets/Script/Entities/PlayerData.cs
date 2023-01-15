using System;

namespace TS.Entities
{
    [Serializable]
    public class PlayerData
    {
        public int level;
        public float maxHp;
        public float damage;
        public int maxExp;
        public int currentExp;
    }
}