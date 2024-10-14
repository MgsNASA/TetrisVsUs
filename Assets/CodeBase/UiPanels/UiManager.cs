using UnityEngine;
using UnityEngine.UI;

// Перечисление для всех типов панелей
public enum GamePanel
{
    StartPanel,
    EndPanel,
    PausePanel,
    GameHudPanel,
    None // Для случаев, когда ни одна панель не активна
}

public class UiManager : MonoBehaviour, IStateClass
{
    public UIPanel startPanel; // Ссылка на StartPanel
    public UIPanel endPanel;   // Ссылка на EndPanel
    public UIPanel pausePanel; // Ссылка на PausePanel
    public UIPanel gameHudPanel;
    private GamePanel currentPanel = GamePanel.None; // Текущая активная панель
    public VerticalMovementTracker verticalMovementTracker;
    internal object VerticalMovementTracker;

    public void Initialize( GameObject player , GameProcessController gameProcessController)
    {
        startPanel = GetComponentInChildren<StartPanel> ();
        endPanel = GetComponentInChildren<EndGamePanel> ();
        gameHudPanel = GetComponentInChildren<GameHudPanel> ();
        pausePanel = GetComponentInChildren<PausePanel> ();
        verticalMovementTracker = GetComponentInChildren<VerticalMovementTracker> ();

        // Инициализация панелей с GameProcessController
        startPanel.Initialize ( gameProcessController );
        endPanel.Initialize ( gameProcessController );
        gameHudPanel.Initialize ( gameProcessController );
        pausePanel.Initialize ( gameProcessController );

        // Инициализация VerticalMovementTracker
        verticalMovementTracker.Initialize ( player );
        ShowPanel ( GamePanel.StartPanel );
    }

    private void Awake( )
    {
        verticalMovementTracker = GetComponentInChildren<VerticalMovementTracker> ();
       
    }

    public void ShowPanel( GamePanel panel )
    {
        // Отключаем все панели
        HideAllPanels ();

        // Проверяем и активируем нужную панель
        switch ( panel )
        {
            case GamePanel.StartPanel:
                if ( !IsDestroyed ( startPanel ) )
                    startPanel.Show ();
                break;
            case GamePanel.EndPanel:
                if ( !IsDestroyed ( endPanel ) )
                    endPanel.Show ();
                break;
            case GamePanel.PausePanel:
                if ( !IsDestroyed ( pausePanel ) )
                    pausePanel.Show ();
                break;
            case GamePanel.GameHudPanel:
                if ( !IsDestroyed ( gameHudPanel ) )
                    gameHudPanel.Show ();
                break;
        }

        // Обновляем текущую активную панель
        currentPanel = panel;
    }


    // Отключаем все панели
    public void HideAllPanels( )
    {
        if ( !IsDestroyed ( startPanel ) )
            startPanel.Hide ();
        if ( !IsDestroyed ( endPanel ) )
            endPanel.Hide ();
        if ( !IsDestroyed ( pausePanel ) )
            pausePanel.Hide ();
        if ( !IsDestroyed ( gameHudPanel ) )
            gameHudPanel.Hide ();
    }


    private bool IsDestroyed( Object obj )
    {
        return obj == null || obj.Equals ( null );
    }




    public void StartClass( )
    {
        // Сброс состояния UI, показываем начальную панель
        ShowPanel ( GamePanel.StartPanel );
        Debug.Log ( "UI Manager has started." );
    }

    public void Pause( )
    {
        // Показываем панель паузы и скрываем HUD
        ShowPanel ( GamePanel.PausePanel );
        verticalMovementTracker.Pause ();
        Debug.Log ( "Game is paused." );
    }

    public void Resume( )
    {
        // Скрываем панель паузы и показываем HUD
        ShowPanel ( GamePanel.GameHudPanel );
        Debug.Log ( "Game has resumed." );
        verticalMovementTracker.Resume ();
    }

    public void Restart( )
    {
        // Скрываем все панели и показываем начальную панель
        HideAllPanels ();
        ShowPanel ( GamePanel.StartPanel );
        Debug.Log ( "Game has restarted." );
        verticalMovementTracker.Restart ();
    }


}
