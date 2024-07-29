using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        // 해당 건물 선택.
        FindAnyObjectByType<HouseManager>().ClickHouse(gameObject);
    }

    /// <summary>
    /// 요청 받은 unit을 생성.
    /// </summary>
    /// <param name="unit">생성할 유닛</param>
    public void MakeUnit(GameObject unit)
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 1;
        Instantiate(unit, spawnPosition, Quaternion.identity);
    }
}
