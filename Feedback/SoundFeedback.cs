using ObjectPooling;
using UnityEngine;

public class SoundFeedback : Feedback
{
    [SerializeField] private SoundSO soundData;
    
    public override void PlayFeedback()
    {
        SoundPlayer soundPlayer = PoolManager.Instance.Pop(PoolingType.SoundPlayer) as SoundPlayer;
        soundPlayer.transform.position = transform.position;
        soundPlayer?.PlaySound(soundData);
    }

    public override void StopFeedback()
    {
        
    }
}