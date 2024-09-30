using UnityEngine;
using UnityEngine.UI;

// Перечисление для всех типов панелей
public enum GamePanel
{
    StartPanel,
    EndPanel,
    PausePanel,
    None // Для случаев, когда ни одна панель не активна
}

public class UiManager : MonoBehaviour
{
    public UIPanel startPanel; // Ссылка на StartPanel
    public UIPanel endPanel;   // Ссылка на EndPanel
    public UIPanel pausePanel; // Ссылка на PausePanel

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
                startPanel.gameObject.SetActive ( true );
                break;
            case GamePanel.EndPanel:
                endPanel.gameObject.SetActive ( true );
                break;
            case GamePanel.PausePanel:
                pausePanel.gameObject.SetActive ( true );
                break;
        }

        // Обновляем текущую активную панель
        currentPanel = panel;
    }

    // Отключаем все панели
    private void HideAllPanels( )
    {
        startPanel.gameObject.SetActive ( false );
        endPanel.gameObject.SetActive ( false );
        pausePanel.gameObject.SetActive ( false );
    }

    // Метод для скрытия всех панелей и установки текущего состояния в None
    public void HideAllAndReset( )
    {
        HideAllPanels ();
        currentPanel = GamePanel.None;
    }
}
