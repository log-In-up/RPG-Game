using GameData;
using UnityEngine;
using Zenject;

namespace Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        #region Editor fields
        [SerializeField] private Transform _model = null;
        #endregion

        #region Fields
        private FixedJoystick _fixedJoystick;
        private PlayerData _playerData;
        private Rigidbody _rigidbody;
        private Vector3 _currentVelocity, _moveAmount;
        #endregion

        #region Zenject
        [Inject]
        private void Constructor(FixedJoystick fixedJoystick, PlayerData playerData)
        {
            _fixedJoystick = fixedJoystick;
            _playerData = playerData;
        }
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Move();
            Look();
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + (transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime));
        }
        #endregion

        #region Methods
        private void Move()
        {
            Vector3 moveDirection = new Vector3(_fixedJoystick.Horizontal, 0.0f, _fixedJoystick.Vertical).normalized;

            _moveAmount = Vector3.SmoothDamp(_moveAmount, moveDirection * _playerData.MovementSpeed, ref _currentVelocity, _playerData.SmoothTime);
        }

        private void Look()
        {
            if (_fixedJoystick.Direction == Vector2.zero) return;

            float angle = Mathf.Atan2(_fixedJoystick.Horizontal, _fixedJoystick.Vertical) * Mathf.Rad2Deg;

            _model.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
        #endregion
    }
}
