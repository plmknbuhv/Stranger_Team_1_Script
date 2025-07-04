using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
using ObjectPooling;

public class SoundPlayer : PoolableMono
{
    [SerializeField] private AudioMixerGroup sfxGroup, musicGroup;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.maxDistance = 30;
    }

    public void PlaySound(SoundSO data)
    {
        if (data.audioType == AudioType.SFX)
        {
            _audioSource.outputAudioMixerGroup = sfxGroup;
        }
        else if(data.audioType == AudioType.Music)
        {
            _audioSource.outputAudioMixerGroup = musicGroup; 
        }

        _audioSource.volume = data.volume;
        _audioSource.pitch = data.basePitch;
        if (data.randomizePitch)
        {
            _audioSource.pitch 
                += Random.Range(-data.randomPitchModifier, data.randomPitchModifier);
        }

        _audioSource.clip = data.clip;
        _audioSource.loop = data.loop;

        if (!data.loop)
        {
            float time = _audioSource.clip.length + 0.2f;
            DOVirtual.DelayedCall(time, () => PoolManager.Instance.Push(this));
        }
        _audioSource.Play();
    }

    public void StopAndGoToPool()
    {
        _audioSource.Stop();
        PoolManager.Instance.Push(this);
    }

    public override void ResetItem()
    {
        
    }
}