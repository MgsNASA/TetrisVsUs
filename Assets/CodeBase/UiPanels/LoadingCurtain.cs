using System;
using System.Collections;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    public CanvasGroup Curtain;

    public void Awake( )
    {
        gameObject.SetActive( true );
        DontDestroyOnLoad ( this );
    }

    public void Show( )
    {
        gameObject.SetActive ( true );
        Curtain.alpha = 1;
    }

    public void Hide( ) => StartCoroutine ( FadeIn () );

    private IEnumerator FadeIn( )
    {
        while ( Curtain.alpha > 0 )
        {
            Curtain.alpha -= 0.1f; // Увеличьте значение для ускорения затемнения
            yield return new WaitForSeconds ( 0.01f ); // Уменьшите интервал для более плавного и быстрого затемнения
        }
        gameObject.SetActive ( false );
    }

}
