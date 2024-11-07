using CodeBase.Services.Input;
using UnityEditor.SearchService;
using UnityEngine;

public class BootstrapState : IState
{
    private const string InitialScene = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader sceneLoader;
    private AllServices _services;

    public BootstrapState( GameStateMachine stateMachine , SceneLoader sceneLoader , AllServices services )
    {
        _stateMachine = stateMachine;
        this.sceneLoader = sceneLoader;
        _services = services;
        RegisterService ();
    }

    public void Enter( )
    {
        sceneLoader.Load ( InitialScene , onLoaded: EnterLoadLevel );
    }

    private void RegisterService( )
    {
        // Регистрируем IInputService
        _services.RegisterSingle<IInputService> (  InputService () );

        // Регистрируем IAssetProvider
        _services.RegisterSingle<IAssetProvider> ( new AssetProvider () );

        // Создаем экземпляр TetrisGridManager и регистрируем в контейнере
        var tetrisGridManager = new GameObject ( "TetrisGridManager" ).AddComponent<TetrisGridManager> ();
        _services.RegisterSingle<ITetrisGridManager> ( tetrisGridManager );

        // Создаем и регистрируем GameFactory
        var gameFactory = new GameFactory ( AllServices.Container.Single<IAssetProvider> () );
        _services.RegisterSingle<IGameFactory> ( gameFactory );
        _services.RegisterSingle<ITetrominoFactory> ( gameFactory );

        // Регистрируем другие зависимости
        _services.RegisterSingle<IPositionValidator> ( new GridPositionValidator ( tetrisGridManager ) );
        _services.RegisterSingle<IRotationManager> ( new RotationManager () );
        _services.RegisterSingle<IBlockMover> ( new BlockMover ( tetrisGridManager ) );
        _services.RegisterSingle<IBlockRotator> ( new BlockRotator ( tetrisGridManager ) );

        //  _services.RegisterSingle<IInputManager> ( new InputManager () );
    }



    private void EnterLoadLevel( )
    {
        Debug.Log ( "Enter" );
        _stateMachine.Enter<LoadLevelState , string> ( "Main" );
    }

    public void Exit( )
    {
    }

    public static IInputService InputService( )
    {
        IInputService service = null;

        if ( Application.isMobilePlatform ) // Для мобильных устройств
        {
            service = new MobileInputService ();
        }
        else // Для остальных платформ (ПК, веб и т.д.)
        {
            service = new StandaloneInputService ();
        }
        return service;
    }
}
