using UnityEngine;

namespace Projectiles
{
    [DisallowMultipleComponent]
    public sealed class Projectile : MonoBehaviour
    {
        #region Editor fields
        [SerializeField, Min(0.0f)] private float _speed = 5.0f;
        [SerializeField, Min(0.0f)] private float _lifetime = 5.0f;
        #endregion

        #region Fields
        private float _speedMultiplier;
        private float _currentSpeed;
        #endregion

        #region MonoBehaviour API
        private void Start()
        {
            Destroy(gameObject, _lifetime);
            _currentSpeed = _speed;
            _speedMultiplier = 1.0f;
        }

        private void Update()
        {
            
            Vector3 translation = _currentSpeed * Time.deltaTime * Vector3.forward;

            transform.Translate(translation);
        }
        #endregion

        #region Public methods
        internal void SetSpeedMultiplier(float speedMultiplier)
        {
            _speedMultiplier = speedMultiplier;

            _currentSpeed = _speedMultiplier * _speed;
        }
        #endregion
    }
}