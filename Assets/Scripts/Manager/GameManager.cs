using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using NavMeshSurface = NavMeshPlus.Components.NavMeshSurface;


public class GameManager : MonoBehaviour
{
    public LayerMask unitLayer; // ������ ���� ���̾�
    public LayerMask groundLayer; // ���� ���� ���̾�
    public LayerMask resourceLayer; // �ڿ��� ���� ���̾�
    private List<GameObject> selectedUnits = new List<GameObject>(); //���õ� ����
    private GameObject selelctedResource; // ���õ� �ڿ�

    public NavMeshSurface surface1; // 1�� navmesh surface
    public NavMeshSurface surface2; // 2�� navmesh surface

    void Update()
    {
        UnitSelection();
        UnitMovement();
        UpdateNavMeshSurface();
    }

    //���� ����
    void UnitSelection()
    {
        if (Input.GetMouseButtonDown(0)) // ���� Ŭ��
        {
            //���콺 ��ġ ����
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //���콺 ��ġ ��ǥ���� raycast
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, unitLayer);
            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name); // ����� �α� 
                // tag�� ������ �͸�
                if (hit.collider.CompareTag("Unit"))
                {
                    GameObject selectedUnit = hit.collider.gameObject;
                    ToggleUnitSelection(selectedUnit);
                }
            }
            else
            {
                ClearSelection();
            }
        }
    }

    //NavMesh surface�� �����ϴ� �Լ�
    void SwitchNavMeshSurface(NavMeshSurface surface)
    {
        if (surface == null)
        {
            Debug.LogError("SwitchNavMeshSurface: surface is null");
            return;
        }
        var units = FindObjectsOfType<UnitInterface>();
        foreach (var unit in units)
        {
            unit.SetNavMeshSurface(surface);
        }
    }

    //�������� ���� ������ ���̾� ���� �Լ�
    void SwitchSortingLayer(string layerName, int order)
    {
        if (layerName == null)
        {
            Debug.LogError("SwitchSortingLayer: layerName & order is null");
            return;
        }
        var units = FindObjectsOfType<UnitInterface>();
        foreach (var unit in units)
        {
            unit.SetSortingLayer(layerName, order);
        }
    }

    //� �̺�Ʈ�� �������� �� Navmesh surface�� ��ȯ������ ���ϴ� �Լ�. ���� �������� ���������� ���.
    void UpdateNavMeshSurface()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchNavMeshSurface(surface1);
            SwitchSortingLayer("Floor-0", 3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchNavMeshSurface(surface2);
            SwitchSortingLayer("Floor-1", 3);
        }
    }

    //�巡�׸� �����Ͽ� �ټ��� ������ �����ϴ� �Լ� (�̿ϼ�)
    void ToggleUnitSelection(GameObject unit)
    {
        if (selectedUnits.Contains(unit))
        {
            selectedUnits.Remove(unit);
            //SetOutlineEffect(unit, false);
        }
        else
        {
            selectedUnits.Add(unit);
            //SetOutlineEffect(unit, true);
        }
    }

    //void setoutlineeffect(gameobject unit, bool showoutline)
    //{
    //    outlineeffect outlineeffect = unit.getcomponent<outlineeffect>();
    //    if (outlineeffect != null)
    //    {
    //        outlineeffect.setoutline(showoutline);
    //    }
    //}

    // ������ ������ ���� ����.
    void ClearSelection()
    {
        foreach (GameObject unit in selectedUnits)
        {
            //SetOutlineEffect(unit, false);
        }
        selectedUnits.Clear();
    }

    // ���ֿ� ������ ��ũ��Ʈ�� �̵������ �ο��ϴ� �Լ�.
    void UnitMovement()
    {
        if (Input.GetMouseButtonDown(1)) // ������ Ŭ��
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (GameObject unit in selectedUnits)
            {
                unit.GetComponent<UnitInterface>().MoveTo(mousePosition);
            }
            ClearSelection();
        }
    }

    //�ڿ�ä�� ��� (���� ��)
    void orderResource()
    {
        if (selectedUnits != null && selectedUnits.Count > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                foreach (GameObject unit in selectedUnits)
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, resourceLayer))
                    {
                        if (hit.collider != null && hit.collider.CompareTag("Resuorce"))
                        {
                            selelctedResource = hit.collider.gameObject;
                            unit.GetComponent<UnitInterface>().getResource(selelctedResource);
                        }        
                    }
                }
            }
        }
       
    }
}
