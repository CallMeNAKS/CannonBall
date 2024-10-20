using UnityEngine;

namespace CodeBase.Domain.AxisBases
{
    public class ReducerAxisBase : AbstractAxisBases
    {
        [SerializeField] private AbstractAxisBases _axis;
        [SerializeField] private Vector2 _cofficient = Vector2.one;
        
        public override void Rotate(Vector2 vector)
        {
            _axis.Rotate(vector * _cofficient);
        }
    }
}