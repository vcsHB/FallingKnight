using System;
using StatSystem;
using Unity.VisualScripting;
using UnityEngine;
namespace Agents.Players
{

    public class PlayerMover : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _reducePower;
        [SerializeField] private float _gravityScale = 1f;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private float _wallDetectRange;
        private Rigidbody2D _rigidCompo;
        public bool IsGravity => _rigidCompo.gravityScale != 0f;
        private Player _player;
        private bool _isFalling;

        private float _movementX;
        private float _movementY;
        private float _moveSpeedMultiplier, _originalgravity;
        [field: SerializeField] public bool CanManualMove { get; set; } = true;
        private Stat _moveSpeedStat;

        private void Awake()
        {
            _rigidCompo = GetComponent<Rigidbody2D>();
        }


        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _moveSpeedMultiplier = 1f;

            _moveSpeedStat = _player.Status.moveSpeed;

        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        private void FixedUpdate()
        {
            if (CanManualMove)
            {
                _rigidCompo.linearVelocityX = _movementX * _moveSpeedStat.GetValue() * _moveSpeedMultiplier;
            }
            // if (_isFalling)
            // {
            //     _movementY += _gravityScale * Time.fixedDeltaTime;
            //     _rigidCompo.linearVelocityY = _movementY * _gravityScale;
            // }
            // else
            // {
            //     _movementY -= _reducePower * Time.fixedDeltaTime;
            // }

            //OnMovement?.Invoke(_rigidCompo.linearVelocity);
        }

        public void SetMovement(float xMovement)
        {
            _movementX = xMovement;
            //_renderer.FlipController(xMovement); // 나중에
        }

        public void SetFall(bool value)
        {

        }


        public void StopImmediately(bool isYtoo = false)
        {
            if (isYtoo)
            {
                _rigidCompo.linearVelocity = Vector2.zero;
            }
            else
            {
                _rigidCompo.linearVelocityX = 0f;
            }
            _movementX = 0f;
        }

        public void AddForceToOuterWall(float dashPower, float jumpPower)
        {
            Vector2 wallPoint = DetectWall();
            Vector2 direction = ((Vector2)transform.position - wallPoint).normalized;
            direction = new Vector2(direction.x * dashPower, jumpPower);
            AddForce(direction);
        }

        public void AddForce(Vector2 power)
        {
            _rigidCompo.AddForce(power, ForceMode2D.Impulse);
        }

        public void ReduceVerticalVelocity(float amount)
        {
            float yVelocity = _rigidCompo.linearVelocityY;
            if (yVelocity > 0) return;
            yVelocity += amount;

            yVelocity = Mathf.Clamp(yVelocity, -100f, 0);
            _rigidCompo.linearVelocityY = yVelocity;
        }

        public void SetGravity(bool value)
        {
            _rigidCompo.gravityScale = value ? _gravityScale : 0f;
        }

        public Vector2 DetectWall()
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, _wallDetectRange, Vector2.zero, 0f, _wallLayer);
            if (hit.collider == null) return Vector2.zero;

            return hit.point;
        }

    }
}