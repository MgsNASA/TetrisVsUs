using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanel : MonoBehaviour
{
    public Button button;
    protected GameProcessController gameProcessController; // Поле для GameProcessController

    // Общий метод для показа панели
    public abstract void Show( );

    // Общий метод для скрытия панели
    public abstract void Hide( );

    // Метод для инициализации GameProcessController
    public void Initialize( GameProcessController controller )
    {
        gameProcessController = controller;
    }
}
