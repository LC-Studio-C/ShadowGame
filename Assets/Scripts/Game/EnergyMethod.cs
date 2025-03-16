using UnityEngine;

public static class EnergyMethod
{
    /// <summary>
    /// 判断当前是否满足移动条件
    /// </summary>
    /// <returns>真：满足；假：不满足</returns>
    public static bool IsMoveCondition()
    {
        if (GameControl.GetInstance().CurrentEnergyNumber <= 0f)
        {
            return false;
        }
        return true;
    }


    /// <summary>
    /// 设置减少的能量值
    /// </summary>
    /// <param name="Cost">减少的能量</param>
    public static void SetEnergyNumber(float Cost)
    {
        GameControl.GetInstance().CurrentEnergyNumber -= Cost;
    }

    /// <summary>
    /// 每个一定时间恢复能量
    /// </summary>
    /// <param name="time">计时</param>
    /// <param name="intervalTime">时间间隔</param>
    /// <param name="recoverNumber">恢复的能量值</param>
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
