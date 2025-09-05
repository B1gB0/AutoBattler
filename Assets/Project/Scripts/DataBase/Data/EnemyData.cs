using System;
using UnityEngine;

namespace Project.Scripts.DataBase.Data
{
    [Serializable]
    public class EnemyData
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private float _health;
        [SerializeField] private int _damage;
        [SerializeField] private int _power;
        [SerializeField] private int _endurance;
        [SerializeField] private int _agility;
        [SerializeField] private string _rewardedWeaponId;
        
        public string Id => _id;
        public string Name => _name;
        public float Health => _health;
        public int Damage => _damage;
        public int Power => _power;
        public int Endurance => _endurance;
        public int Agility => _agility;
        public string RewardedWeaponId => _rewardedWeaponId;
    }
}