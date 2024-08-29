using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum RESOURCE_TYPE
    {
        GOLD, WOOD, MEAT
    }

    private Dictionary<RESOURCE_TYPE, int> resources;

    [SerializeField]private TMP_Text goldAmountText;
    [SerializeField]private TMP_Text woodAmountText;
    [SerializeField]private TMP_Text meatAmountText;
    

    void Start()
    {
        resources = new Dictionary<RESOURCE_TYPE, int>
        {
            { RESOURCE_TYPE.GOLD, 1000 },
            { RESOURCE_TYPE.WOOD, 1000 },
            { RESOURCE_TYPE.MEAT, 1000 }
        };
        Debug.Log("Gold: " + resources[RESOURCE_TYPE.GOLD]);
        Debug.Log("Wood: " + resources[RESOURCE_TYPE.WOOD]);
        Debug.Log("Meat: " + resources[RESOURCE_TYPE.MEAT]);
    }

    void Update()
    {
        goldAmountText.text = resources[RESOURCE_TYPE.GOLD].ToString();
        woodAmountText.text = resources[RESOURCE_TYPE.WOOD].ToString();
        meatAmountText.text = resources[RESOURCE_TYPE.MEAT].ToString();
    }
    /// <summary>
    /// 자원 획득
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    public void AddResource(RESOURCE_TYPE type, int amount)
    {
        resources[type] += amount;
    }

    /// <summary>
    /// 자원 소모
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    public void SpendResource(RESOURCE_TYPE type, int amount)
    {
        if(resources[type] < amount)
        {
            Debug.Log("ERROR : Not enough resources");
            return;
        }
        
        resources[type] -= amount;
    }

    /// <summary>
    /// 자원이 충분한지 확인
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool CheckResourceAmount(RESOURCE_TYPE type, int amount)
    {
 
        return resources[type] >= amount;

    }

    /// <summary>
    /// 자원량 반환
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetResourceAmount(RESOURCE_TYPE type)
    {
        return resources[type];
    }

    /// <summary>
    /// 자원량 설정
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    public void SetResourceAmount(RESOURCE_TYPE type, int amount)
    {
        resources[type] = amount;
    }
}
