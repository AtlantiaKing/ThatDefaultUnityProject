using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace that
{
	[CreateAssetMenu(menuName = "ThatAudio/AudioPatch")]
	public class AudioPatch : ScriptableObject
	{
		private class PlayTimestamp
		{
			public PlayTimestamp(float clipLength, float timeStamp)
			{
				ClipLength = clipLength;
				TimeStamp = timeStamp;
			}
			public float ClipLength { get; set; }
			public float TimeStamp { get; set; }
		}

		[Header("Possibly played clips")]
		public AudioClip[] _audioClips;
		private List<AudioClip> _uniqueClips; //a list of remaining/unique clips

		[Header("Possible variation")]
		[Range(0, 1)][SerializeField] private float _maxVolume = 1;
		[Range(0, 1)][SerializeField] private float _minVolume = 1;
		[Range(-3, 3)][SerializeField] private float _maxPitch = 1;
		[Range(-3, 3)][SerializeField] private float _minPitch = 1;

		[Header("Optional")]
		[SerializeField] private AudioSource _trailingAudioSourcePrefab;
		[SerializeField] private AudioRolloffMode _defaultTrailingRolloffMode = AudioRolloffMode.Linear;
		[SerializeField] private AudioMixerGroup _mixer;
		[SerializeField] private float _cooldown = 0;
		[Tooltip("-1 means unlimited")][SerializeField] private int _maxAmountActive = -1;
		private float _lastTime;
		private float _longestClipTime;
		public float LongestClipTime
		{
			get
			{
				if (_longestClipTime <= 0)
				{
					_longestClipTime = _audioClips.Max(x => x.length);
				}
				return _longestClipTime;
			}
		}

		private readonly List<PlayTimestamp> _playTimestamps = new();

		private void Awake()
		{
			if (_uniqueClips == null || _uniqueClips.Count == 0) return;

			_uniqueClips = _audioClips.ToList(); //converting the given clips to a list, then putting them in _uniqueclips too
			_lastTime = Time.timeSinceLevelLoad;
		}
		private void OnValidate() //makes it so we can mess around with the volume/pitch using sliders and gives us the possibility to play sounds with a random pitch and volume
		{
			if (_minVolume > _maxVolume)
			{
				_minVolume = _maxVolume;
			}
			if (_minPitch > _maxPitch)
			{
				_minPitch = _maxPitch;
			}
			_cooldown = Mathf.Max(_cooldown, 0);
			_maxAmountActive = Mathf.Max(_maxAmountActive, -1);
		}

		public void PlayTrailing(Transform transform)
		{
			AudioSource audioSource = SpawnTrailingAudioSource(transform);
			Play(audioSource);
		}

		public void PlayOneShotTrailing(Transform transform)
		{
			AudioSource audioSource = SpawnTrailingAudioSource(transform);
			PlayOneShot(audioSource);
		}

		public void Play(AudioSource source)
		{
			if (!CanPlay())
			{
				return;
			}
			if (_mixer) source.outputAudioMixerGroup = _mixer;
			source.clip = ReturnRandomClip();
			source.volume = Random.Range(_minVolume, _maxVolume);
			source.pitch = Random.Range(_minPitch, _maxPitch);
			source.Play();
			AddPlaytimeStamp(new(source.clip.length, Time.timeSinceLevelLoad));
		}

		public void PlayOneShot(AudioSource source)
		{
			if (!CanPlay())
			{
				return;
			}
			if (_mixer) source.outputAudioMixerGroup = _mixer;
			source.pitch = Random.Range(_minPitch, _maxPitch);
			AudioClip clip = ReturnRandomClip();
			source.PlayOneShot(clip, Random.Range(_minVolume, _maxVolume));
			AddPlaytimeStamp(new(clip.length, Time.timeSinceLevelLoad));
		}

		private AudioClip ReturnRandomClip()
		{
			if (_audioClips.Length == 1)
			{
				return _audioClips[0];
			}
			Debug.Assert(_audioClips.Length != 0, $"AudioPatch ({name}) is empty!");
			if (_uniqueClips.Count <= 0)
			{
				_uniqueClips = _audioClips.ToList();
			}
			int randomIndex = Random.Range(0, _uniqueClips.Count);
			AudioClip randomClip = _uniqueClips[randomIndex];
			_uniqueClips.RemoveAt(randomIndex);

			return randomClip;
		}

		private AudioSource SpawnTrailingAudioSource(Transform transform)
		{
			AudioSource obj;
			if (_trailingAudioSourcePrefab == null)
			{
				obj = new GameObject("[AudioPatch] Trailing audiosource").AddComponent<AudioSource>();
			}
			else
			{
				obj = Instantiate(_trailingAudioSourcePrefab);
			}
			DestroyTimer destroyTimer = obj.gameObject.AddComponent<DestroyTimer>();
			destroyTimer.Time = LongestClipTime;
			destroyTimer.StartTimer();
			obj.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
			obj.playOnAwake = false;
			obj.rolloffMode = _defaultTrailingRolloffMode;
			return obj;
		}

		private bool HasCooldownPassedAndUpdate()
		{
			bool hasCooldownPassed = _cooldown <= Mathf.Abs(Time.timeSinceLevelLoad - _lastTime);
			if (hasCooldownPassed)
			{
				_lastTime = Time.timeSinceLevelLoad;
			}
			return hasCooldownPassed;
		}

		private bool CanPlay()
		{
			if (HasCooldownPassedAndUpdate())
			{
				return true;
			}
			if (_maxAmountActive == -1)
			{
				return true;
			}
			UpdateTimestamps();
			return _playTimestamps.Count < _maxAmountActive;
		}

		private void UpdateTimestamps()
		{
			_playTimestamps.RemoveAll(
				(PlayTimestamp playTimestamp) =>
				{
					return playTimestamp == null ||
					Mathf.Abs(Time.timeSinceLevelLoad - playTimestamp.TimeStamp) > playTimestamp.ClipLength;
				}
				);
		}
		private void AddPlaytimeStamp(PlayTimestamp timestamp)
		{
			if (_maxAmountActive != -1) _playTimestamps.Add(timestamp);
		}
	}
}