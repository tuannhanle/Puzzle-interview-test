using System;
using Cysharp.Threading.Tasks;
using Gameplay.UserInput;
using Mics;
using UnityEngine;

namespace Scripts.Gameplay.Action
{
    public class MoveToSideAction : IAction
    {
        public Transform MovingTransform { get; set; }
        public Boolean IsLocal { get; set; } = true;

        private InputHandler _inputHandler = new InputHandler();

        public float SpeedF { get; set; } = 0F;

        public Transform SensorL;

        public Transform SensorR;

        public void Execuse()
        {
            _inputHandler.callback += MoveToSide;
            _inputHandler.Enable();

        }

        public void Stop()
        {
            if (_inputHandler.callback == null)
                return;
            _inputHandler.callback -= MoveToSide;
            _inputHandler.Disable();

        }

        public void MoveToSide(float amount) => MoveToSideAsync(amount).Forget();

        private async UniTaskVoid MoveToSideAsync(float amount)
        {
            await UniTask.WaitForFixedUpdate();
            var direction = amount > 0 ? Vector3.right : Vector3.left;
            var translation = Math.Abs(amount) * direction * SpeedF * Time.deltaTime;
            MovingTransform.Translate(translation, IsLocal ? Space.Self : Space.World);
            var isReachLeft = MovingTransform.position.x < SensorL.position.x;
            if (isReachLeft)
                MovingTransform.position =  new Vector3( SensorL.position.x, MovingTransform.position.y, MovingTransform.position.z);
            var isReachRight = MovingTransform.position.x > SensorR.position.x ;
            if (isReachRight)
                MovingTransform.position =  new Vector3( SensorR.position.x, MovingTransform.position.y, MovingTransform.position.z);
        }
        

    }
}