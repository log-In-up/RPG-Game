using GameData;
using Player;
using System;
using UnityEngine.UI;

namespace MVC
{
    public sealed class GameModel
    {
        #region Fields
        private PlayerMovement _playerMovement;
        private PlayerProjectileSpawner _playerProjectileSpawner;
        private PlayerRestore _playerLifeRestore;
        private Slider _healthBar, _manaBar;
        private PlayerData _playerData;
        #endregion

        #region Properties
        public PlayerMovement PlayerMovement => _playerMovement;
        public PlayerProjectileSpawner PlayerProjectileSpawner => _playerProjectileSpawner;
        public PlayerRestore PlayerLifeRestore => _playerLifeRestore;
        public Slider HealthBar => _healthBar;
        public Slider ManaBar => _manaBar;
        #endregion

        public GameModel(PlayerBehaviour playerBehaviour, PlayerData playerData)
        {
            _playerMovement = playerBehaviour.Movement;
            _playerProjectileSpawner = playerBehaviour.ProjectileSpawner;
            _playerLifeRestore = playerBehaviour.LifeRestore;

            _playerData = playerData;
        }

        internal void SetupBars(Slider healthBar, Slider manaBar)
        {
            _healthBar = healthBar;
            _healthBar.minValue = 0.0f;
            _healthBar.maxValue = _playerData.MaxHealth;

            _manaBar = manaBar;
            _manaBar.minValue = 0.0f;
            _manaBar.maxValue = _playerData.MaxMana;
        }
    }
}
