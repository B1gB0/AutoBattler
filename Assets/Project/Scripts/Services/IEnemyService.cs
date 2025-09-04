using Cysharp.Threading.Tasks;
using Project.Scripts.Characters.Enemy;
using Project.Scripts.Game.Factories;

namespace Project.Scripts.Services
{
    public interface IEnemyService
    {
        public Enemy CurrentEnemy { get; }
        public UniTask Init();
        public UniTask CreateEnemy();
        public void GetEnemyFactory(EnemyFactory enemyFactory);
    }
}