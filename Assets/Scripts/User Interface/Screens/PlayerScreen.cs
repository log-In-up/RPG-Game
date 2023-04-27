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
        [SerializeField] private Button _lifeRestore = null;
        [SerializeField] private Button _throwKnife = null;
        [SerializeField] private Button _jump = null;
        [SerializeField] private Slider _healthBar = null;
        [SerializeField] private Slider _manaBar = null;
        #endregion

        #region Properties
        public override UIScreen Screen => UIScreen.Player;
        #endregion

        #region Overridden methods
        public override void Setup()
        {
            UICore.MVCApplication.GameModel.SetupBars(_healthBar, _manaBar);

            base.Setup();
        }

        public override void Activate()
        {
            _abilities.onClick.AddListener(OnClickAbilities);
            _bowShot.onClick.AddListener(OnClickBowShot);
            _dash.onClick.AddListener(OnClickDash);
            _lifeRestore.onClick.AddListener(OnClickRestore);
            _throwKnife.onClick.AddListener(OnClickThrowKnife);
            _jump.onClick.AddListener(OnClickJump);

            ShowAbilitiesButtons();

            base.Activate();
        }

        public override void Deactivate()
        {
            _abilities.onClick.RemoveListener(OnClickAbilities);
            _bowShot.onClick.RemoveListener(OnClickBowShot);
            _dash.onClick.RemoveListener(OnClickDash);
            _lifeRestore.onClick.RemoveListener(OnClickRestore);
            _throwKnife.onClick.RemoveListener(OnClickThrowKnife);
            _jump.onClick.RemoveListener(OnClickJump);

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

            canUseSkill = IsSkillUnlocked(SkillType.LifeRestore) | IsSkillUnlocked(SkillType.AdvancedLifeRestore)
                | IsSkillUnlocked(SkillType.ManaRestore);
            _lifeRestore.transform.gameObject.SetActive(canUseSkill);

            canUseSkill = IsSkillUnlocked(SkillType.ThrowKnife);
            _throwKnife.transform.gameObject.SetActive(canUseSkill);

            canUseSkill = IsSkillUnlocked(SkillType.Jump);
            _jump.transform.gameObject.SetActive(canUseSkill);
        }

        private bool IsSkillUnlocked(SkillType skillType) => UICore.MVCApplication.PerksModel.IsSkillUnlocked(skillType);
        #endregion

        #region Event handlers
        private void OnClickAbilities() => UICore.OpenScreen(UIScreen.Perks);

        private void OnClickDash() => UICore.MVCApplication.GameView.OnClickDash();

        private void OnClickThrowKnife() => UICore.MVCApplication.GameView.OnClickThrowKnife();

        private void OnClickBowShot() => UICore.MVCApplication.GameView.OnClickBowShot();

        private void OnClickJump() => UICore.MVCApplication.GameView.OnCLickJump();

        private void OnClickRestore() => UICore.MVCApplication.GameView.OnClickRestore();
        #endregion
    }
}