using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

// Credits: Zain Al Rubaie

namespace that
{
    [CreateAssetMenu(menuName = "ThatAudio/AudioPatch")]
    public class AudioPatch : ScriptableObject
    {
        [Header("Possibly played clips")]
        public AudioClip[] _audioClips;
        private List<AudioClip> _uniqueClips; //a list of remaining/unique clips

        [Header("Possible variation")]
        [Range(0, 1)] public float maxVolume = 1;
        [Range(0, 1)] public float minVolume = 1;
        [Range(-3, 3)] public float maxPitch = 1;
        [Range(-3, 3)] public float minPitch = 1;

        [Header("Optional")]
        [SerializeField] private AudioMixerGroup _mixer;
        private float _longestClipTime;

        private void Awake()
        {
            _uniqueClips = _audioClips.ToList(); //converting the given clips to a list, then putting them in _uniqueclips too
            _longestClipTime = _uniqueClips.Max(x => x.length);
        }
        private void OnValidate() //makes it so we can mess around with the volume/pitch using sliders and gives us the possibility to play sounds with a random pitch and volume
        {
            if (minVolume > maxVolume)
            {
                minVolume = maxVolume;
            }
            if (minPitch > maxPitch)
            {
                minPitch = maxPitch;
            }
        }

        public void PlayTrailingAudioSource(Transform transform)
        {
            SpawnTrailingAudioSource(transform, out AudioSource src, out DestroyTimer timer);
            timer.Time = _longestClipTime;
            Play(src);
        }
        
        public void PlayOneShotTrailingAudioSource(Transform transform)
        {
            SpawnTrailingAudioSource(transform, out AudioSource src, out DestroyTimer timer);
            timer.Time = _longestClipTime;
            PlayOneShot(src);
        }

        public void Play(AudioSource source)
        {
            if (_mixer) source.outputAudioMixerGroup = _mixer;
            source.clip = ReturnRandomClip(); //gets a random clip and loads it into the audio source
                                              //takes a random audio clip with a random pitch and volume within the user defined range.
            source.volume = Random.Range(minVolume, maxVolume);
            source.pitch = Random.Range(minPitch, maxPitch);
            source.Play();
        }

        public void PlayOneShot(AudioSource source) //Plays a oneshot sound (useful for playing multiple overlapping sounds from 1 gameobject)
        {
            if (_mixer) source.outputAudioMixerGroup = _mixer;
            source.pitch = Random.Range(minPitch, maxPitch);
            source.PlayOneShot(ReturnRandomClip(), Random.Range(minVolume, maxVolume));
        }

        private AudioClip ReturnRandomClip()
        {

            if (_uniqueClips.Count > 0) //if there are still unique clips remaining within the uniqueclips variable
            {
                int randomIndex = Random.Range(0, _uniqueClips.Count); //makes a random index within the range of the audioclip lost 
                AudioClip randomClip = _uniqueClips[randomIndex]; //copies an audioclip using that random index
                _uniqueClips.RemoveAt(randomIndex); //removes the element at that index after copying it

                return randomClip; //returns this element
            }
            else //if the remaining unique elements run out
            {
                _uniqueClips = _audioClips.ToList(); //refresh the list of remaining elements and repeat
                int randomIndex = Random.Range(0, _uniqueClips.Count);
                AudioClip randomClip = _uniqueClips[randomIndex];
                _uniqueClips.RemoveAt(randomIndex);

                return randomClip;
            }
        }

        private void SpawnTrailingAudioSource(Transform transform, out AudioSource audioSource, out DestroyTimer destroyTimer)
        {
            var obj = new GameObject("[THAT] TrailingAudioSource");
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            audioSource = obj.AddComponent<AudioSource>();
            destroyTimer = obj.AddComponent<DestroyTimer>();
        }
    }
}
