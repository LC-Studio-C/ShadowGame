using UnityEngine;

public static class EnergyMethod
{
    /// <summary>
    /// �жϵ�ǰ�Ƿ������ƶ�����
    /// </summary>
    /// <returns>�棺���㣻�٣�������</returns>
    public static bool IsMoveCondition()
    {
        if (GameControl.GetInstance().CurrentEnergyNumber <= 0f)
        {
            return false;
        }
        return true;
    }


    /// <summary>
    /// ���ü��ٵ�����ֵ
    /// </summary>
    /// <param name="Cost">���ٵ�����</param>
    public static void SetEnergyNumber(float Cost)
    {
        GameControl.GetInstance().CurrentEnergyNumber -= Cost;
    }

    /// <summary>
    /// ÿ��һ��ʱ��ָ�����
    /// </summary>
    /// <param name="time">��ʱ</param>
    /// <param name="intervalTime">ʱ����</param>
    /// <param name="recoverNumber">�ָ�������ֵ</param>
    public static void SetEnergyRecoverNumber(ref float time,float intervalTime,float recoverNumber)
    {
        time += Time.deltaTime;
        if (time > intervalTime)
        {
            GameControl.GetInstance().CurrentEnergyNumber += recoverNumber;
            time = 0f;
        }
    }
}
