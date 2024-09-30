using UnityEngine;

public class GameBoostrapper : MonoBehaviour , ICoroutineRunner
{
    private Game _game;
    public LoadingCurtain Curtain;
    private void Awake( )
    {
        _game = new Game (this, Curtain);
        _game._stateMachine.Enter<BootstrapState> ();
        DontDestroyOnLoad (this);
    }
}
