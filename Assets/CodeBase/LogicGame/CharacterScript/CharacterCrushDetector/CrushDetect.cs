using UnityEngine;

public class CrushDetector : MonoBehaviour, ICrushDetector
{
    public float raycastLength = 1f;
    public LayerMask platformLayer;
    public Color rayColor = Color.red;
    public Vector3 raycastMargin = Vector3.zero;

    public bool CheckIfCrushed( Transform transform )
    {
        Vector3 rayStartPos = transform.position + raycastMargin;
        RaycastHit2D hit = Physics2D.Raycast ( rayStartPos , Vector2.up , raycastLength , platformLayer );

        if ( hit.collider != null )
        {
            RaycastHit2D groundHit = Physics2D.Raycast ( rayStartPos , Vector2.down , raycastLength , platformLayer );

            if ( groundHit.collider != null )
            {
                return true; // Персонаж зажат
            }
        }
        return false; // Персонаж не раздавлен
    }

    public void OnCharacterDataChanged( CharacterStats stats )
    {
       
    }

    private void OnDrawGizmos( )
    {
        Gizmos.color = rayColor;
        Vector3 rayStartPos = transform.position + raycastMargin;

        Gizmos.DrawLine ( rayStartPos , rayStartPos + Vector3.up * raycastLength );
        Gizmos.DrawLine ( rayStartPos , rayStartPos + Vector3.down * raycastLength );
    }
}
