using UnityEngine;

namespace Project.Scripts.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Health.Health Health{ get; private set; }
    }
}