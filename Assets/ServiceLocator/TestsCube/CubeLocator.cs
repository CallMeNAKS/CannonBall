using UnityEngine;

namespace DefaultNamespace.TestsCube
{
    public class CubeLocator : MonoBehaviour
    {
        private IServiceLocator<ICube> _cubeLocator;
        
        [SerializeField] private ICube _cube;

        private void Awake()
        {
            _cubeLocator.Register(_cube);
        }
    }
}