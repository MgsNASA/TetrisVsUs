using System.Collections.Generic;


public class GameStateMachine
{
    private readonly Dictionary<System.Type , IExitableState> _states;
    private IExitableState _activateState;

    public GameStateMachine( SceneLoader sceneLoader , LoadingCurtain curtain , AllServices services )
    {
        _states = new Dictionary<System.Type , IExitableState>
        {
            [ typeof ( BootstrapState ) ] = new BootstrapState ( this , sceneLoader , services ) ,
            [ typeof ( LoadLevelState ) ] = new LoadLevelState ( this , sceneLoader , curtain ,services.Single<IGameFactory>()) ,
            [ typeof ( GameLoopState ) ] = new GameLoopState ( this )

        };
    }

    public void Enter<TState>( ) where TState : class, IState
    {
        IState state = ChangeState<TState> ();
        state.Enter ();
    }

    public void Enter<TState, TPayload>( TPayload payload ) where TState : class, IPayloadedState<TPayload>
    {
        IPayloadedState<TPayload> state = ChangeState<TState> ();
        state.Enter ( payload );
    }

    private TState ChangeState<TState>( ) where TState : class, IExitableState
    {
        _activateState?.Exit ();
        TState state = GetState<TState> ();
        _activateState = state;
        return state;
    }

    private TState GetState<TState>( ) where TState : class, IExitableState =>
        _states [ typeof ( TState ) ] as TState;
}
