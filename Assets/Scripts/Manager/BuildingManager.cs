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
    [SerializeField]private GameObject popupMessage;
    [SerializeField]private TMP_Text popUpText;
    [SerializeField]private bool isEnough = true;

    private GameObject selectedUnit;
    private GameObject popupMessageInstance;
    private Transform building;
    
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

            if(activeBuildingType != null && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 rayPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                if(CanSpawnBuilding(activeBuildingType, rayPos)&&selectedUnit!=null){
            
                    if(!CheckCost()){
                        Debug.Log("Not enough resources");
                        popUpText.text = "Not enough resources";
                        popupMessageInstance = Instantiate(popupMessage, messagePosition , Quaternion.identity, GameObject.Find("Canvas").transform);
                        Destroy(popupMessageInstance, 1.5f);
                        
                        return;
                    }
            
                    selectedUnit.GetComponent<IUnit>().MoveTo(rayPos);
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
        if(Physics2D.OverlapBox(position+(Vector3)buildingBoxCollider2D.offset, buildingBoxCollider2D.size, 0) != null){
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
        // 유닛 애미메이션이 이동으로 변화하는 시간동안 대기 (Idle->Run)
        yield return new WaitForSeconds(1);
        
        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(0);
        while (!animState.IsName("Idle"))
        {
            animState = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log(animState.IsName("Idle"));
            yield return null;
        }

        animator.SetBool("Build", true);
        building = Instantiate(activeBuildingType.prefab, rayPos, Quaternion.identity);

        Animator buildingAnimator = building.GetComponent<Animator>();
        buildingAnimator.SetBool("isBuilding", true);
        
        yield return new WaitForSeconds(3);
        buildingAnimator.SetBool("isBuilding", false);
        animator.SetBool("Build", false);
    }

    /// <summary>
    /// 건물 건설 비용 체크
    /// </summary>
    private bool CheckCost()
    {
        //ㅁ자원 공용변수가 만들어지면 cost_meat, cost_gold, cost_wood 변수를 만들어서 scriptableObject에 저장
        return isEnough;
    }
}
