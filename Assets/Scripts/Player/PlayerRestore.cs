using GameData;
using MVC;
using SkillTree;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Player
{
    [DisallowMultipleComponent]
    public sealed class PlayerRestore : MonoBehaviour
    {
        #region Fields
        private MVCApplication _mvcApplication;

        private float _restoreEffectiveness, _lifeRestore, _manaRestore;
        private List<LifeRestoreSkill> _skillList;
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
            _restoreEffectiveness = 1.0f;
            _lifeRestore = _manaRestore = 0.0f;

            _skillList = new List<LifeRestoreSkill>();
        }
        #endregion

        #region Event handlers
        private void OnUnlockSkill(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.LifeRestore:
                    LifeRestoreSkill lifeRestore = (LifeRestoreSkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);
                    _skillList.Add(lifeRestore);
                    _lifeRestore = lifeRestore.LifeRestore;
                    break;
                case SkillType.AdvancedLifeRestore:
                    LifeRestoreSkill advLifeRestore = (LifeRestoreSkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);
                    _skillList.Add(advLifeRestore);
                    _lifeRestore = advLifeRestore.LifeRestore;
                    break;
                case SkillType.ManaRestore:
                    ManaRestoreSkill manaRestore = (ManaRestoreSkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);
                    _manaRestore = manaRestore.ManaRestore;
                    break;
                case SkillType.RestoreEffectiveness:
                    RestoreEffectivenessSkill restoreEffectiveness = (RestoreEffectivenessSkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);
                    _restoreEffectiveness = restoreEffectiveness.LifeRestoreEffectiveness;
                    break;
                default:
                    break;
            }
        }

        private void OnLockSkill(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.LifeRestore:
                    LifeRestoreSkill lifeRestore = (LifeRestoreSkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);
                    _skillList.Remove(lifeRestore);

                    if(_skillList.Count > 0)
                    {
                        LifeRestoreSkill skill = _skillList.Last();
                        _lifeRestore = skill.LifeRestore;
                    }
                    else
                    {
                        _lifeRestore = 0.0f;
                    }
                    break;
                case SkillType.AdvancedLifeRestore:
                    LifeRestoreSkill advLifeRestore = (LifeRestoreSkill)_mvcApplication.PerksModel.SkillList.GetSkill(skillType);
                    _skillList.Remove(advLifeRestore);

                    if (_skillList.Count > 0)
                    {
                        LifeRestoreSkill skill = _skillList.Last();
                        _lifeRestore = skill.LifeRestore;
                    }
                    else
                    {
                        _lifeRestore = 0.0f;
                    }
                    break;
                case SkillType.ManaRestore:
                    _manaRestore = 0.0f;
                    break;
                case SkillType.RestoreEffectiveness:
                    _restoreEffectiveness = 1.0f;
                    break;
                default:
                    break;
            }
        }

        private void OnLockAllSkills()
        {
            _restoreEffectiveness = 1.0f;
            _lifeRestore = _manaRestore = 0.0f;

            _skillList.Clear();
        }
        #endregion

        #region Public methods
        internal void DoRestore()
        {
            float health = _lifeRestore * _restoreEffectiveness;
            _mvcApplication.Controller.UpdateHealthBar(health);

            float mana = _manaRestore * _restoreEffectiveness;
            _mvcApplication.Controller.UpdateManaBar(mana);
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