using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera Camera;
    public Transform Player; // Ссылка на игрока
    public Spawner Spawner; // Ссылка на спавнер
    public float initialDistanceStep = 5.0f; // Начальное расстояние, на которое камера поднимется вверх
    public float followThreshold = 10.0f; // Высота, на которой камера переместится
    public float followSpeed = 2.0f; // Скорость плавного перемещения камеры
    public float distanceMultiplier = 1.2f; // Множитель для увеличения расстояния
    public float spawnerMoveStep = 10.0f; // Шаг перемещения спаунера по оси Y

    private float currentDistanceStep; // Текущее расстояние перемещения камеры
    private float nextCameraY; // Следующее положение камеры по оси Y

    private void Awake( )
    {
        Camera = Camera.main;
        currentDistanceStep = initialDistanceStep; // Инициализируем начальное расстояние
        nextCameraY = Camera.transform.position.y; // Начальное положение камеры
    }

    private void LateUpdate( )
    {
        if ( Player != null )
        {
            // Проверяем, достиг ли игрок уровня, когда камера должна подняться
            if ( Player.position.y > nextCameraY + followThreshold )
            {
                // Увеличиваем положение камеры на фиксированное расстояние
                nextCameraY += currentDistanceStep;

                // Увеличиваем шаг на заданный множитель
                currentDistanceStep *= distanceMultiplier;

                // Перемещаем спавнер вверх на заданное количество единиц (например, 10)
                MoveSpawnerUp ();
            }

            // Плавно перемещаем камеру к новой высоте
            Vector3 newPosition = Camera.transform.position;
            newPosition.y = Mathf.Lerp ( Camera.transform.position.y , nextCameraY , followSpeed * Time.deltaTime );

            Camera.transform.position = newPosition;
        }
    }

    // Метод для перемещения спаунера
    private void MoveSpawnerUp( )
    {
        if ( Spawner != null )
        {
            Vector3 spawnerPosition = Spawner.transform.position;
            spawnerPosition.y += spawnerMoveStep; // Увеличиваем Y координату спаунера на 10
            Spawner.transform.position = spawnerPosition;
        }
    }
}
