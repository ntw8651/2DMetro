using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 그냥 이 BaseState는 모-든 오브젝트가 가지게 하자
public interface IBaseState
{
    // Start is called before the first frame update
    public string Nickname { get; set; }
    public int maxHealth { get; set; }
    public int health { get; set; }

    //대충 isAttacking은 그걸로 쓸 수 있겠지, 패링...이랄가...
    // [Header("Checker Bools")]
    public bool canAttack{ get; set; }
    public bool canMove{ get; set; }
    public bool canJump{ get; set; }
    public bool isAimming{ get; set; }
    public bool isAttacking{ get; set; }
    public bool isMoving{ get; set; }
    public bool isJumping{ get; set; }
    public bool isInBattle{ get; set; }

    public AudioClip hitSFX{ get; set; }
    public GameObject hitVFX{ get; set; }



    // [Header("ETC State")]
    public float fallingSpeedMultiply{ get; set; }
    public float moveSpeedMultiply{ get; set; }


    public enum Faction // 전투를 위한 부분
    {
        players,
        monsters,
        humans,
        hostile, //모두와 적
        allie, //모두와 친구(비전투 npc)
        neutral //아무도 아님, 공격은 가능하나, 의도적으로 공격당하지 않음. 파괴가능 자연지물
    }
    public Faction faction { get; set; }

    //음 수치감소랑 퍼센트감소 둘다 넣을까 일단 resist는 이렇게 판만 짜두고 미뤄두자

}
