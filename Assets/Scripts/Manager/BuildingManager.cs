using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 건축을 관리하는 클래스
/// </summary>
public class BuildingManager : MonoBehaviour
{
    [SerializeField]private BuildingTypeSO activeBuildingType;

    /// <summary>
    /// 건물을 건설할 때 마우스 왼쪽 버튼을 누르면 해당 위치에 건물을 생성
    /// </summary>
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            if(activeBuildingType != null && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 rayPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                if(CanSpawnBuilding(activeBuildingType, rayPos)){
                    Instantiate(activeBuildingType.prefab, rayPos, Quaternion.identity);    
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

    private bool CanSpawnBuilding(BuildingTypeSO buildingTypeSO, Vector3 position){
        BoxCollider2D buildingBoxCollider2D = buildingTypeSO.prefab.GetComponent<BoxCollider2D>();
        if(Physics2D.OverlapBox(position+(Vector3)buildingBoxCollider2D.offset, buildingBoxCollider2D.size, 0) != null){
            return false;
        } else {
            return true;
        }
    }
}
