using System;
using Project.Scripts.Characters.Damage;
using UnityEngine;

namespace Project.Scripts.DataBase.Data
{
    [Serializable]
    public class PlayerClassesData
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private int _rewardedHealthForLevel;
        [SerializeField] private string _startWeaponId;

        public string Id => _id;
        public string Name => _name;
        public int RewardedHealthForLevel => _rewardedHealthForLevel;
        public string StartWeaponId => _startWeaponId;
    }
}