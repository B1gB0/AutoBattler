using Cysharp.Threading.Tasks;
using Project.Scripts.Characters.Enemy;
using Project.Scripts.Characters.Player;
using Project.Scripts.DataBase.Data;
using Project.Scripts.Game.Factories;
using Project.Scripts.Services;
using Project.Scripts.UI;
using Project.Scripts.UI.View;
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
        [SerializeField] private Transform _positionPlayerBar;
        [SerializeField] private Transform _positionEnemyBar;
        [SerializeField] private Transform _healthBarsTrasforms;

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

        private HealthBar _playerHealthBar;
        private HealthBar _enemyHealthBar;
        
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

            _choosingCharacterPanel = 
                await _uiFactory.CreateChoosingCharacterPanel(_canvas.transform);

            await _enemyService.Init();
            await _weaponService.Init();
            
            _enemyService.GetEnemyFactory(_enemyFactory);
            _weaponService.GetWeaponFactory(_weaponFactory);

            _choosingCharacterPanel.OnCharacterButtonClicked += _uiRoot.HideBackground;
            _choosingCharacterPanel.OnCharacterDataGetted += OnCreateCharacter;
            _battleService.OnVictoryPlayer += OnCreateEnemy;
            _battleService.OnBattleCompleted += ShowPlayerBar;

            await CreateEnemy();
        }

        private void ShowPlayerBar()
        {
            _playerHealthBar.Show();
        }

        private void OnSpawnCharacters()
        {
            _player.Character.gameObject.SetActive(true);
            _player.Character.SetStartHealth();
            _playerHealthBar.Show();
            
            _battleService.GetAllParams(_uiRoot, _playerPosition, _playerAttackPosition,
                _enemyPosition, _enemyAttackPosition, _player);
        }

        private async UniTask CreateEnemy()
        {
            Enemy enemy = await _enemyService.CreateEnemy();
            _enemyHealthBar = await _uiFactory.CreateHealthBar(_healthBarsTrasforms, _positionEnemyBar, enemy.Health);
            _enemyService.CurrentEnemy.gameObject.SetActive(true);
            _enemyService.CurrentEnemy.SetStartHealth();
            _enemyHealthBar.Show();
        }

        private void OnCreateEnemy()
        {
            CreateEnemy().Forget();
        }

        private async void OnCreateCharacter(PlayerClassesData data)
        {
            _player = new Player(await _playerFactory.CreateCharacter(data));
            _playerHealthBar = await _uiFactory.CreateHealthBar(_healthBarsTrasforms, _positionPlayerBar, _player.Character.Health);
            OnSpawnCharacters();
        }

        private void OnDestroy()
        {
            _choosingCharacterPanel.OnCharacterButtonClicked -= _uiRoot.HideBackground;
            _choosingCharacterPanel.OnCharacterDataGetted -= OnCreateCharacter;
            _battleService.Dispose();
        }
    }
}
