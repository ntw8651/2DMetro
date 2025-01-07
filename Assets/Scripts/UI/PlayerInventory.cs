using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    // Start is called before the first frame update
    //생각해보니까... 인벤토리에 저장하려면... 어...어케? 그 script가 휘발되지 않나?가 아니라 오브젝트 자체를 저장하면 된댔었나
    //어떤 겜 하나 보고왔는데 이건 그거야 그 아이템에서 원거리 근접 무기만 따로 관리하는거야 확실히... 이게 뭔가 좀 모습 보여주기는 또 좋더라고 무기 세부사항 보여주기도 좋고
    public List<Item> items = new List<Item>();

    //FOR DEBUG
    public List<Item> debugAddItems = new List<Item>();
    

    void Start()
    {
        //FOR DEBUG
        foreach (var i in debugAddItems)
        {
            AddItem(i);
        }
        
    }

    public void AddItem(Item item)
    {
        Item newItem = Instantiate(item);
        if (item.type == Item.Type.RangedWeapon)
        {
            newItem.rangedWeaponInfo = Instantiate(item.rangedWeaponInfo);
        }
        else if (newItem.type == Item.Type.MeleeWeapon)
        {
            newItem.meleeWeaponInfo = Instantiate(item.meleeWeaponInfo);
        }
        else
        {
            //이미 존재하고 장비템이 아니면, 개수만 증가
            foreach (var i in items)
            {
                if (i.id == newItem.id)
                {
                    i.count++;
                    return;
                }
            }
        }
        newItem.isDropped = false;
        newItem.isEquipped = false;
        
        
        items.Add(newItem);
    }

    public void DropItem()
    {
        
    }

    public void SortItem()
    {
        items.Sort();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


}
