using Project.Scripts.DataBase.Data;
using UnityEngine;

namespace Project.Scripts.Game.Factories
{
    public class WeaponFactory : MonoBehaviour
    {
        public Weapon.Weapon _currentWeapon { get; private set; }

        public Weapon.Weapon CreateWeapon(WeaponData data)
        {
            _currentWeapon = new (data.Damage, data.DamageType, data.Name);
            return _currentWeapon;
        }
    }
}