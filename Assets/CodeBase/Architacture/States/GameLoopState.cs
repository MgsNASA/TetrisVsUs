using UnityEngine;

public class GameLoopState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly IGameFactory _gameFactory;
    
    public GameLoopState( GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
     
    }

    public void Enter( )
    {
        
        AllServices.Container.Single<IGameFactory> ().CreateObject ( "Prefab/Player/Player" );
        AllServices.Container.Single<IGameFactory> ().CreateObject ( "Prefab/GameScriptsObjects/GameProcessController" );
      //  _gameFactory.CreateObject ( "Prefabs/Player/Player.prefab" );
        // Начало игрового процесса
     
        Debug.Log ( "Game Started" );
        
        // Дополнительная логика, например, создание игрока и окружения
        // _gameFactory.CreatePlayer ();
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
