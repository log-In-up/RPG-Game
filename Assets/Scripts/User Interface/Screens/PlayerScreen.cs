using System;
using System.Collections;
using SkillTree;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public sealed class PlayerScreen : ScreenObserver
    {
        #region Editor fields
        [SerializeField] private Button _abilities = null;
        [SerializeField] private Button _bowShot = null;
        [SerializeField] private Button _dash = null;
        [SerializeField] private Button _lifeSteal = null;
        [SerializeField] private Button _throwKnife = null;
        #endregion

        #region Properties
        public override UIScreen Screen => UIScreen.Player;
        #endregion

        #region Overridden methods
        public override void Activate()
        {
            _abilities.onClick.AddListener(OnClickAbilities);
            _bowShot.onClick.AddListener(OnClickBowShot);
            _dash.onClick.AddListener(OnClickDash);
            _lifeSteal.onClick.AddListener(OnClickLifeSteal);
            _throwKnife.onClick.AddListener(OnClickThrowKnife);

            ShowAbilitiesButtons();

            base.Activate();
        }

        public override void Deactivate()
        {
            _abilities.onClick.RemoveListener(OnClickAbilities);
            _bowShot.onClick.RemoveListener(OnClickBowShot);
            _dash.onClick.RemoveListener(OnClickDash);
            _lifeSteal.onClick.RemoveListener(OnClickLifeSteal);
            _throwKnife.onClick.RemoveListener(OnClickThrowKnife);

            base.Deactivate();
        }
        #endregion

        #region Methods
        private void ShowAbilitiesButtons()
        {
            bool canUseSkill = IsSkillUnlocked(SkillType.Bow);
            _bowShot.transform.gameObject.SetActive(canUseSkill);

            canUseSkill = IsSkillUnlocked(SkillType.Dash) | IsSkillUnlocked(SkillType.AdvancedDash);
            _dash.transform.gameObject.SetActive(canUseSkill);

            canUseSkill = IsSkillUnlocked(SkillType.LifeSteal) | IsSkillUnlocked(SkillType.AdvancedLifeSteal)
                | IsSkillUnlocked(SkillType.ManaSteal);
            _lifeSteal.transform.gameObject.SetActive(canUseSkill);

            canUseSkill = IsSkillUnlocked(SkillType.ThrowKnife);
            _throwKnife.transform.gameObject.SetActive(canUseSkill);
        }

        private bool IsSkillUnlocked(SkillType skillType) => UICore.MVCApplication.PerksModel.IsSkillUnlocked(skillType);
        #endregion

        #region Event handlers
        private void OnClickAbilities() => UICore.OpenScreen(UIScreen.Perks);

        private void OnClickDash()
        {

        }

        private void OnClickThrowKnife()
        {

        }

        private void OnClickBowShot()
        {

        }

        private void OnClickLifeSteal()
        {

        }
        #endregion
    }
}