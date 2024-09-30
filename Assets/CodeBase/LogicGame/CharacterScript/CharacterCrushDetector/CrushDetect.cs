using UnityEngine;

public class CrushDetect : MonoBehaviour,ICrushDetector
{
    public float raycastLength = 1f;
    public LayerMask platformLayer; // Один слой для блоков и земли
    public Color rayColor = Color.red; // Цвет для лучей вверх и вниз
    public Vector3 raycastMargin = Vector3.zero; // Смещение для лучей

    private bool isCrushed = false;
    public void OnCharacterDataChanged( CharacterStats stats )
    {
       
    }

    public bool CheckIfCrushed( Transform transform )
    {
        // Смещаем начало луча на margin
        Vector3 rayStartPos = transform.position + raycastMargin;

        // Проверяем, падает ли на персонажа блок (или платформа)
        RaycastHit2D hit = Physics2D.Raycast ( rayStartPos , Vector2.up , raycastLength , platformLayer );

        if ( hit.collider != null )
        {
            // Проверяем, стоит ли персонаж на платформе или блоке
            RaycastHit2D groundHit = Physics2D.Raycast ( rayStartPos , Vector2.down , raycastLength , platformLayer );

            if ( groundHit.collider != null )
            {
            //    GameProcessController.Instance.GameOver ();
                isCrushed = true; // Персонаж зажат между землей и блоком
                return true;
            }
        }

        isCrushed = false;
        return false; // Персонаж не раздавлен
    }


    // Метод для отрисовки Gizmos, чтобы видеть лучи постоянно
    private void OnDrawGizmos( )
    {
        Gizmos.color = rayColor;

        // Смещаем начало луча на margin для отрисовки Gizmos
        Vector3 rayStartPos = transform.position + raycastMargin;

        // Отображаем вверх луч для проверки блоков
        Gizmos.DrawLine ( rayStartPos , rayStartPos + Vector3.up * raycastLength );

        // Отображаем вниз луч для проверки платформы
        Gizmos.DrawLine ( rayStartPos , rayStartPos + Vector3.down * raycastLength );
    }
}
