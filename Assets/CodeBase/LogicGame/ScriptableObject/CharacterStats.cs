using UnityEngine;

[CreateAssetMenu ( fileName = "NewCharacterStats" , menuName = "Character/Stats" )]
public class CharacterStats : ScriptableObject
{
    public float moveSpeed = 5f;
    public float jumpForce = 300f;
    public float jumpCooldown = 1f;
    public float minX = 0.4f;
    public float maxX = 9.6f;
    public bool canWalk = true;
    public bool canJump = true;
    public bool canDie = true;

}
