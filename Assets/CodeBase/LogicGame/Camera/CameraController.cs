using UnityEngine;

public class CameraController : MonoBehaviour, ICameraController, IStateClass
{
    [SerializeField] private float initialDistanceStep = 10.0f; // Движение каждые 10 единиц
    [SerializeField] private float followSpeed = 2.0f;
    [SerializeField] private float distanceMultiplier = 1.2f;

    private Spawner spawnerController;
    private Camera mainCamera;
    private float nextCameraY;
    private Vector3 initialCameraPosition; // Хранит начальную позицию камеры

    private void Awake( )
    {
        mainCamera = Camera.main;
        initialCameraPosition = mainCamera.transform.position; // Сохраняем начальную позицию
        nextCameraY = initialCameraPosition.y;
        spawnerController = Spawner.Instance.GetComponent<Spawner> ();

        VerticalMovementTracker verticalMovementTracker = FindObjectOfType<VerticalMovementTracker> ();
        if ( verticalMovementTracker != null )
        {
            VerticalMovementTracker.OnThresholdReached += MoveCameraUp;
        }
        else
        {
            Debug.LogError ( "VerticalMovementTracker не найден в сцене!" );
        }
    }

    private void OnDestroy( )
    {
        VerticalMovementTracker.OnThresholdReached -= MoveCameraUp;
    }

    private void MoveCameraUp( int newThreshold )
    {
        nextCameraY = newThreshold;
        spawnerController.MoveSpawnerUp ( initialDistanceStep );
        initialDistanceStep *= distanceMultiplier;
    }

    private void Update( )
    {
        Vector3 newPosition = mainCamera.transform.position;
        newPosition.y = Mathf.Lerp ( mainCamera.transform.position.y , nextCameraY , followSpeed * Time.deltaTime );
        mainCamera.transform.position = newPosition;
    }

    public void FollowTarget( Transform target )
    {
        // Реализация, если потребуется
    }

    public void StartClass( )
    {
        // Дополнительная инициализация, если потребуется
    }

    public void Pause( )
    {
        // Логика паузы, если потребуется
    }

    public void Resume( )
    {
        // Логика возобновления, если потребуется
    }

    public void Restart( )
    {
        mainCamera.transform.position = initialCameraPosition; // Возвращаем камеру на начальную позицию
        nextCameraY = initialCameraPosition.y; // Обновляем целевую позицию камеры
        initialDistanceStep = 10.0f; // Сбрасываем шаг движения
    }
}
