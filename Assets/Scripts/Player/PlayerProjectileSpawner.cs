using GameData;
using MVC;
using Projectiles;
using SkillTree;
using UnityEngine;
using Zenject;

namespace Player
{
    [DisallowMultipleComponent]
    public sealed class PlayerProjectileSpawner : MonoBehaviour
    {
        #region Editor fields
        [SerializeField] private Transform _spawnPoint = null;
        [SerializeField] private Transform _model = null;
        #endregion

        #region Fields
        private float _projectileSpeedMultiplier;

        private MVCApplication _mvcApplication;
        private ArcherySkill _archerySkill;
        private KnifeThrowingSkill _knifeThrowingSkill;
        #endregion

        #region Zenject
        [Inject]
        private void Constructor(MVCApplication mvcApplication)
        {
            _mvcApplication = mvcApplication;
        }
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _projectileSpeedMultiplier = 1.0f;
        }
        #endregion

        #region Public methods
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

        internal void ThrowKnife()
        {
            if (_knifeThrowingSkill == null) return;

            SpawnProjectile(_knifeThrowingSkill.Knife);
        }

        internal void MakeBowShot()
        {
            if (_archerySkill == null) return;

            SpawnProjectile(_archerySkill.Arrow);
        }
        #endregion

        #region Event handlers
        private void OnUnlockSkill(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.Bow:
                    _archerySkill = (ArcherySkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);
                    break;
                case SkillType.ThrowKnife:
                    _knifeThrowingSkill = (KnifeThrowingSkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);
                    break;
                case SkillType.ProjectileSpeed:
                    ProjectileSpeedUpSkill skillData = (ProjectileSpeedUpSkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);
                    _projectileSpeedMultiplier = skillData.ProjectileSpeedMultiplier;
                    break;
                default:
                    break;
            }
        }

        private void OnLockSkill(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.Bow:
                    _archerySkill = null;
                    break;
                case SkillType.ThrowKnife:
                    _knifeThrowingSkill = null;
                    break;
                case SkillType.ProjectileSpeed:
                    _projectileSpeedMultiplier = 1.0f;
                    break;
                default:
                    break;
            }
        }

        private void OnLockAllSkills()
        {
            _archerySkill = null;
            _knifeThrowingSkill = null;

            _projectileSpeedMultiplier = 1.0f;
        }
        #endregion

        #region Methods
        private void SpawnProjectile(GameObject projectile)
        {
            GameObject newProjectile = Instantiate(projectile, _spawnPoint.position, _model.rotation);

            if (newProjectile.TryGetComponent(out Projectile projectileComponent))
            {
                projectileComponent.SetSpeedMultiplier(_projectileSpeedMultiplier);
            }
            else
            {
                Debug.LogError($"The Projectile component is missing on {projectile}");
            }
        }
        #endregion
    }
}