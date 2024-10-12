using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public LayerMask platformLayer;
    private bool isGrounded; // Флаг для проверки соприкосновения с землей
    private float groundBufferTime = 0.1f; // Буферное время, чтобы персонаж не думал, что он в воздухе из-за маленьких зазоров
    private float timeSinceLastGrounded;

    // Метод для проверки находится ли объект на земле

    public bool IsGrounded( Transform transform )
    {
        // Персонаж считается на земле, если он был на земле в последние groundBufferTime секунды
        return isGrounded || Time.time - timeSinceLastGrounded <= groundBufferTime;
    }

    // Когда объект касается земли
    private void OnCollisionStay2D( Collision2D collision )
    {
        if ( IsCollisionWithPlatform ( collision ) )
        {
            isGrounded = true;
            timeSinceLastGrounded = Time.time; // Обновляем время последнего касания земли
        }
    }

    // Когда объект покидает землю
    private void OnCollisionExit2D( Collision2D collision )
    {
        if ( IsCollisionWithPlatform ( collision ) )
        {
            isGrounded = false;
        }
    }

    // Проверяем, что столкновение происходит с землей (платформой)
    private bool IsCollisionWithPlatform( Collision2D collision )
    {
        // Сравниваем слой объекта с платформой
        return platformLayer == ( platformLayer | ( 1 << collision.gameObject.layer ) );
    }
}
