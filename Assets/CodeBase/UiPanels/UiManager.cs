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

public class UiManager : MonoBehaviour
{
    public UIPanel startPanel; // Ссылка на StartPanel
    public UIPanel endPanel;   // Ссылка на EndPanel
    public UIPanel pausePanel; // Ссылка на PausePanel
    public UIPanel gameHudPanel;
    private GamePanel currentPanel = GamePanel.None; // Текущая активная панель



    // Метод для управления отображением панелей
    public void ShowPanel( GamePanel panel )
    {
        // Отключаем все панели
        HideAllPanels ();

        // Включаем нужную панель
        switch ( panel )
        {
            case GamePanel.StartPanel:
                startPanel.Show ();
                break;
            case GamePanel.EndPanel:
                endPanel.Show ();
                break;
            case GamePanel.PausePanel:
                pausePanel.Show ();
                break; 
            case GamePanel.GameHudPanel:
                gameHudPanel.Show ();
                break;
        }

        // Обновляем текущую активную панель
        currentPanel = panel;
    }

    // Отключаем все панели
    private void HideAllPanels( )
    {
        startPanel.Hide ();
        endPanel.Hide ();
        pausePanel.Hide ();
        gameHudPanel.Hide ();
    }

    // Метод для скрытия всех панелей и установки текущего состояния в None
    public void HideAllAndReset( )
    {
        HideAllPanels ();
        currentPanel = GamePanel.None;
    }
}
