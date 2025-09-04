using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Scripts.Characters.Enemy;
using Project.Scripts.DataBase.Data;
using Project.Scripts.Game.Factories;
using Reflex.Attributes;

namespace Project.Scripts.Services
{
    public class EnemyService : Service, IEnemyService
    {
        private const int MinIndex = 0;
        
        private Dictionary<string, EnemyData> _enemiesData = new ();
        private IDataBaseService _dataBaseService;

        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public Enemy CurrentEnemy { get; }

        public override UniTask Init()
        {
            foreach (var enemy in _dataBaseService.Content.Enemies)
            {
                _enemiesData.Add(enemy.Id, enemy);
            }
            
            return UniTask.CompletedTask;
        }

        public void GetEnemyFactory(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public async UniTask CreateEnemy()
        {
            await _enemyFactory.CreateEnemy(GetRandomEnemyData().Id);
        }

        private EnemyData GetRandomEnemyData()
        {
            int randomIndex = UnityEngine.Random.Range(MinIndex, _enemiesData.Count);
            
            var keys = _enemiesData.Keys.ToList();

            foreach (var data in _enemiesData)
            {
                _enemiesData.Remove(data.Key);
                return _enemiesData[keys[randomIndex]];
            }

            return null;
        }
    }
}