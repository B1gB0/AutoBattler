using Cysharp.Threading.Tasks;
using Project.Scripts.Characters;
using Project.Scripts.DataBase.Data;
using Project.Scripts.Services;
using Reflex.Attributes;
using UnityEngine;

namespace Project.Scripts.Game.Factories
{
    public class PlayerFactory : MonoBehaviour
    {
        private const int Min = 1;
        private const int Max = 4;
        
        [SerializeField] private Transform _playerSpawnPoint;

        private IResourceService _resourceService;
        private IWeaponService _weaponService;

        private int _power;
        private int _endurance;
        private int _agility;
        
        [Inject]
        public void Construct(IResourceService resourceService, IWeaponService weaponService)
        {
            _resourceService = resourceService;
            _weaponService = weaponService;
        }

        public async UniTask<Character> CreateCharacter(PlayerClassesData data)
        {
            var playerTemplate = await _resourceService.Load<GameObject>(data.Id);
            playerTemplate = Instantiate(playerTemplate, _playerSpawnPoint);
            
            Character character = playerTemplate.GetComponent<Character>();
            await _weaponService.CreateWeapon(data.StartWeaponId);
            
            SetRandomCharacteristics();
            
            character.Construct(data.RewardedHealthForLevel, _weaponService.CurrentWeapon.Damage,
                _power, _endurance, _agility);
            
            Debug.Log("здоровье " + character.Health.TargetHealth);
            Debug.Log("урон " + character.Damage);
            Debug.Log("текущий тип урона " + _weaponService.CurrentWeapon.DamageType);
            Debug.Log("текущее оружие " + _weaponService.CurrentWeapon.Name);
            Debug.Log("сила " + character.Power);
            Debug.Log("вынсоливость " + character.Endurance);
            Debug.Log("ловкость " + character.Agility);
            
            character.gameObject.SetActive(false);

            return character;
        }

        private void SetRandomCharacteristics()
        {
            _power = Random.Range(Min, Max);
            _endurance = Random.Range(Min, Max);
            _agility = Random.Range(Min, Max);
        }
    }
}