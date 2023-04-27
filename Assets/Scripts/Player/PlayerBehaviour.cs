using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public sealed class PlayerBehaviour : MonoBehaviour
    {
        #region Editor fields
        [SerializeField] private PlayerMovement _movement = null;
        [SerializeField] private PlayerProjectileSpawner _projectileSpawner = null;
        [SerializeField] private PlayerRestore _lifeRestore = null;
        #endregion

        #region Properties
        public PlayerMovement Movement => _movement;
        public PlayerProjectileSpawner ProjectileSpawner => _projectileSpawner;
        public PlayerRestore LifeRestore => _lifeRestore;
        #endregion

        #region Public methods
        internal void DisableConnectionWithSkilltree()
        {
            _movement.DisableConnectionWithSkilltree();
            _projectileSpawner.DisableConnectionWithSkilltree();
            _lifeRestore.DisableConnectionWithSkilltree();
        }

        internal void EnableConnectionWithSkilltree()
        {
            _movement.EnableConnectionWithSkilltree();
            _projectileSpawner.EnableConnectionWithSkilltree();
            _lifeRestore.EnableConnectionWithSkilltree();
        }
        #endregion
    }
}