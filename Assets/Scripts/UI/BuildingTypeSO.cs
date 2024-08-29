using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BuildingTypeSO : ScriptableObject{

    public Transform prefab;
    public Sprite sprite;   
    public int woodCost;
    public int goldCost;
    
}