using UnityEngine;

public interface IAssetProvider : IService
{
    public GameObject Instantiate(string Path );
    public GameObject Instantiate( string path , Vector3 at );
}