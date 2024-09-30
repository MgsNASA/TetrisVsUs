public class AllServices
{
    private static AllServices _instance;
    public static AllServices Container => _instance ?? ( _instance = new AllServices () );

    public void RegisterSingle<TService>( TService implementation ) where TService : IService =>
        Implementation<TService>.ServiceInstance = implementation;

    public TService Single<TService>( ) where TService : IService =>
        Implementation<TService>.ServiceInstance;

    // Добавьте метод TrySingle
    public bool TrySingle<TService>( out TService service ) where TService : IService
    {
        service = Implementation<TService>.ServiceInstance;
        return service != null; // Возвращает true, если служба инициализирована
    }

    private static class Implementation<TService> where TService : IService
    {
        public static TService ServiceInstance;
    }
}
