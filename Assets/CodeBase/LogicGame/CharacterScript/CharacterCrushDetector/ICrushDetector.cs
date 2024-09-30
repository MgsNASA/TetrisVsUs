using UnityEngine;

internal interface ICrushDetector:ICharacterObserver
{
    bool CheckIfCrushed( Transform characterTransform );
}