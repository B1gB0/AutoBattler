using Cysharp.Threading.Tasks;
using Project.Scripts.Characters.Health;
using Project.Scripts.Services;
using Project.Scripts.UI;
using Project.Scripts.UI.View;
using Reflex.Attributes;
using UnityEngine;

namespace Project.Scripts.Game.Factories
{
    public class UIFactory : MonoBehaviour
    {
        private const string ChoosingCharacterPanelPath = "ChoosingCharacterPanel";
        private const string HealthBarPath = "HealthBar";

        private IResourceService _resourceService;
        private IDataBaseService _dataBaseService;
        
        [Inject]
        public void Construct(IResourceService resourceService, IDataBaseService dataBaseService)
        {
            _resourceService = resourceService;
            _dataBaseService = dataBaseService;
        }

        public async UniTask<ChoosingCharacterPanel> CreateChoosingCharacterPanel(Transform canvasTransform)
        {
            var choosingPanelTemplate = await _resourceService.Load<GameObject>(ChoosingCharacterPanelPath);
            choosingPanelTemplate = Instantiate(choosingPanelTemplate, canvasTransform);

            ChoosingCharacterPanel choosingCharacterPanel =
                choosingPanelTemplate.GetComponent<ChoosingCharacterPanel>();
            choosingCharacterPanel.Construct(_dataBaseService.Content.PlayerClasses);

            return choosingCharacterPanel;
        }

        public async UniTask<HealthBar> CreateHealthBar(Transform canvasTransform, Transform position, Health health)
        {
            var healthBarTemplate = await _resourceService.Load<GameObject>(HealthBarPath);
            healthBarTemplate = Instantiate(healthBarTemplate, canvasTransform);

            HealthBar healthBar = healthBarTemplate.GetComponent<HealthBar>();
            healthBar.Construct(health);
            healthBar.transform.position = position.position;

            return healthBar;
        }
    }
}