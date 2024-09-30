using UnityEngine;

public interface ITetrominoFactory :IService
{
    GameObject CreateRandomTetromino( Vector3 position , Quaternion rotation );
}