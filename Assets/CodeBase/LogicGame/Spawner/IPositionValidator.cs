using UnityEngine;

public interface IPositionValidator:IService
{
    bool ValidMove( Transform tetrominoTransform , Vector3 direction );
}
