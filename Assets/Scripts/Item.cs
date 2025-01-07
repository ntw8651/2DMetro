using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]

public class Item : ScriptableObject
{
    //������ ������ ������ �ٴڿ� ������ �ִ� ���¸� ��������.
    public string displayName;
    public int id;
    
    public Sprite displaySprite; // inventory show simply small image
    public GameObject prefab; // throwable having collision Object
    
    public int count = 1;
    public bool isEquipped = false;
    public bool isDropped = true;

    //�����������̰� � �������̵� �� �ǰ� �صΰ� ���� ���߿� �巳�� ���� �� ��� ���̶� ������ ��~�ع����� ������ ��Ȳ �������� ������
    public enum Type
    {
        MeleeWeapon, // left click
        RangedWeapon, // right- and left click
        BothWeapon, // left right both click
        Consumable,
        
    }
    public Type type;

    public RangedWeaponInfo rangedWeaponInfo;
    public MeleeWeaponInfo meleeWeaponInfo;

    

}
