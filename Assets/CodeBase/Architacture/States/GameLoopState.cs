using UnityEngine;

public class GameLoopState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly IGameFactory _gameFactory;
    private GameProcessController _gameProcessController;
    public GameLoopState( GameStateMachine stateMachine )
    {
        _stateMachine = stateMachine;

    }

    public void Enter( )
    {

        _gameProcessController = AllServices.Container.Single<IGameFactory> ().CreateObject ( "Prefab/GameScriptsObjects/GameProcessController" ).GetComponent<GameProcessController> ();
        _gameProcessController.StartGame ();
        //_gameFactory.CreateObject ( "Prefabs/Player/Player.prefab" );

        Debug.Log ( "Game Started" );
    }

    public void Exit( )
    {
        Debug.Log ( "Exiting GameLoopState" );
    }

    public void OnLoaded( )
    {
        Debug.Log ( "Level Loaded" );
        //_gameProcessController.GameResume ();
    }
}
