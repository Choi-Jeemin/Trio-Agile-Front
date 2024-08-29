using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// House 건물 관리
/// - 건물 클릭시 팝업창 나타나게 하기
/// - 유닛 구매 시 -> House에 생성 명령.
/// </summary>
public class HouseManager : MonoBehaviour
{
    public GameObject houseUI;

    [SerializeField] private Button buyPown;
    [SerializeField] private Button buyWarrior;
    [SerializeField] private Button buyArcher;

    // 사용자가 선택한 건물.
    [SerializeField]private GameObject nowHouse;

    Dictionary<GameObject, bool> buttonStatus = new Dictionary<GameObject, bool>();

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
        if (!buttonStatus.ContainsKey(house))
        {
            buttonStatus[house] = true;
        }
        this.nowHouse = house;
        OpenOrCloseUI();
    }

    /// <summary>
    /// House 및 House 메뉴판이 아닌 객체 클릭시 호출하는 함수.
    /// </summary>
    public void CloseMenu()
    {
        this.nowHouse = null;
        OpenOrCloseUI();
    }

    /// <summary>
    /// 유닛 생성 요청 함수.
    /// </summary>
    /// <param name="unit">생성할 유닛</param>
    public void BuyUnit(GameObject unit)
    {
        if (this.nowHouse == null)
            return;

        // to-do: 구매 가능 여부 로직 추가.
        bool isAvailable = true;

        if (isAvailable)
        {
            // 구매 가능한 경우, 구매 버튼 비활성화.
            buttonStatus[this.nowHouse] = false;
            OpenOrCloseUI();

            // House에 유닛 생성을 명령.
            this.nowHouse.GetComponent<HouseController>().MakeUnit(unit);
        }
    }

    /// <summary>
    /// House가 현재 대기 상태임을 알림.
    /// </summary>
    /// <param name="house">대기 상태인 House</param>
    public void AbleToBuy(GameObject house)
    {
        if (!this.buttonStatus.ContainsKey(house))
            return;
        buttonStatus[house] = true;

        if (this.nowHouse == house)
        {
            OpenOrCloseUI();
        }
    }

    /// <summary>
    /// House에 클릭 여부에 따른 UI 화면 출력 여부 결정.
    /// </summary>
    /// <param name="status">구매 Button의 활성화 여부 결정.</param>
    private void OpenOrCloseUI()
    {
        // 해당 화면에 houseUI 표시 여부.
        houseUI.SetActive(nowHouse != null);

        if (nowHouse == null)
            return;

        buyArcher.interactable = buttonStatus[nowHouse];
        buyPown.interactable = buttonStatus[nowHouse];
        buyWarrior.interactable = buttonStatus[nowHouse];
    }
}
