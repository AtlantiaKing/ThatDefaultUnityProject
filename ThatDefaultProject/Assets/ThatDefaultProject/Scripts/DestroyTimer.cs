using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace that
{
    public class DestroyTimer : MonoBehaviour
    {
        [SerializeField] private float _time = 1f;
        public float TimeLeft { get { return _time; } set { _time = value; } }

#if UNITY_EDITOR
        [Header("Editor only. Tooltip for info")]
        [Tooltip("This value will show off the start value while the time value will tick down to 0.")]
        [SerializeField] private float _startValue;
#endif

#if UNITY_EDITOR
        void Start()
        {
            _startValue = _time;
        }
#endif

        void Update()
        {
            _time -= Time.deltaTime;
            if (_time > 0f) return;
            Destroy(gameObject);
        }
    }
}
