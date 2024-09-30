using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private GameStateMachine _gameStateMachine;
    private SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;

    public LoadLevelState( GameStateMachine gameStateMachine , SceneLoader sceneLoader , LoadingCurtain curtain,IGameFactory gameFactory )
    {
        this._gameStateMachine = gameStateMachine;
        this._sceneLoader = sceneLoader;
        this._curtain = curtain;
        _gameFactory = gameFactory;
    }

    public void Enter( string sceneName )
    {
        _curtain.Show ();
        _sceneLoader.Load ( sceneName , OnLoaded );
    }

    public void Exit( )
    {
        _curtain.Hide ();
    }
    private void OnLoaded( )
    {
        _gameStateMachine.Enter<GameLoopState> ();

    }
}
