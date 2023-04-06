using Zenject;

using HealthSystem;

using WeaponSystem;

using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private float _moveSpeed;

        [SerializeField] private float _attackDistance;

        [SerializeField] private WeaponBase _weapon;

        [SerializeField] private Rigidbody2D _rigidbody;

        private PlayerHitBox _player;

        private Vector2 _direction;

        private float _dictanceToPlayer;

        private bool _move;

        [Inject]
        private void Construct(PlayerHitBox player)
        {
            _player = player;
        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            _dictanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);

            if (_dictanceToPlayer < _attackDistance)
            {
                _move = false;

                _weapon.enabled = true;

                enabled = false;
            }
            else
            {
                _move = true;
            }
        }

        private void FixedUpdate()
        {
            if (!_move)
                return;

            Move(_direction.normalized);
        }

        private void Initialize()
        {
            _weapon.enabled = false;

            _direction = _player.transform.position - transform.position;

            _direction.Normalize();

            float playerRotation = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, playerRotation - 90f);
        }

        private void Move(Vector2 direction)
        {
            _rigidbody.MovePosition((Vector2)transform.position + (direction * _moveSpeed * Time.fixedDeltaTime));
        }
    }
}