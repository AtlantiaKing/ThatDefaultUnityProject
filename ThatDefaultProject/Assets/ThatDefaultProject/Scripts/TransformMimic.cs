using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace That
{
    public class TransformMimic : MonoBehaviour
    {
        [Header("Mimic")]
        [SerializeField] private Transform _transformToMimic;
        [Header("Settings")]
        [SerializeField] private Vector3 _positionOffset = new(0,0,0);
        [SerializeField] private bool _mimicPosition = true;
        [SerializeField] private bool _mimicRotation = true;

        void Update()
        {
            if (_transformToMimic == null) return;

            if (_mimicPosition)
            {
                transform.position = _transformToMimic.position + _positionOffset;
            }
            if (_mimicRotation) 
            {
                transform.rotation = _transformToMimic.rotation;
            }
        }
    }
}
