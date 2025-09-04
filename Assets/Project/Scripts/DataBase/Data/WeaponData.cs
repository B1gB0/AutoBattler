using System;
using Project.Scripts.Characters.Damage;
using UnityEngine;

namespace Project.Scripts.DataBase.Data
{
    [Serializable]
    public class WeaponData
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private int _damage;
        [SerializeField] private DamageType _damageType;

        public string Id => _id;
        public string Name => _name;
        public int Damage => _damage;
        public DamageType DamageType => _damageType;
    }
}