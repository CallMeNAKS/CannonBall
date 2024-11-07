using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public class Pupil : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _leftPupil;
        [SerializeField] private Transform _rightPupil;
        [SerializeField] private Transform _leftEye;
        [SerializeField] private Transform _rightEye;
        [SerializeField] private float _pupilRange = 0.05f;
        
        private void Update()
        {
            MovePupilTowardsPlayer(_leftPupil, _leftEye);
            MovePupilTowardsPlayer(_rightPupil, _rightEye);
        }
        
        private void MovePupilTowardsPlayer(Transform pupil, Transform eye)
        {
            Vector3 directionToPlayer = (_player.position - eye.position).normalized;
        
            Vector3 localDirection = eye.InverseTransformDirection(directionToPlayer);
        
            localDirection.x = Mathf.Clamp(localDirection.x, -_pupilRange, _pupilRange);
            localDirection.y = Mathf.Clamp(localDirection.y, -_pupilRange, _pupilRange);

            pupil.localPosition = new Vector3(localDirection.x, localDirection.y, pupil.localPosition.z);
        }
    }
}