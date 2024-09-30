using UnityEngine;

public interface IBlockRotator :IService
{
    void Rotate( Transform tetrisBlock , Vector3 rotationPoint );
}
