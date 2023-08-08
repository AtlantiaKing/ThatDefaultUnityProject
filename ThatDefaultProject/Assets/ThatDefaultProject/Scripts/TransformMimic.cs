using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace that
{
    public class TransformMimic : MonoBehaviour
    {
        [Header("Mimic")]
        [SerializeField] private Transform _transformToMimic;
        [Header("Settings")]
        [SerializeField] private bool _mimicPosition = true;
        [SerializeField] private bool _mimicRotation = true;

        void Update()
        {
            if (_transformToMimic == null) return;

            if (_mimicPosition)
            {
                transform.position = _transformToMimic.position;
            }
            if (_mimicRotation) 
            {
                transform.rotation = _transformToMimic.rotation;
            }
        }
    }
}
