using UnityEngine;

namespace that
{
	public class DestroyTimer : MonoBehaviour
	{
		[SerializeField] private float _time = 1f;
		[SerializeField] private bool _runOnStart = false;
		public bool RunOnStart { get => _runOnStart; }

		public float Time { get { return _time; } set { _time = value; } }

		void Start()
		{
			if (RunOnStart) StartTimer();
		}

		public void StartTimer()
		{
			Destroy(gameObject, _time);
		}
	}
}
