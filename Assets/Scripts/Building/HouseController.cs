using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    private GameObject progressBar;

    // 현재 생성 중인 유닛
    private GameObject nowMake;

    // 각 유닛 생성 시간 : 프레임 개수
    private int makePown = 1000;
    private int makeWarrior = 2000;
    private int makeArchor = 3000;
    
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
        if (nowMake != null)
            return;

        nowMake = unit;
        StartCoroutine(MakeUnitRoutine());
    }

    /// <summary>
    /// 실제 유닛 생성 루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator MakeUnitRoutine()
    {
        // processbar 활성화
        progressBar.SetActive(true);

        // progressbar에 유닛 생성 요청.
        ProgressBarController progressBarController = progressBar.GetComponent<ProgressBarController>();

        progressBarController.StartWork(makePown);

        // progressbar 진행도 100%까지 대기.
        while (!progressBarController.IsIdle) 
            yield return null;

        // progressbar 비활성.
        progressBar.SetActive(false);

        // 유닛 생성
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 1;
        Instantiate(nowMake, spawnPosition, Quaternion.identity);
        nowMake = null;

        // 해당 건물 생성을 할 수 있는 상태를 알림.
        FindObjectOfType<HouseManager>().AbleToBuy(gameObject);
    }
}
