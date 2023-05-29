using App.Scripts.Mics;
using Mics;
using Scripts.Gameplay.Action;
using Scripts.Gameplay.ScriptableObjects;
using Unity.BossRoom.Infrastructure;
using UnityEngine;
using VContainer;

namespace Scripts.Gameplay.GameplayObjects
{
    public class MyCamera : MiddlewareBehaviour
    {
        private IAction _moveForward;
        private IAction _moveToSide;
        [Inject] private ShareDataSO _shareDataSO;
        private GameSettingSO _gameSettingSO => _shareDataSO.GameSettingSO;
        [SerializeField] private Transform _sensorL;
        [SerializeField] private Transform _sensorR;
        #region Event

        DisposableGroup _subscriptions;

        [Inject]
        void InjectDependencies(ISubscriber<ShareData.PlayGame> playeGameEvent)
        {
            _subscriptions = new DisposableGroup();
            _subscriptions.Add(playeGameEvent.Subscribe(OnPlayGameEventRaise));
        }

        private void OnPlayGameEventRaise(ShareData.PlayGame passage)
        {
            _moveForward.Execuse();
            _moveToSide.Execuse();
        }
        
        private void Awake()
        {

            _moveForward = new MoveForwardAction()
            {
                Direction = Vector3.forward,
                SpeedF = _gameSettingSO.SpeedForward,
                MovingTransform = this.transform,
                IsBreak = false,
                IsLocal = false
            };
            _moveToSide = new MoveToSideAction()
            {
                MovingTransform = this.transform,
                IsLocal = false,
                SpeedF = _gameSettingSO.SpeedSide,
                SensorL = _sensorL,
                SensorR = _sensorR

            };
        }
        
        #endregion
    }
}