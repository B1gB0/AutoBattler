using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Scripts.Characters.Enemy;
using Project.Scripts.DataBase.Data;
using Project.Scripts.Game.Factories;
using Reflex.Attributes;

namespace Project.Scripts.Services
{
    public class EnemyService : Service, IEnemyService
    {
        private readonly Dictionary<string, EnemyData> _allEnemiesData = new();
        private readonly List<EnemyData> _availableEnemies = new();
        
        private IDataBaseService _dataBaseService;
        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public event Action<Enemy> OnEnemyCreated;
        public Enemy CurrentEnemy { get; private set; }

        public override UniTask Init()
        {
            foreach (var enemy in _dataBaseService.Content.Enemies)
            {
                _allEnemiesData.Add(enemy.Id, enemy);
                _availableEnemies.Add(enemy);
            }

            return UniTask.CompletedTask;
        }

        public void GetEnemyFactory(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public async UniTask<Enemy> CreateEnemy()
        {
            CurrentEnemy = await _enemyFactory.CreateEnemy(GetRandomEnemyDataWithoutRepetition());
            OnEnemyCreated?.Invoke(CurrentEnemy);
            return CurrentEnemy;
        }

        private EnemyData GetRandomEnemyDataWithoutRepetition()
        {
            if (_availableEnemies.Count == 0)
            {
                ResetAvailableEnemies();
            }
            
            int randomIndex = UnityEngine.Random.Range(0, _availableEnemies.Count);
            EnemyData selectedEnemy = _availableEnemies[randomIndex];
            
            _availableEnemies.RemoveAt(randomIndex);
            
            return selectedEnemy;
        }

        private void ResetAvailableEnemies()
        {
            _availableEnemies.Clear();
            foreach (var enemy in _allEnemiesData.Values)
            {
                _availableEnemies.Add(enemy);
            }
        }
    }
}