using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 건축 버튼을 선택하는 UI를 관리하는 클래스
/// </summary>
public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private List<BuildingTypeSO> buildingTypeSOList;
    [SerializeField] private BuildingManager buildingManager;

    private Dictionary<BuildingTypeSO, Transform> buildingBtnDictionary;

    /// <summary>
    /// Awake : start 메서드보다 먼저 호출되는 메서드, 초기화 하기 위해 사용
    /// buildingTypeSOList에 있는 BuildingTypeSO를 가져와서 건설 버튼을 동적으로 생성
    /// </summary>
    private void Awake(){
        Transform buildingBtnTemplate = transform.Find("buildingBtnTemplate");
        buildingBtnTemplate.gameObject.SetActive(false);

        buildingBtnDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index =0;
        foreach (BuildingTypeSO buildingTypeSO in buildingTypeSOList)
        {
            Debug.Log(buildingTypeSO.name);
            Transform buildingBtnTransform = Instantiate(buildingBtnTemplate, transform);
            buildingBtnTransform.gameObject.SetActive(true);

            buildingBtnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(10 + index * 70 , 10);
            buildingBtnTransform.Find("image").GetComponent<Image>().sprite = buildingTypeSO.sprite;
            
            buildingBtnTransform.GetComponent<Button>().onClick.AddListener(() => {
                buildingManager.SetActiveBuildingType(buildingTypeSO);
                UpdateSelectedVisual();
                Debug.Log("Selected : "+buildingTypeSO.name);
            });
            
            buildingBtnDictionary[buildingTypeSO] = buildingBtnTransform;
            index++;
        }
    }

    private void Start(){
        
        UpdateSelectedVisual();
        SetBuildingDisable();
        
    }

    /// <summary>
    /// 건설 버튼 활성화
    /// </summary>
    public void SetBuildingAble(){
        foreach (BuildingTypeSO buildingTypeSO in buildingBtnDictionary.Keys)
        {
            SetActiveBtn(buildingTypeSO, true);
        }
    }

    /// <summary>
    /// 건설 버튼 비활성화
    /// </summary>
    public void SetBuildingDisable(){
        foreach (BuildingTypeSO buildingTypeSO in buildingBtnDictionary.Keys)
        {
            SetActiveBtn(buildingTypeSO, false);
            buildingBtnDictionary[buildingTypeSO].Find("selected").gameObject.SetActive(false);

        }
    }

    /// <summary>
    /// 선택된 버튼의 이미지를 업데이트
    /// </summary>
    private void UpdateSelectedVisual(){

        foreach (BuildingTypeSO buildingTypeSO in buildingBtnDictionary.Keys)
        {
            buildingBtnDictionary[buildingTypeSO].Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = buildingManager.GetActiveBuildingType();

        if(activeBuildingType != null){

            if(buildingBtnDictionary.ContainsKey(activeBuildingType)){                
                buildingBtnDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
            }
        }
    }
    
    /// <summary>
    /// 건설 버튼 활성화 여부 설정
    /// </summary>
    /// <param name="buildingTypeSO"></param>
    /// <param name="active"></param>
    private void SetActiveBtn(BuildingTypeSO buildingTypeSO, bool active){
        if(buildingBtnDictionary.ContainsKey(buildingTypeSO)){
            buildingBtnDictionary[buildingTypeSO].gameObject.SetActive(active);
        }
    }

    // /// <summary>
    // /// 건물의 비용을 반환, 0번째 인덱스에는 나무, 1번째 인덱스에는 금
    // /// </summary>
    // /// <param name="buildingTypeSO"></param>
    // /// <returns></returns>
    // public List<int> GetBuildingCost(BuildingTypeSO buildingTypeSO){
        
    //     List<int> costList = new List<int>
    //     {
    //         buildingTypeSO.woodCost,
    //         buildingTypeSO.goldCost
    //     };

    //     return costList;
    // }
}
