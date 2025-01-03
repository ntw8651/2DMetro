using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class PlayerWeaponControl : MonoBehaviour
{

    public GameObject BackHand;
    public GameObject FrontHand;
    public GameObject player;

    private AudioSource audioSource;
    private PlayerState playerState;



    private Animator animator;
    private AnimationClip[] animationClips;


    private bool isPressAttackKeyMelee = false;
    private bool isPlayingAttackAnimation = false;

    private IEnumerator meleeAttackAnimationEndCorutine;
    private IEnumerator turnOffIsAttackingCorutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerState = player.GetComponent<PlayerState>();


        //for animation


    }

    // Update is called once per frame
    void Update()
    {
        //need in Update
        if (playerState.nowHand != null)
        {
            //rangedWeapon
            RangedWeaponReload();
            RangedWeaponTrigger();
            RangedWeaponDecreaseFirerate();
            RangedWeaponAimState();


            //meleeWeapon
            MeleeAttack();
        }


        //Set State
        

        
        

        
        

    }



    //Melee Weapon
    public void PlayerMeleeComboAnimationSet()
    {
        
        animator = player.GetComponent<Animator>();

        animationClips = playerState.nowHand.GetComponent<ItemBase>().item.meleeWeaponInfo.comboAnimations;
        AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
        

        //AnimationClipOverrides clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        //�ϴ�, �߽� �޺� ī��Ʈ �����ְ�. �ִϸ��̼� �־��ֱ����. �ٵ� �ƽ� �޺� ī��Ʈ�� �׳� ������ ȣ���ϸ� �ȵǳ� 1�� ����Ұ� �ǳ� ����Ʈ ���̹踸ŭ ���Ǵ°ű� �ѵ�...
        for (int i = 0; i< animationClips.Length; i++) 
        {
            string text = "MeleeCombo_" + (i + 1).ToString();
            aoc[text] = animationClips[i];
        }
        animator.runtimeAnimatorController = aoc;
        //playerState.meleeAnimationCount = 0;
    }
    private void MeleeAttack()
    {
        if(Input.GetMouseButtonDown(0) && playerState.nowHand.GetComponent<ItemBase>().item.type == Item.Type.MeleeWeapon)
        {
            //���Է�
            isPressAttackKeyMelee = true;
            
        }


        
        if(isPressAttackKeyMelee && !isPlayingAttackAnimation)
        {
            
            if (turnOffIsAttackingCorutine != null)
            {
                StopCoroutine(turnOffIsAttackingCorutine);
            }
            if (meleeAttackAnimationEndCorutine != null)
            {
                StopCoroutine(meleeAttackAnimationEndCorutine);
            }
            


            playerState.meleeAnimationCount += 1;
            player.GetComponent<IBaseState>().isAttacking = true;
            if(playerState.meleeAnimationCount > playerState.nowHand.GetComponent<ItemBase>().item.meleeWeaponInfo.comboAnimations.Length)
            {
                playerState.meleeAnimationCount = 1;
            }

            player.GetComponent<IBaseState>().isAttacking = true;
            player.GetComponent<Animator>().SetBool("isAttacking", true);
            player.GetComponent<Animator>().SetInteger("MeleeCombo", playerState.meleeAnimationCount);
            
            
            playerState.nowHand.GetComponent<MeleeWeaponBase>().AttackStart();
            playerState.nowHand.GetComponent<MeleeWeaponBase>().CollisionCheckStart();//�ݶ��̴� ���ֱ�
            
            //�ִϸ��̼� ������ ĵ��

            isPlayingAttackAnimation = true;
            isPressAttackKeyMelee = false;





            
            

            //���� �ִϸ��̼� �� ����
            float animationLength = animationClips[playerState.meleeAnimationCount-1].length;


            //meleeAttackAnimationEndCorutine = MeleeAttackAnimationEnd(animationLength + playerState.meleeComboDelay);//���� ���۵� �ִϸ��̼� ���̸�ŭ ��, �ִϸ��̼� �����ٰ� �����ֱ�(= �̺�Ʈ�� �����)
            //StartCoroutine(meleeAttackAnimationEndCorutine);
            

            //���� �̰� �װ� ������, 3�� ���൵ �ȵǰ� ���� �ȳ��µ� �̹� �Ѱɷ� ó���Ǵ°ž�

            //������ �� 1���� 1�� ����� �ȵƴµ� 2�� �ǰ����� ���޾� ����� �ǹ����� ���� ���� ������� 3�ִϸ��̼ǿ��� ���� �ʿ亸�� �� ���� �ð��� ��ƸԾ �׷��ſ�������?
        }
        else if (!isPlayingAttackAnimation)
        {
            //��...�ε� �ϴ��� TurnOffIsAttackng�Լ��� �����
        }
    }
    public void MeleeAttackEnd()
    {
        isPlayingAttackAnimation = false;
        turnOffIsAttackingCorutine = TurnOffIsAttacking(playerState.meleeComboWaitingTime);
        StartCoroutine(turnOffIsAttackingCorutine);//���� ���� �ð����� �� ������ �����ڼ� Ǯ��

        
        playerState.nowHand.GetComponent<MeleeWeaponBase>().CollisionCheckEnd();//�ݶ��̴� ���ֱ�
    }
    private IEnumerator MeleeAttackAnimationEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPlayingAttackAnimation = false;
        
    }
    private IEnumerator TurnOffIsAttacking(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.GetComponent<IBaseState>().isAttacking = false;//�� �̾���� �ƿ� �װų�? �޺� ���� ��ü�� �ϳ��� �ִϸ��̼����� �����־�...
        playerState.meleeAnimationCount = 0;
        player.GetComponent<Animator>().SetInteger("MeleeCombo", playerState.meleeAnimationCount);
        player.GetComponent<Animator>().SetBool("isAttacking", player.GetComponent<IBaseState>().isAttacking);

        playerState.nowHand.GetComponent<MeleeWeaponBase>().AttackEnd();//���� ���ݸ�� ����
    }



    //Ranged Weapon

    private void RangedWeaponAimState()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (playerState.nowHand.GetComponent<ItemBase>().item.type == Item.Type.RangedWeapon)
            {
                player.GetComponent<IBaseState>().isAimming = true;
            }

        }
        else if (Input.GetMouseButtonUp(1))
        {
            player.GetComponent<IBaseState>().isAimming = false;
        }
        BackHand.transform.position = playerState.nowHand.GetComponent<ItemBase>().backHand.transform.position;;
        FrontHand.transform.position = playerState.nowHand.GetComponent<ItemBase>().frontHand.transform.position;;
    }
    private void RangedWeaponDecreaseFirerate()
    {
        if (playerState.rangeFirerate > 0)
        {
            playerState.rangeFirerate -= Time.deltaTime;
        }
    }
    
    private void RangedWeaponTrigger()
    {
        if (player.GetComponent<IBaseState>().isAimming)
        {
            if (Input.GetMouseButton(0) && playerState.nowHand.GetComponent<RangedWeaponBase>().stat.canContinue)
            {
                RangedWeaponFire();
            }
            else if (Input.GetMouseButtonDown(0) && !playerState.nowHand.GetComponent<RangedWeaponBase>().stat.canContinue)
            {
                RangedWeaponFire();//�� �����غ��ϱ� �׳� ���� if�� �ְ� return���ָ� �Ǵ�...�� �ƴϱ��� �̰� ����
            }
        }
    }

    private void RangedWeaponFire()
    {
        if (playerState.rangeFirerate <= 0)
            {
                if (playerState.nowHand.GetComponent<RangedWeaponBase>().Shoot()) //fire succes
                {
                    playerState.rangeFirerate = player.GetComponent<PlayerState>().nowHand.GetComponent<RangedWeaponBase>().stat.firerate;
                }
            }

        
    }
    private void RangedWeaponReload()
    {
        //Reload need if player put a gun on hand
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerState.nowHand.GetComponent<RangedWeaponBase>().stat.ammo < playerState.nowHand.GetComponent<RangedWeaponBase>().stat.maxAmmo)
            {
                //need Motion
                playerState.nowHand.GetComponent<RangedWeaponBase>().stat.ammo = playerState.nowHand.GetComponent<RangedWeaponBase>().stat.maxAmmo;
            }
        }
    }


    //E T C
    public void PutHandOnWeapon()
    {
        if (playerState.nowHand != null)
        {
            if (playerState.nowHand.GetComponent<ItemBase>().frontHand != null)
            {
                //NEED FIX : trasnform���� ���������� �������� �Ŷ� ���� �־���� �������°� �ӵ� ���ؼ� �̵��ΰɷ� ��ü. ���ص� ū ������ ���� �ҵ�
                Vector3 FrontHandPosition = playerState.nowHand.GetComponent<ItemBase>().frontHand.transform.position;
                if (FrontHandPosition != Vector3.zero)
                {
                    FrontHand.SetActive(true);
                    FrontHand.transform.position = FrontHandPosition;
                }
            }
            else
            {
                FrontHand.SetActive(false);
            }

            if (playerState.nowHand.GetComponent<ItemBase>().frontHand != null)
            {
                Vector3 BackHandPosition = playerState.nowHand.GetComponent<ItemBase>().frontHand.transform.position;
                if (BackHandPosition != Vector3.zero)
                {
                    BackHand.SetActive(true);
                    BackHand.transform.position = BackHandPosition;
                }
            }
            else
            {
                BackHand.SetActive(false);
            }
        }
        else
        {
            FrontHand.SetActive(false);
            BackHand.SetActive(false);
        }

        
        //NEED FIX �̰� �׳� null Check�� SetActive(false)��Ű��


    }
    public void PutHandOutWeapon()
    {
        FrontHand.SetActive(false);
        BackHand.SetActive(false);
    }
}
