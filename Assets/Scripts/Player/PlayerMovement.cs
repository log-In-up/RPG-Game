using GameData;
using MVC;
using SkillTree;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private float _dashDistance, _jumpHeight;
        private Dictionary<SkillType, float> _dashes;

        private MVCApplication _mvcApplication;
        private FixedJoystick _fixedJoystick;
        private PlayerData _playerData;
        private Rigidbody _rigidbody;
        private Vector3 _currentVelocity, _moveAmount;
        #endregion

        #region Zenject
        [Inject]
        private void Constructor(FixedJoystick fixedJoystick, PlayerData playerData, MVCApplication mvcApplication)
        {
            _fixedJoystick = fixedJoystick;
            _playerData = playerData;
            _mvcApplication = mvcApplication;
        }
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _dashDistance = 0.0f;
            _jumpHeight = 0.0f;

            _dashes = new Dictionary<SkillType, float>();
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

        private bool SkillIsRelatedToMovement(SkillType skillType)
        {
            List<SkillType> skills = new List<SkillType> { SkillType.Dash, SkillType.AdvancedDash };

            return skills.Contains(skillType);
        }
        #endregion

        #region Event handlers
        private void OnUnlockSkill(SkillType skillType)
        {
            if (SkillIsRelatedToMovement(skillType))
            {
                DashSkill skillData = (DashSkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);

                _dashes.Add(skillType, skillData.DashDistance);
                _dashDistance = _dashes[skillType];
            }

            if(skillType.Equals(SkillType.Jump))
            {
                JumpSkill skillData = (JumpSkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);

                _jumpHeight = skillData.JumpPower;
            }
        }

        private void OnLockSkill(SkillType skillType)
        {
            if (SkillIsRelatedToMovement(skillType))
            {
                DashSkill skillData = (DashSkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);

                _dashes.Remove(skillType);
                _dashDistance = _dashes.Count > 0 ? _dashes.Values.Last() : skillData.DashDistance;
            }

            if (skillType.Equals(SkillType.Jump))
            {
                _jumpHeight = 0.0f;
            }
        }

        private void OnLockAllSkills()
        {
            _dashes.Clear();
            _dashDistance = 0.0f;
            _jumpHeight = 0.0f;
        }
        #endregion

        #region Public methods
        internal void Dash()
        {
            if (_dashDistance == 0.0f) return;

            Vector3 translation = _dashDistance * _model.forward;

            transform.Translate(translation);
        }

        internal void Jump()
        {
            if (_jumpHeight == 0.0f) return;

            _rigidbody.AddForce(Vector3.up * _jumpHeight);
        }

        internal void EnableConnectionWithSkilltree()
        {
            _mvcApplication.PerksModel.OnLockAllSkills += OnLockAllSkills;
            _mvcApplication.PerksModel.OnLockSkill += OnLockSkill;
            _mvcApplication.PerksModel.OnUnlockSkill += OnUnlockSkill;
        }

        internal void DisableConnectionWithSkilltree()
        {
            _mvcApplication.PerksModel.OnLockAllSkills -= OnLockAllSkills;
            _mvcApplication.PerksModel.OnLockSkill -= OnLockSkill;
            _mvcApplication.PerksModel.OnUnlockSkill -= OnUnlockSkill;
        }
        #endregion
    }
}
