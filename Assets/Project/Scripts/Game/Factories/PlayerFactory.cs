using Project.Scripts.Services;
using Reflex.Attributes;
using UnityEngine;

namespace Project.Scripts.Game.Factories
{
    public class PlayerFactory : MonoBehaviour
    {
        private IResourceService _resourceService;
        
        [Inject]
        public void Construct(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }
    }
}