using Project.Scripts.Characters.Player;
using Project.Scripts.DataBase.Data;
using Project.Scripts.Game.Factories;
using Project.Scripts.Services;
using Project.Scripts.UI;
using Reflex.Attributes;
using UnityEngine;

namespace Project.Scripts.Game
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private Transform _playerAttackPosition;
        [SerializeField] private Transform _enemyPosition;
        [SerializeField] private Transform _enemyAttackPosition;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private UIRoot _uiRoot;
        
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private WeaponFactory _weaponFactory;
        [SerializeField] private PlayerFactory _playerFactory;
        [SerializeField] private UIFactory _uiFactory;

        private Player _player;

        private IDataBaseService _dataBaseService;
        private IEnemyService _enemyService;
        private IWeaponService _weaponService;
        private IBattleService _battleService;
        
        private ChoosingCharacterPanel _choosingCharacterPanel;
        
        [Inject]
        private void Construct(IDataBaseService dataBaseService, IEnemyService enemyService,
            IBattleService battleService, IWeaponService weaponService)
        {
            _dataBaseService = dataBaseService;
            _enemyService = enemyService;
            _battleService = battleService;
            _weaponService = weaponService;
        }

        private async void Start()
        {
            await _dataBaseService.Init();
            
            _battleService.GetAllParams(_uiRoot, _playerPosition, _playerAttackPosition,
                _enemyPosition, _enemyAttackPosition);

            _choosingCharacterPanel = 
                await _uiFactory.CreateChoosingCharacterPanel(_canvas.transform);

            await _weaponService.Init();
            await _enemyService.Init();
            
            _enemyService.GetEnemyFactory(_enemyFactory);
            _weaponService.GetWeaponFactory(_weaponFactory);

            await _enemyService.CreateEnemy();
            
            _choosingCharacterPanel.OnCharacterButtonClicked += _uiRoot.HideBackground;
            _choosingCharacterPanel.OnCharacterDataGetted += OnCreateCharacter;
            
        }

        private async void OnCreateCharacter(PlayerClassesData data)
        {
            _player = new Player(await _playerFactory.CreateCharacter(data));
        }

        private void OnDestroy()
        {
            _choosingCharacterPanel.OnCharacterButtonClicked -= _uiRoot.HideBackground;
            _choosingCharacterPanel.OnCharacterDataGetted -= OnCreateCharacter;
            _battleService.Dispose();
        }
    }
}
