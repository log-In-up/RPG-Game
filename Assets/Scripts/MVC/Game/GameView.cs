using UnityEngine.UI;

namespace MVC
{
    public sealed class GameView
    {
        #region Fields
        private GameController _controller;
        #endregion

        public GameView(GameController controller, GameModel model)
        {
            _controller = controller;
        }

        #region Public methods
        internal void OnClickBowShot() => _controller.ActivatePlayerBowShot();

        internal void OnClickDash() => _controller.ActivatePlayerDash();

        internal void OnCLickJump() => _controller.ActivatePlayerJump();

        internal void OnClickRestore() => _controller.ActivatePlayerRestore();

        internal void OnClickThrowKnife() => _controller.PlayerThrowKnife();
        #endregion
    }
}