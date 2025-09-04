using Project.Scripts.UI;
using Project.Scripts.UI.View;
using UnityEngine;

namespace Project.Scripts.Services
{
    public interface IFloatingTextService
    {
        void OnChangedFloatingText(string value, Transform target, Color targetColor);
        void Init(FloatingTextView textView);
    }
}