namespace MVC
{
    public sealed class GameController
    {
        #region Fields
        private GameModel _model;
        #endregion

        public GameController(GameModel model)
        {
            _model = model;
        }

        internal void ActivatePlayerBowShot() => _model.PlayerProjectileSpawner.MakeBowShot();

        internal void ActivatePlayerDash() => _model.PlayerMovement.Dash();

        internal void ActivatePlayerJump() => _model.PlayerMovement.Jump();

        internal void ActivatePlayerRestore() => _model.PlayerLifeRestore.DoRestore();

        internal void PlayerThrowKnife() => _model.PlayerProjectileSpawner.ThrowKnife();

        internal void UpdateHealthBar(float value) => _model.HealthBar.value += value;

        internal void UpdateManaBar(float value) => _model.ManaBar.value += value;
    }
}