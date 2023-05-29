using App.Scripts.Mics;
using Scripts.Gameplay.ScriptableObjects;
using UnityEngine;

namespace Scripts.Gameplay.GameplayObjects
{
    public class ShareDataSO : MiddlewareBehaviour
    {
        [SerializeField] private GameSettingSO _gameSettingSO;
        public GameSettingSO GameSettingSO => _gameSettingSO;
    }
}