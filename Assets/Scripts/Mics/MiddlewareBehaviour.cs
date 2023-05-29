using Unity.BossRoom.Infrastructure;
using UnityEngine;

namespace App.Scripts.Mics
{
    /// <summary>
    /// Wraps MonoBehaviour for MetanomyLabs standard functionality, and provides a control layer for
    /// moderating use of typical Unity messages like Awake, Update, and OnDestroy
    /// </summary>
    public abstract class MiddlewareBehaviour : MonoBehaviour
    {
        protected UniTaskTokenHelper _cancelToken = new UniTaskTokenHelper();
        protected DisposableGroup _subscriptions;

        protected void OnDestroy()
        {
            _cancelToken.ClearRunningTasks();
        }

        protected void OnDisable()
        {
            _cancelToken.ClearRunningTasks();
        }
    }
}
