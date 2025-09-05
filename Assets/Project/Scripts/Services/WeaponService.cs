using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Scripts.DataBase.Data;
using Project.Scripts.Game.Factories;
using Reflex.Attributes;

namespace Project.Scripts.Services
{
    public class WeaponService : Service, IWeaponService
    {
        private WeaponFactory _weaponFactory;
        private IDataBaseService _dataBaseService;
        
        private Dictionary<string, WeaponData> _weaponsData = new ();
        
        public Weapon.Weapon CurrentWeapon { get; private set; }

        [Inject]
        private void Construct(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
        
        public override UniTask Init()
        {
            foreach (var weapon in _dataBaseService.Content.Weapons)
            {
                _weaponsData.Add(weapon.Id, weapon);
            }
            
            return UniTask.CompletedTask;
        }

        public UniTask CreateWeapon(string id)
        {
            CurrentWeapon = _weaponFactory.CreateWeapon(_weaponsData[id]);
            
            return UniTask.CompletedTask;
        }

        public void GetWeaponFactory(WeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
        }
    }
}