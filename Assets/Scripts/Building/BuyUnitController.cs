using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyUnitController : MonoBehaviour
{
    // 생성한 유닛 Prefab 정보.
    public GameObject createUnit;

    private HouseManager UIManager;
    // Start is called before the first frame update
    void Start()
    {
        UIManager = FindAnyObjectByType<HouseManager>();

        GetComponent<Button>().onClick.AddListener(() =>
        {
            UIManager.BuyUnit(createUnit);
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
