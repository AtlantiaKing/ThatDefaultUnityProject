using UnityEngine;

namespace That
{
    public class AudioPatchTester : MonoBehaviour
    {
        enum PlayMethod { Play, PlayOneShot, PlayTrailing, PlayOneShotTrailing }

        [SerializeField] private bool _play = false;
        [SerializeField] private AudioPatch _patch;
        [SerializeField] private PlayMethod _playMethod;

        [Header("Optional")]
        [SerializeField] private AudioSource _source;
        [SerializeField] private Transform _trailTransform;

        private void Update()
        {
            if (_play == false) return;

            _play = false;
            if (_source == null)
            {
                _source = gameObject.AddComponent<AudioSource>();
            }
            if (_trailTransform == null)
            {
                _trailTransform = gameObject.transform;
            }
            if (_patch == null)
            {
                Debug.LogWarning("No audio patch assigned");
                return;
            }

            switch (_playMethod)
            {
                case PlayMethod.Play:
                    _patch.Play(_source);
                    break;
                case PlayMethod.PlayOneShot:
                    _patch.PlayOneShot(_source);
                    break;
                case PlayMethod.PlayTrailing:
                    _patch.PlayTrailing(_trailTransform);
                    break;
                case PlayMethod.PlayOneShotTrailing:
                    _patch.PlayOneShotTrailing(_trailTransform);
                    break;
                default:
                    Debug.LogWarning("This play method is not supported. Please add it to the switch case.");
                    break;
            }
        }
    }
}
