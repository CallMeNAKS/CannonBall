namespace DefaultNamespace
{
    public abstract class ServiceBase : IService
    {
        public int Version { get; }

        public ServiceBase(int version)
        {
            Version = version;
        } 
    }
}