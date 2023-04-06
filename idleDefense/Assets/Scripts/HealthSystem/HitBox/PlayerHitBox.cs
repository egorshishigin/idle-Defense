using Zenject;

using Game.GameOver;

using WeaponSystem;

using UnityEngine;

namespace HealthSystem
{
    public class PlayerHitBox : HitBoxBase
    {
        [SerializeField] private PlayerWeapon _weapon;

        private GameOver _gameOver;

        [Inject]
        private void Construct(GameOver gameOver)
        {
            _gameOver = gameOver;
        }

        protected override void DiedHandler()
        {
            _weapon.gameObject.SetActive(false);

            _gameOver.FinishGame();
        }
    }
}