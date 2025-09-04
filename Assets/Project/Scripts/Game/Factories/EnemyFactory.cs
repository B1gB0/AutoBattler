using Cysharp.Threading.Tasks;
using Project.Scripts.Characters.Enemy;
using Project.Scripts.Services;
using Reflex.Attributes;
using UnityEngine;

namespace Project.Scripts.Game.Factories
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private Transform _enemySpawnPoint;
        
        private IResourceService _resourceService;
        
        [Inject]
        public void Construct(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public async UniTask<Enemy> CreateEnemy(string enemyId)
        {
            var enemyTemplate = await _resourceService.Load<GameObject>(enemyId);
            enemyTemplate = Instantiate(enemyTemplate, _enemySpawnPoint);
            Enemy enemy = enemyTemplate.GetComponent<Enemy>();
            
            return enemy;
        }
    }
}