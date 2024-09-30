public class Game
{
    public GameStateMachine _stateMachine;

    public Game( ICoroutineRunner coroutineRunner,LoadingCurtain curtain )
    {
        _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain ,AllServices.Container);
    }
}