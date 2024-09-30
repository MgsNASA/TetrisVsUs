using UnityEngine;

public interface IBlockMover :IService
{
    void Move( Vector3 direction , Transform tetrisBlock );
    bool ValidMove( Transform tetrisBlock ); // Добавлено
}
