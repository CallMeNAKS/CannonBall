using System;

namespace DIContainer.TestsCube
{
    public interface ICubeService
    {
        void ExpandCube();

        public event Action CubeExpanded;
    }
}