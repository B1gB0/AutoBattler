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
        [SerializeField] private Transform _playerSpawnPoint;
        
        private IResourceService _resourceService;
        
        [Inject]
        public void Construct(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public async UniTask<Character> CreateCharacter(PlayerClassesData data)
        {
            var playerTemplate = await _resourceService.Load<GameObject>(data.Id);
            playerTemplate = Instantiate(playerTemplate, _playerSpawnPoint);
            Character character = playerTemplate.GetComponent<Character>();

            return character;
        }
    }
}