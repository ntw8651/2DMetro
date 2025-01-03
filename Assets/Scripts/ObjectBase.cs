using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour, IBaseState
{
    public string Nickname { get; set; }
    public int maxHealth { get; set; }
    public int health { get; set; }
    public bool canAttack { get; set; }
    public bool canMove { get; set; }
    public bool canJump { get; set; }
    public bool isAimming { get; set; }
    public bool isAttacking { get; set; }
    public bool isMoving { get; set; }
    public bool isJumping { get; set; }
    public bool isInBattle { get; set; }
    public AudioClip hitSFX { get; set; }
    public GameObject hitVFX { get; set; }
    public float fallingSpeedMultiply { get; set; }
    public float moveSpeedMultiply { get; set; }
    public IBaseState.Faction faction { get; set; }
    
    public void Start()
    {
        // 자기 Scriptable에서 가져오는 걸로
        Nickname = "ObjectBase";
        maxHealth = 100;
        health = 100;
        canAttack = true;
        canMove = true;
        canJump = true;
        isAimming = false;
        isAttacking = false;
        isMoving = false;
        isJumping = false;
        isInBattle = false;
        hitSFX = null;
        hitVFX = null;
        fallingSpeedMultiply = 1.0f;
        moveSpeedMultiply = 1.0f;
        faction = IBaseState.Faction.neutral;
    }
}
