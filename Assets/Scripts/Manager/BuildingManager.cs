using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 건축을 관리하는 클래스
/// </summary>
public class BuildingManager : MonoBehaviour
{
    [SerializeField]private BuildingTypeSO activeBuildingType;
    [SerializeField]private GameManager gameManager;
    [SerializeField]private ResourceManager resourceManager;
    [SerializeField]private GameObject popupMessage;
    [SerializeField]private TMP_Text popUpText;
    [SerializeField]private bool isEnough = true;
    [SerializeField]private LayerMask layerMask;
    [SerializeField]private BuildingTypeSelectUI buildingTypeSelectUI;
    
    private GameObject selectedUnit;
    private GameObject popupMessageInstance;
    private Transform building;
    private bool isBusy = false;
    private Vector3 messagePosition = Vector3.zero;

    /// <summary>
    /// 건물을 건설할 때 마우스 왼쪽 버튼을 누르면 해당 위치에 건물을 생성
    /// </summary>
    private void Update()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        selectedUnit = gameManager.getSelectedUnitOnly();
        
        if(Input.GetMouseButtonDown(0))
        {
            if(activeBuildingType != null && !EventSystem.current.IsPointerOverGameObject() && !isBusy)
            {
                Vector3 rayPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

                if(CanSpawnBuilding(activeBuildingType, rayPos)&&selectedUnit!=null){
                    Debug.Log(CheckCost());
                    if(!CheckCost()){
                        
                        //건물 선택 해제
                        SetActiveBuildingType(null);
                        //건설 가능 상태로 변경
                        SetBusy(false);
                        //유닛 선택 해제
                        gameManager.ClearSelection();
                        
                        Debug.Log("Not enough resources");
                        popUpText.text = "Not enough resources";
                        popupMessageInstance = Instantiate(popupMessage, messagePosition, Quaternion.identity, GameObject.Find("Canvas").transform);
                        Destroy(popupMessageInstance, 1.5f);
                        
                        return;
                    }

                    // TODO : GameManger에서 객체 이동을 함수화 후 실행.
                    SetBusy(true);
                    //자원 소모
                    resourceManager.SpendResource(ResourceManager.RESOURCE_TYPE.WOOD, activeBuildingType.woodCost);
                    resourceManager.SpendResource(ResourceManager.RESOURCE_TYPE.GOLD, activeBuildingType.goldCost);
                    selectedUnit.GetComponent<IUnit>().MoveToSame(rayPos);
                    Animator unitAnimator = selectedUnit.GetComponent<Animator>();
                    StartCoroutine(BuildAfterMoving(unitAnimator, rayPos));    
                }

            }
            
        }
    }

    /// <summary>
    /// activeBuildingType을 선택된 건물로 설정
    /// </summary>
    /// <param name="buildingTypeSO"></param>
    public void SetActiveBuildingType(BuildingTypeSO buildingTypeSO)
    {
        activeBuildingType = buildingTypeSO;
    }

    /// <summary>
    /// 현재 선택된 건물을 반환
    /// </summary>
    /// <returns>activeBuildingType</returns>
    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    /// <summary>
    /// 건물을 생성할 수 있는지 확인
    /// </summary>
    /// <param name="buildingTypeSO"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool CanSpawnBuilding(BuildingTypeSO buildingTypeSO, Vector3 position){
        BoxCollider2D buildingBoxCollider2D = buildingTypeSO.prefab.GetComponent<BoxCollider2D>();
        if(Physics2D.OverlapBox(position+(Vector3)buildingBoxCollider2D.offset, buildingBoxCollider2D.size, 0, layerMask) != null){
            return false;
        } else {
            return true;
        }
    }

    /// <summary>  
    /// 유닛이 이동하는동안 대기
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    private IEnumerator BuildAfterMoving(Animator animator, Vector3 rayPos)
    {
        // 유닛 애니메이션이 이동으로 변화하는 시간동안 대기 (Idle->Run)
        yield return new WaitForSeconds(1);
        
        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(0);
        while (!animState.IsName("Idle"))
        {
            animState = animator.GetCurrentAnimatorStateInfo(0);
            //Debug.Log(animState.IsName("Idle"));
            yield return null;
        }

        animator.SetBool("Build", true);
        building = Instantiate(activeBuildingType.prefab, rayPos, Quaternion.identity);

        Animator buildingAnimator = building.GetComponent<Animator>();
        buildingAnimator.SetBool("isBuilding", true);
        
        yield return new WaitForSeconds(2.5f);
        buildingAnimator.SetBool("isBuilding", false);
        animator.SetBool("Build", false);
        
        //건물 선택 해제
        SetActiveBuildingType(null);
        //건설 가능 상태로 변경
        SetBusy(false);
        //유닛 선택 해제
        gameManager.ClearSelection();
    }

    /// <summary>
    /// 건물 건설 비용 체크
    /// </summary>
    private bool CheckCost()
    {

        isEnough = true;

        if(!resourceManager.CheckResourceAmount(ResourceManager.RESOURCE_TYPE.WOOD, activeBuildingType.woodCost)){
            Debug.Log("Not enough wood");
            isEnough = false;
        }
        
        if(!resourceManager.CheckResourceAmount(ResourceManager.RESOURCE_TYPE.GOLD, activeBuildingType.goldCost)){
            Debug.Log("Not enough gold");
            isEnough = false;
        }

        return isEnough;
    }

    /// <summary>
    /// 건설 중인지 확인
    /// </summary>
    private void SetBusy(bool isBusy)
    {
        this.isBusy = isBusy;
    }
}
