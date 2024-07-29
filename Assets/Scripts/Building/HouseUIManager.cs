using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// House 건물 관리
/// - 건물 클릭시 팝업창 나타나게 하기
/// </summary>
public class HouseUIManager : MonoBehaviour
{
    public GameObject houseUI;

    public Button buyPown;
    public Button buyWarrior;
    public Button buyArcher;

    // 사용자가 선택한 건물.
    private GameObject nowHouse;

    Dictionary<GameObject, bool> buttonStatus = new Dictionary<GameObject, bool>();

    private void Start()
    {
        OpenOrCloseUI(true);
    }

    /// <summary>
    /// House를 클릭시 호출하는 함수.
    /// </summary>
    /// <param name="house">현재 클릭한 House</param>
    public void ClickHouse(GameObject house)
    {
        if (!buttonStatus.ContainsKey(house))
        {
            buttonStatus[house] = true;
        }
        this.nowHouse = house;
        //OpenOrCloseUI(buttonStatus[this.nowHouse]);
        OpenOrCloseUI(false);
    }

    /// <summary>
    /// House 및 House 메뉴판이 아닌 객체 클릭시 호출하는 함수.
    /// </summary>
    public void ClickOther()
    {
        this.nowHouse = null;
        OpenOrCloseUI(true);
    }

    /// <summary>
    /// House에 클릭 여부에 따른 UI 화면 출력 여부 결정.
    /// </summary>
    /// <param name="status">구매 Button의 활성화 여부 결정.</param>
    private void OpenOrCloseUI(bool status)
    {
        // 해당 화면에 houseUI 표시 여부.
        houseUI.SetActive(nowHouse != null);

        buyArcher.interactable = status;
        buyPown.interactable = status;
        buyWarrior.interactable = status;
    }
}
