using System.Collections;
using UnityEngine;

public interface IBlockMover :IService
{
    IEnumerator Move( Vector3 direction , Transform tetrisBlock );
    bool ValidMove( Transform tetrisBlock ,Vector3 direction); // Добавлено
}
