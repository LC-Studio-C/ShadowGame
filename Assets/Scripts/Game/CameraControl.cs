using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("跟随的目标")]
    [SerializeField]
    private GameObject targer = null;

    [Header("对于目标的偏移量")]
    [SerializeField]
    private Vector3 offset = new Vector3();

    [Header("平滑参数")]
    [Tooltip("数值越小跟随越灵敏")]
    [SerializeField]
    private float smoothTime = 0.3f;
    
    //摄像机当前移动速度
    private Vector3 velocity = Vector3.zero;
    private void Start()
    {
        if (targer == null)
        {
            targer = GameObject.Find("Player");
        }
    }

    private void LateUpdate()
    {
        //计算移动目标
        Vector3 targerPosition = targer.transform.position + offset;

        //平滑过渡摄像机位置
        transform.position = Vector3.SmoothDamp(transform.position, targerPosition, ref velocity, smoothTime);
    }
}
