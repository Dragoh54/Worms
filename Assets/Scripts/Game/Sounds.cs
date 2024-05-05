using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    private AudioSource _audioSrc;
    public SoundArrays[] soundArrays;

    private void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
    }

    public void PlaySound(int id, float volume = 1f, bool rnd = false, bool destroy = false, float p1 = 0.85f, float p2 = 1.2f)
    {
        AudioClip clip = rnd ? soundArrays[id].soundArray[Random.Range(0, soundArrays[id].soundArray.Length)] : clips[id];

        //_audioSrc.PlayOneShot(clip, volume);

        if (destroy)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
        else
        {
            _audioSrc.PlayOneShot(clip, volume);
        }
    }

    public void StopSound()
    {
        _audioSrc.Stop();
    }

    public AudioClip GetAudioClip(int id) 
    {
        return clips[id];    
    }

    [System.Serializable]
    public class SoundArrays
    {
        public AudioClip[] soundArray;
    }
}
