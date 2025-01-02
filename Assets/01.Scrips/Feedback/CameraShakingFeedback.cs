using Managers;
using UnityEngine;
namespace FeedbackSystem
{
    public class CameraShakingFeedback : Feedback
    {
        [SerializeField] private float _power;
        [SerializeField] private float _duration;

        public override void CreateFeedback()
        {
            CameraManager.Instance.Shake(_power, _duration);
        }

        public override void FinishFeedback()
        {

        }
    }
}