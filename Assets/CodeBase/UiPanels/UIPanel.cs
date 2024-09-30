using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanel : MonoBehaviour
{
    public Button button;
    // Общий метод для показа панели
    public virtual void Show( )
    {
        gameObject.SetActive ( true );
        Debug.Log ( $"{GetType ().Name} показана" );
    }

    // Общий метод для скрытия панели
    public virtual void Hide( )
    {
        gameObject.SetActive ( false );
        Debug.Log ( $"{GetType ().Name} скрыта" );
    }
}
