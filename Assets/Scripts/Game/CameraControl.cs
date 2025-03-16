using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("�����Ŀ��")]
    [SerializeField]
    private GameObject targer = null;

    [Header("����Ŀ���ƫ����")]
    [SerializeField]
    private Vector3 offset = new Vector3();

    [Header("ƽ������")]
    [Tooltip("��ֵԽС����Խ����")]
    [SerializeField]
    private float smoothTime = 0.3f;
    
    //�������ǰ�ƶ��ٶ�
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
        //�����ƶ�Ŀ��
        Vector3 targerPosition = targer.transform.position + offset;

        //ƽ�����������λ��
        transform.position = Vector3.SmoothDamp(transform.position, targerPosition, ref velocity, smoothTime);
    }
}
