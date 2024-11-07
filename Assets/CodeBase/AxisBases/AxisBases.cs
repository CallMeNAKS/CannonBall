using UnityEngine;

namespace CodeBase.Domain.AxisBases
{
    public class AxisBases : AbstractAxisBases
    {
        [SerializeField] private Transform _axis;
        [SerializeField] private Vector2 _current;
        public Vector2 Current => _current;

        private void Awake()
        {
            Apply();
        }

        private void Apply()
        {
            _axis.rotation = Quaternion.Euler(_current.y, _current.x, 0);
        }

        public override void Rotate(Vector2 vector)
        {
            _current += vector;
            Apply();
        }
    }
}