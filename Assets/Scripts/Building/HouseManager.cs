using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// House 건물 관리
/// - 건물 클릭시 팝업창 나타나게 하기
/// </summary>
public class HouseManager : MonoBehaviour
{
    public GameObject houseUI;

    private GameObject nowHouse;

    private void Start()
    {
        OpenOrCloseUI();
    }

    /// <summary>
    /// House를 클릭시 호출하는 함수.
    /// </summary>
    /// <param name="house">현재 클릭한 House</param>
    public void ClickHouse(GameObject house)
    {
        this.nowHouse = house;
        OpenOrCloseUI();
    }

    /// <summary>
    /// House 및 House 메뉴판이 아닌 객체 클릭시 호출하는 함수.
    /// </summary>
    public void ClickOther()
    {
        this.nowHouse = null;
        OpenOrCloseUI();
    }

    /// <summary>
    /// House에 클릭 여부에 따른 UI 화면 출력 여부 결정.
    /// </summary>
    private void OpenOrCloseUI()
    {
        houseUI.SetActive(nowHouse != null);
    }
}
