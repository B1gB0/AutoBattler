using Cysharp.Threading.Tasks;
using Project.Scripts.Services;
using Project.Scripts.UI;
using Reflex.Attributes;
using UnityEngine;

namespace Project.Scripts.Game.Factories
{
    public class UIFactory : MonoBehaviour
    {
        private const string ChoosingCharacterPanelPath = "ChoosingCharacterPanel";
        
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
    }
}