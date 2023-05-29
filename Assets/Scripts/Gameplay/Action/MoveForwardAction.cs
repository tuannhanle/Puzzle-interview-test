using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts.Gameplay.Action
{
    #nullable enable
    public class MoveForwardAction : IAction
    {
        public Transform MovingTransform { get; set; }
        public Vector3 Direction { get; set; } = Vector3.up;
        public float SpeedF { get; set; } = 0F;
        public Boolean IsBreak { get; set; } = false;

        public Boolean IsLocal { get; set; } = true;
        public void Execuse()
        {
            MoveForward().Forget();
        }

        public void Stop()
        {
            IsBreak = false;
        }

        private async UniTaskVoid MoveForward()
        {
            await UniTask.WaitForFixedUpdate();
            if (IsBreak)
                return;
            // Check if the object has been destroyed
            if (this == null)
                return;
            //cancel token
            var translation = Direction * SpeedF * Time.deltaTime;
            MovingTransform.Translate(translation, IsLocal ? Space.Self : Space.World);
            MoveForward().Forget();
        }

    }
}