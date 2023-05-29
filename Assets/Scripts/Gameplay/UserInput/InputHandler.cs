using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.UserInput
{
    public enum Side { Left, Right}
    public class InputHandler : IDisable
    {
        private Boolean _isDisable { get; set; }
        public Action<float> callback { get; set; }

        private const string AXIST_NAME = "Mouse X";
        public void Disable()
        {
            _isDisable = true;
            callback = null;
        }

        public void Enable()
        {
            _isDisable = false;
            GetTouch(callback).Forget();
#if UNITY_EDITOR
            SimulateTouchByMouse(callback).Forget();
#endif
        }
        
        private async UniTaskVoid GetTouch(Action<float> callback)
        {
            float lastPosX = 0f;

            while (_isDisable == false)
            {
                await UniTask.Yield();
                if(Input.touchCount == 1)
                {
                    var touch = Input.GetTouch(0);

                    if(touch.phase == TouchPhase.Began )
                    {
                        lastPosX = touch.position.x;
                    } 
                    else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        // get the moved direction compared to the initial touch position
                        var direction = touch.position.x - lastPosX;
                        lastPosX = touch.position.x;
                        if (direction == 0)
                            continue;
                        callback?.Invoke(direction);
                    }
                }
            }
        }

        private async UniTaskVoid SimulateTouchByMouse(Action<float> callback)
        {
            bool isMouseDown = false;
            while (_isDisable == false)
            {
                await UniTask.Yield();
                if (Input.GetMouseButtonUp(0))
                {
                    isMouseDown = false;
                    continue;
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    isMouseDown = true;
                }
                
                if (isMouseDown)
                {
                    var currentAxisX = Input.GetAxis(AXIST_NAME);
                    if (currentAxisX == 0)
                        continue;
                    callback?.Invoke(currentAxisX);
                }
            }
        }
    }


    public interface IDisable
    {
        void Disable();
        void Enable();
    }
}
