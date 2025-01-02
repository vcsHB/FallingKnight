using UnityEngine;
namespace Agents.Players
{

    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _rigidCompo;

        private void Awake()
        {
            _rigidCompo = GetComponent<Rigidbody2D>();
        }


        public void SetMovement()
        {


        }

        public void StopImmediatly(bool isYtoo)
        {

            if (isYtoo)
            {
                _rigidCompo.linearVelocity = Vector2.zero;
            }
            else
            {
                _rigidCompo.linearVelocityX = 0f;
            }
        }
    }
}
