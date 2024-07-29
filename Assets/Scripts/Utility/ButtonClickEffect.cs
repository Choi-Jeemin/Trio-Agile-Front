using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 버튼 클릭 효과
/// </summary>
public class ButtonClickEffect : MonoBehaviour
{
    public Sprite clickedImage;
    private Sprite originImage;
    private Image imageComponent;

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        imageComponent = GetComponent<Image>();

        CheckInit();
        
        originImage = imageComponent.sprite;
        button.onClick.AddListener(OnButtonClick);
    }

    /// <summary>
    /// 버튼이 클릭되었을 때 실행 할 함수.
    /// </summary>
    void OnButtonClick()
    {
        StartCoroutine(ClickEffect());
    }

    /// <summary>
    /// 클릭 효과 루틴
    /// </summary>
    /// <returns>의미없음</returns>
    IEnumerator ClickEffect()
    {
        // 현재 사진을 클릭 시 사진으로 교체.
        imageComponent.sprite = clickedImage;
        Debug.Log("호출 되나?");
        yield return new WaitForSeconds(0.1f);

        // 현재 사진을 평상 시 사진으로 교체.
        imageComponent.sprite = originImage;
    }

    /// <summary>
    /// 제대로 초기화 되어 있는 지 확인 용.
    /// </summary>
    private void CheckInit()
    {
        if (imageComponent == null)
        {
            Debug.LogError(gameObject.name + " : Image component is not set.");
        }

        if (clickedImage == null)
        {
            Debug.LogError(gameObject.name + " : clickedImage is not set.");
        }

        if (button == null)
        {
            Debug.LogError(gameObject.name + " : this is not button.");
        }
    }
}
