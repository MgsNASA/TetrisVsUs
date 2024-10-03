using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanel : MonoBehaviour
{
    public Button button;
    // Общий метод для показа панели
    public abstract void Show( );


    // Общий метод для скрытия панели
    public abstract void Hide( );

}
