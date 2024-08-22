using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 프로그레스바 상태를 관리 함.
/// </summary>
public class ProgressBarController : MonoBehaviour
{
    // 해당 프로그레스바가 대기 중인지 확인용
    public bool IsIdle { get; private set; }

    // 현재 프로그레스바
    private Transform sliderTransform;
    private Slider progressbar;

    // 프로그래스바의 현재 값과 최댓 값, 프레임당 증가 값.
    private float nowValue = 0f;
    private float finalValue = 100f;
    private float fillSpeed = 1f;

    /// <summary>
    /// 프로그래스바 채워지기 시작하는 것을 요청.
    /// </summary>
    /// <param name="fillFrame">100 까지 채워지는데 걸리는 프레임 수.</param>
    public void StartWork(int fillFrame)
    {
        IsIdle = false;
        
        if (progressbar == null)
        {
            InitProgreebar();
        }

        nowValue = 0;
        finalValue = fillFrame;
        progressbar.maxValue = finalValue;

        // 실행 시작.
        StartCoroutine(DoWork());
    }

    /// <summary>
    /// 프로그래스바가 다 채웠음을 알림.
    /// </summary>
    private void EndWork()
    {
        IsIdle = true;
    }

    /// <summary>
    /// 실제 프로그래스바 채우는 작업을 수행.
    /// </summary>
    /// <returns></returns>
    IEnumerator DoWork()
    {
        // 진행도를 0으로 설정.
        progressbar.value = nowValue;

        // 프레임 당 fillSpeed 만큼 채움.
        while (nowValue < finalValue)
        {
            nowValue = Mathf.Min(nowValue + fillSpeed, finalValue);
            progressbar.value = nowValue;
            yield return null;
        }

        // 진행도 100%임을 알림.
        EndWork();
    }

    /// <summary>
    /// 프로그래스바 초기화 작업을 수행함.
    /// </summary>
    void InitProgreebar()
    {
        sliderTransform = transform.Find("Slider");

        if (sliderTransform != null)
        {
            progressbar = sliderTransform.gameObject.GetComponent<Slider>();

            if (progressbar == null)
            {
                Debug.LogError("Slider not have Slider Component.");
            }

        }
        else
        {
            Debug.LogError(gameObject.name + " : not a progreebar object.");
        }
    }
}
