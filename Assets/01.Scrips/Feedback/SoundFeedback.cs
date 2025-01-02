using ObjectPooling;
using SoundManage;
using UnityEngine;

namespace FeedbackSystem
{

    public class SoundFeedback : Feedback
    {

        [SerializeField] private SoundSO _soundSO;

        public override void CreateFeedback()
        {
            SoundPlayer soundPlayer = PoolManager.Instance.Pop(PoolingType.SoundPlayer) as SoundPlayer;
            if(soundPlayer == null)
            {
                Debug.Log("???");
                return;
            }
            soundPlayer.PlaySound(_soundSO);
        }

        public override void FinishFeedback()
        {
        }
    }
}