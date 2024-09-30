using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private readonly ICoroutineRunner _coroutineRunner;
    public SceneLoader( ICoroutineRunner coroutineRunner ) => _coroutineRunner = coroutineRunner;

    public void Load( string name , Action onLoaded = null )
    {
        _coroutineRunner.StartCoroutine ( LoadScene ( name , onLoaded ) ); // Передаём коллбэк
    }

    public IEnumerator LoadScene( string nextScene , Action onLoaded = null )
    {
        Debug.Log ( $"Attempting to load scene: {nextScene}" );

        // Проверяем, не загружена ли уже нужная сцена
        if ( SceneManager.GetActiveScene ().name == nextScene )
        {
            Debug.Log ( "Already in the target scene." );
            onLoaded?.Invoke (); // Вызываем коллбэк, если он существует
            yield break;
        }

        // Запускаем асинхронную загрузку сцены
        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync ( nextScene );

        // Ожидаем завершения загрузки
        while ( !waitNextScene.isDone )
        {
            Debug.Log ( $"Loading {nextScene}... Progress: {waitNextScene.progress * 100}%" );
            yield return null;
        }

        // Убедимся, что сцена загружена полностью
        Debug.Log ( $"Scene {nextScene} loaded successfully." );

        // Убедитесь, что сцена активна
        Scene loadedScene = SceneManager.GetSceneByName ( nextScene );
        if ( loadedScene.IsValid () && loadedScene.isLoaded )
        {
            Debug.Log ( $"Scene {nextScene} is now the active scene." );
            SceneManager.SetActiveScene ( loadedScene ); // Делаем её активной
        }

        // Вызываем коллбэк, если он существует
        onLoaded?.Invoke ();
    }
}
