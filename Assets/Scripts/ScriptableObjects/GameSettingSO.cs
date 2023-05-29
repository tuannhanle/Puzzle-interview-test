using UnityEngine;

namespace Scripts.Gameplay.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GamePlaySetting", menuName = "Setting", order = 0)]
    public class GameSettingSO : ScriptableObject
    {
        [Header("Attacker")] 
        [SerializeField] private float speedForward;
        public float SpeedForward => speedForward;
            
        [SerializeField] private float speedSide;
        public float SpeedSide => speedSide;
    }
}