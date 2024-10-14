    using UnityEngine;
    using System.Collections;

    public class CameraController : MonoBehaviour, ICameraController, IStateClass
    {
        [SerializeField] private float initialDistanceStep = 10.0f; // Движение каждые 10 единиц
        [SerializeField] private float followSpeed = 2.0f;
        [SerializeField] private float distanceMultiplier = 20f;
        [SerializeField] private float shakeDuration = 0.5f; // Длительность дрожания
        [SerializeField] private float shakeMagnitude = 0.2f; // Сила дрожания
        [SerializeField] private AudioClip shakeSound; // Звук тряски камеры
        [SerializeField] private AudioSource audioSource; // Источник звука

        private Spawner spawnerController;
        private Camera mainCamera;
        private float nextCameraY;
        private Vector3 initialCameraPosition; // Хранит начальную позицию камеры
        private Coroutine shakeCoroutine; // Хранит ссылку на корутину дрожания
        private Coroutine moveCoroutine; // Хранит ссылку на корутину движения камеры

        private void Awake( )
        {
            mainCamera = Camera.main;
            initialCameraPosition = mainCamera.transform.position; // Сохраняем начальную позицию
            nextCameraY = initialCameraPosition.y;
            spawnerController = Spawner.Instance.GetComponent<Spawner> ();

            // Проверяем и добавляем AudioSource, если его нет
            if ( audioSource == null )
            {
                audioSource = gameObject.AddComponent<AudioSource> ();
            }
    
        }


        private void MoveCameraUp( int newThreshold )
        {
            // Добавим лог для проверки значений
            Debug.Log ( $"New Threshold Reached: {newThreshold}, Current Next Y: {nextCameraY}" );

            if ( newThreshold <= nextCameraY )
                return; // Избегайте ненужных вызовов, если цель уже выше

            nextCameraY = newThreshold;
            spawnerController.MoveSpawnerUp ( newThreshold );
            initialDistanceStep += distanceMultiplier;

            // Остановка и перезапуск корутины плавного движения камеры
            if ( moveCoroutine != null )
            {
                StopCoroutine ( moveCoroutine );
            }
            moveCoroutine = StartCoroutine ( MoveCameraSmoothly () );

            // Дрожание камеры
            TriggerCameraShake ( shakeDuration , shakeMagnitude );
        }



        private IEnumerator MoveCameraSmoothly( )
        {
            while ( Mathf.Abs ( mainCamera.transform.position.y - nextCameraY ) > 0.01f )
            {
                Vector3 newPosition = mainCamera.transform.position;
                newPosition.y = Mathf.Lerp ( mainCamera.transform.position.y , nextCameraY , followSpeed * Time.deltaTime );
                mainCamera.transform.position = newPosition;
                yield return null; // Ждем следующий кадр
            }

            mainCamera.transform.position = new Vector3 ( mainCamera.transform.position.x , nextCameraY , mainCamera.transform.position.z );
            moveCoroutine = null;
        }

        public void TriggerCameraShake( float duration , float magnitude )
        {
            if ( shakeCoroutine != null )
            {
                StopCoroutine ( shakeCoroutine ); // Остановка текущей корутины, если она уже запущена
            }

            // Проигрываем звук тряски, если аудиоклип и источник звука заданы
            if ( shakeSound != null && audioSource != null )
            {
                audioSource.PlayOneShot ( shakeSound );
            }

            shakeCoroutine = StartCoroutine ( CameraShakeCoroutine ( duration , magnitude ) ); // Запуск новой корутины с параметрами
        }

    private IEnumerator CameraShakeCoroutine( float duration , float magnitude )
    {
        float elapsed = 0.0f;
        Vector3 originalPosition = mainCamera.transform.position; // Сохраняем оригинальную позицию

        while ( elapsed < duration )
        {
            Vector3 shakeOffset = Random.insideUnitSphere * magnitude;
            shakeOffset.z = 0; // Оставляем смещение только по осям X и Y
            mainCamera.transform.position = originalPosition + shakeOffset; // Применяем смещение к оригинальной позиции

            elapsed += Time.deltaTime;
            yield return null; // Ждем до следующего кадра
        }

        mainCamera.transform.position = originalPosition; // Возвращаем камеру в оригинальную позицию
        shakeCoroutine = null; // Очистить ссылку на корутину
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
            if ( shakeCoroutine != null )
            {
                StopCoroutine ( shakeCoroutine ); // Остановка дрожания при перезапуске
            }
            if ( moveCoroutine != null )
            {
                StopCoroutine ( moveCoroutine ); // Остановка плавного движения при перезапуске
            }
            mainCamera.transform.position = initialCameraPosition; // Возвращаем камеру на начальную позицию
            nextCameraY = initialCameraPosition.y; // Обновляем целевую позицию камеры
            initialDistanceStep = 10.0f; // Сбрасываем шаг движения
        }

    internal void Initialize( )
    {
        throw new System.NotImplementedException ();
    }

    internal void Initialize( VerticalMovementTracker verticalMovementTracker )
    {

        VerticalMovementTracker.OnThresholdReached += MoveCameraUp;
    }
}
