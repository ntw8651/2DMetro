using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IBaseState
{
    /*
     This Script for all enemy.
    START SETTING INFO
     */

    /*
     음... 적은 적마다 다양한 공격 방식을 갖고 있겠지...

    아 대충 하는 말이 Scriptable로 개체 속성을 지정해두면 나중에 EnemyBase같은 걸 붙였을때 개체마다 다른 스크립트를 넣을 필요 없이(기본 행동에 한해서)
    개체 속성만 뚜까뚜까 해가지고 할 수 있다 머 그런 
     
     나중에 Info는 Stat로 바꾸고 EnemyBase는 EnemyBaseBehavior정도로 바꾸면 알아먹기 더 편할듯

    이 EnemyBase도 상속으로 주는 게 맞겠지
    
     */
    [SerializeField]
    private EnemyState enemyStateOriginal;
    [HideInInspector]
    public EnemyState enemyState;

    // Base State
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

    void Start()
    {
        //initialize
        enemyState = Instantiate(enemyStateOriginal);//이것도 가져오는 걸로 바꾸자
    }

    // Update is called once per frame
    void Update()
    {

    }
}
