using UnityEngine;

public class AssetProvider : IAssetProvider
{
    public GameObject Instantiate( string path , Vector3 position )
    {
        GameObject prefab = Resources.Load<GameObject> ( path );
        if ( prefab == null )
        {
            Debug.LogError ( $"Prefab not found at path: {path}" );
            return null;
        }
        return GameObject.Instantiate ( prefab , position , Quaternion.identity );
    }

    // Дополнительные перегрузки метода Instantiate
    public GameObject Instantiate( string path )
    {
        GameObject prefab = Resources.Load<GameObject> ( path );
        if ( prefab == null )
        {
            Debug.LogError ( $"Prefab not found at path: {path}" );
            return null;
        }
        return GameObject.Instantiate ( prefab );
    }
}
