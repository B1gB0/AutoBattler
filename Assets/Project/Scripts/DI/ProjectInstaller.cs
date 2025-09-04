using Project.Scripts.Services;
using UnityEngine;
using Reflex.Core;

namespace Project.Scripts.DI
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(typeof(ResourceService), typeof(IResourceService));
            builder.AddSingleton(typeof(DataBaseService), typeof(IDataBaseService));
            builder.AddSingleton(typeof(EnemyService), typeof(IEnemyService));
            builder.AddSingleton(typeof(BattleService), typeof(IBattleService));
        }
    }
}
