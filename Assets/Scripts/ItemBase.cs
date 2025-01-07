using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour, IInteraction
{
    public Item itemOrigin;
    public Item item;

    public GameObject frontHand;
    public GameObject backHand;

    public IInteraction.InteractType type => IInteraction.InteractType.Pickup;
    //무기를 꺼낼 때, 이 아이템에서 할당해주면 되는 거 아닌가? 값 자체는 ItemBase에서 갖고 있고... 만약 손에 있다면 오브젝트로 관리하지만,
    /*
     이걸 가방에 넣으면 info든 뭐든 다 넣어주는거지 다시 아이템에 아니 굳이 이걸 다시 넣을때 반영해줄 필요도 없지 않나. 그냥 인벤토리에 있는 정보로 GetComponent해서 쓰면 바뀌어도 반영되잖아
    어..!! 그리고 오브젝트에다가 어 어어아ㅏ아 하려던거 생각 아 휘발성 왤케 큰거야!!!!!!!!
    아무튼 아에 아이템에 저장해도 될듯??
     음... 그럼 이것도 결국 오브젝트로 관리해야 할 듯 그게 내가 보기 편할 것 같아
    어...아닌가 그냥... 아에... 하나로 관리...
    아니 지금 생각해보면, ItemBase는 아이템이잖아. 바닥에 떨어져져있는 아이템한테 붙여주는 스크립트라고.
     */
    /*
     * 이거 아이템 요소에서 다른 건 그냥 Item에서 공유하고
     * 개수에 대한 것만 ItemBase 자체에서 처리하자.
     * 
     */
    public void Interact(GameObject player)
    {
        /*
         * 1. 플레이어 인벤토리 확인 -> 근데 일단 인벤 무제한을 염두로 뒀으니까 그냥 넣기
         * 2. 넣을 수 있다면
         * 3. 넣는다
         * 4. 아이템을 삭제한다
         */
    } 
    void Start()
    {
        if (item == null)
        {
            item = Instantiate(itemOrigin);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
