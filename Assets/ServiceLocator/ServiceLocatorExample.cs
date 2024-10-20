using UnityEngine;

namespace DefaultNamespace
{
    public class ServiceLocatorExample : MonoBehaviour
    {
        private IServiceLocator<IService> _locator;
        private void Awake()
        {
            _locator = new ServiceLocator<IService>();

            var analytics = new AnalyticsService(1);
            var iap = new IAPService(1);
            
            _locator.Register(analytics);
            _locator.Register(iap);
        }

        private void Start()
        {
             var analytics = _locator.Get<AnalyticsService>();
             
             Debug.Log($"AnaliticsService version: {analytics.Version}");
        }
    }
}