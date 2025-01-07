using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyState : ScriptableObject
{
    public string displayName;

    public int meleeDamage = 1;

    public GameObject prefab;
    public Sprite sprite;
    
    
    public int maxHealth = 100;
    public AudioClip hitSFX;
    public GameObject hitVFX;
    public float fallingSpeedMultiply = 1;
    public float moveSpeedMultiply = 1;

    public bool canAttack = true;
    public bool canMove = true;
    public bool canJump = true;

}
