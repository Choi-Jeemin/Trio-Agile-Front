using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    private GameObject progressBar;
    
    // Start is called before the first frame update
    void Start()
    {
        Transform progressBarTransfrom = transform.Find("ProgressBar");

        if (progressBarTransfrom == null )
        {
            Debug.LogError(gameObject.name + " : not have Child(name : ProgressBar)");
        } else
        {
            progressBar = progressBarTransfrom.gameObject;
        }
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
        StartCoroutine(MakeUnitRoutine());
        //Vector3 spawnPosition = transform.position;
        //spawnPosition.y -= 1;
        //Instantiate(unit, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// 실제 유닛 생성 루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator MakeUnitRoutine()
    {
        // processbar 활성화
        progressBar.SetActive(true);

        //bar.transform.SetParent(GameObject.Find("Canvas").transform, false);

        yield return null;
    }
}
