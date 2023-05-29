using System;
using System.Collections.Generic;
using App.Scripts.Mics;
using Mics;
using Scripts.Gameplay.Action;
using Scripts.Gameplay.GameplayObjects;
using Scripts.Gameplay.ScriptableObjects;
using Unity.BossRoom.Infrastructure;
using UnityEngine;
using VContainer;

namespace Gameplay.GameplayObjects
{
    public class Attacker : MiddlewareBehaviour
    {
        private IAction _moveForward;
        private IAction _moveToSide;
        [Inject] private ShareDataSO _shareDataSO;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Transform _sensorL;
        [SerializeField] private Transform _sensorR;

        private GameSettingSO _gameSettingSO => _shareDataSO.GameSettingSO;
        private int _foodStackCountD = 0;
        List<ICollidable> _victimList = new List<ICollidable>();
        // private Vector3 _firstVictimPos;
        private Vector3 _currentVictimPos;
        //
        private int _victimListIndexCounter = 0;
        private const int FIRST = 1;
        private const float HEIGHT_ATTACKER = 1f;
        private const float HEIGHT_VICTIM = 1f;
        private const float CURRENT_HEIGHT_AND_HALF_RATIO = 1.5f;


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
        
        private void OnTriggerEnter(Collider other)
        {
            var victim = other.GetComponent<ICollidable>();
            if (victim == null)
                return;

            var otherPos = other.transform.position;
            _victimList.Add(victim);
            if (_victimList.Count == FIRST)
            {
                var boundActtackMax = _renderer.bounds.max;
                // _firstVictimPos =  new Vector3(boundVictimMax.x, boundVictimMax.y * CURRENT_HEIGHT_AND_HALF_RATIO, boundVictimMax.z) ;
                _currentVictimPos = new Vector3(otherPos.x, boundActtackMax.y * CURRENT_HEIGHT_AND_HALF_RATIO, otherPos.z);
                other.transform.position = _currentVictimPos;
                // _currentVictimPos = new Vector3(otherPos.x, transform.position.y + _renderer.bounds.max.y, otherPos.z);
                victim.Collide(transform);
            }
            else if (_victimList.Count > FIRST)
            {
                var boundVictimMax = _victimList[_victimListIndexCounter].renderer.bounds.max;
                _currentVictimPos = new Vector3(otherPos.x, boundVictimMax.y, otherPos.z);
                other.transform.position = _currentVictimPos;
                victim.Collide(_victimList[_victimListIndexCounter].Transform, _victimListIndexCounter + 1);
                _victimListIndexCounter++;
            }
        }

        #endregion
        
        

    }
}