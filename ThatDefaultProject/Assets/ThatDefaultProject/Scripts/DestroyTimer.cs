using UnityEngine;

namespace that
{
    public class DestroyTimer : MonoBehaviour
    {
        [Tooltip(
            "Time can only have meaningful change when it is set before the Start of DestroyTimer is called. " +
            "Start is called the first frame that the script is active. " +
            "If you want to set the time later, disable the script."
            )]
        [SerializeField] private float _time = 1f;

        /// <summary>
        /// Time can only have meaningful change when it is set before the Start of DestroyTimer is called.
        /// <para>Start is called the first frame that the script is active.</para>
        /// <para>If you want to set the time later, disable the script.</para>
        /// </summary>
        public float Time { get { return _time; } set {  _time = value; } }

        void Start() => Destroy(gameObject, _time);
    }
}
