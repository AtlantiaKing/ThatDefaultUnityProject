using UnityEngine;

namespace that
{
	public class ParticlePathTester : MonoBehaviour
	{
		private enum PlayMethod { PlayAttached, PlayTrailing }

		[SerializeField] private bool _play = false;
		[SerializeField] private ParticlePatch _particlePatch;
		[SerializeField] private PlayMethod _playMethod;
		[SerializeField] private Transform _targetTransform;

		void Update()
		{
			if (_play == false) return;

			_play = false;
			Transform transformToUse = _targetTransform != null ? _targetTransform : transform;
			switch (_playMethod)
			{
				case PlayMethod.PlayAttached:
					_particlePatch.PlayAttached(transformToUse);
					break;
				case PlayMethod.PlayTrailing:
					_particlePatch.PlayTrailing(transformToUse);
					break;
			}
		}
	}
}
