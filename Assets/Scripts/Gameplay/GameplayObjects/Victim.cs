using System;
using App.Scripts.Mics;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Diagnostics;
using VContainer;
using Mics;
using UnityEngine.Serialization;
using Utils = Mics.Utils;

namespace Gameplay.GameplayObjects
{
    public interface ICollidable
    {
        Transform Transform { get; }
        Renderer renderer { get; }
        void Collide(Transform followedTransform, int index = 1, bool isFollowStart = true);
    }
    public enum VictimType {Pizza, Donut}
    public class Victim : MiddlewareBehaviour, ICollidable
    {
        [SerializeField] private VictimType _victimType;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _followSpeed;

        public VictimType VictimType => _victimType;
        public Vector3 GetPos => this.Transform.position;


        private Vector3 pos => transform.position;
        public Transform Transform => this.transform;
        public Renderer renderer => _renderer ?? this.GetComponent<Renderer>();

        public void Collide(Transform followedTransform,int index = 1, bool isFollowStart = true)
        {
            _collider.enabled = false;
            CollideAsync(followedTransform,index, isFollowStart).Forget();
        }
        
        private async UniTaskVoid CollideAsync(Transform followedTransform, int index = 1, bool isFollowStart = true)
        {
            while (isFollowStart)
            {
                await UniTask.WaitForFixedUpdate();
                if (this == null)
                    return;
                var speed = (50f - index*5);
                speed =  speed <= _followSpeed ? _followSpeed : speed;
                var interpolation = speed  * Time.deltaTime;
                var posX = Mathf.Lerp(pos.x, followedTransform.position.x, interpolation);
                // var posZ = Mathf.Lerp(pos.z, followedTransform.position.z, float.NegativeInfinity);
                Transform.position = new Vector3(posX, pos.y , followedTransform.position.z);
            }
        }
    }

}