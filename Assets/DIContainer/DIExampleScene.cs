using UnityEditor;
using UnityEngine;

namespace DIContainer
{
    public class DIExampleScene : MonoBehaviour
    {
        public void Init(DIContainer projectContainer)
        {
            // var serviceWithoutTag = projectContainer.Resolve<MyAwesomeProjectService>();
            // var service1 = projectContainer.Resolve<MyAwesomeProjectService>("option 1");
            // var service2 = projectContainer.Resolve<MyAwesomeProjectService>("option 2");
            
            var sceneContainer = new DIContainer(projectContainer);
            sceneContainer.RegisterSingleton(c => new MySceneService(c.Resolve<MyAwesomeProjectService>()));
            sceneContainer.RegisterSingleton(_ => new MyAwesomeFactory());
            sceneContainer.RegisterInstance(new MyAwesomeObject("instance", 10));
            
            var objectFactory = sceneContainer.Resolve<MyAwesomeFactory>();

            for (var i = 0; i < 3; i++)
            {
                var id = $"object {i}";
                var obj = objectFactory.CreateInstance(id, i);
                Debug.Log($"Object created with factory. Id: {id}, Object: {obj}");
            }
        }
    }
}