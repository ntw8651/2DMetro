using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointBar : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        this.transform.GetComponent<Image>().fillAmount = (float) player.GetComponent<IBaseState>().health / (player.GetComponent<IBaseState>().maxHealth);
    }

}
