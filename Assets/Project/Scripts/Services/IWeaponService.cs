using Cysharp.Threading.Tasks;
using Project.Scripts.Game.Factories;

namespace Project.Scripts.Services
{
    public interface IWeaponService
    {
        public Weapon.Weapon CurrentWeapon { get; }
        public UniTask Init();
        public UniTask CreateWeapon(string id);
        public void GetWeaponFactory(WeaponFactory weaponFactory);
    }
}