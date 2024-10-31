using System;
using UnityEngine;

namespace CodeBase.Domain.AxisBases
{
    public class AxisRangeLimits : AbstractAxisBases
    {
        [SerializeField] private AbstractAxisBases _origin;
        [SerializeField] private AxisBases _axis;

        [Header("Range Limits")] 
        [SerializeField] private Range _range;

        public override void Rotate(Vector2 vector)
        {
            vector.y = _range.In(_axis.Current.y + vector.y) ? vector.y : 0;
            _origin.Rotate(vector);
        }

        [Serializable]
        public struct Range
        {
            public float Min;
            public float Max;

            public bool In(float value)
            {
                return value >= Min && value <= Max;
            }
        }
    }
}