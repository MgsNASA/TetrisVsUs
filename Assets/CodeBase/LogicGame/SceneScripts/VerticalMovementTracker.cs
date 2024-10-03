using TMPro;
using UnityEngine;

public class VerticalMovementTracker : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI; // Ссылка на компонент TextMeshPro
    public GameObject player;

    public void Awake( )
    {
        player = GameObject.Find ( "Player(Clone)" );
    }
    private void Update( )
    {
        // Отслеживаем текущее положение игрока по оси Y
        float currentYPosition = player.transform.position.y;

        // Округляем положение до ближайшего целого числа
        int roundedYPosition = Mathf.RoundToInt ( currentYPosition );

        // Обновляем текстовое поле с текущей позицией по оси Y
        UpdateText ( roundedYPosition );
    }

    // Метод для обновления текста на экране
    private void UpdateText( int currentYPosition )
    {
        if ( textMeshProUGUI != null )
        {
            textMeshProUGUI.text = $"Current Y Position: {currentYPosition} units";
        }
    }
}
