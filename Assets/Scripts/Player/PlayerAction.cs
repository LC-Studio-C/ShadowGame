using UnityEngine;

public static class PlayerAction
{
    public static bool MoveInput(ref PlayerMoveDirection playerMoveDirect, PlayerState playerState, ref bool isForward)
    {
        if (playerState == PlayerState.NormalMove)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerMoveDirect = PlayerMoveDirection.Left;
                return true;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                playerMoveDirect = PlayerMoveDirection.Forward;
                return true;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                playerMoveDirect = PlayerMoveDirection.Right;
                return true;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                playerMoveDirect = PlayerMoveDirection.Backward;
                return true;
            }
        }

        if (playerState == PlayerState.LockMove)
        {
            switch (playerMoveDirect)
            {
                case PlayerMoveDirection.Forward:
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        isForward = true;
                        return true;
                    }
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        isForward = false;
                        return true;
                    }
                    break;
                case PlayerMoveDirection.Backward:
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        isForward = false;
                        return true;
                    }
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        isForward = true;
                        return true;
                    }
                    break;
                case PlayerMoveDirection.Right:
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        isForward = false;
                        return true;
                    }
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        isForward = true;
                        return true;
                    }
                    break;
                case PlayerMoveDirection.Left:
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        isForward = true;
                        return true;
                    }
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        isForward = false;
                        return true;
                    }
                    break;
            }
        }
        return false;
    }

    public static void PlayerMove(PlayerMoveDirection playerMoveDirect, PlayerState playerState, float speed, GameObject player, GameObject blockJudgeTrigger, bool isBlock, bool isForward, bool isBackBlock)
    {
        Vector3 playerMoveForward = Vector3.zero;
        Vector3 playerRotationForward = Vector3.zero;

        switch (playerMoveDirect)
        {
            case PlayerMoveDirection.Forward:
                playerMoveForward = Vector3.forward;
                playerRotationForward = new Vector3(0, 0, 0);
                break;
            case PlayerMoveDirection.Left:
                playerMoveForward = Vector3.left;
                playerRotationForward = new Vector3(0, -90, 0);
                break;
            case PlayerMoveDirection.Backward:
                playerMoveForward = Vector3.back;
                playerRotationForward = new Vector3(0, -180, 0);
                break;
            case PlayerMoveDirection.Right:
                playerMoveForward = Vector3.right;
                playerRotationForward = new Vector3(0, -270, 0);
                break;
        }

        if (playerState == PlayerState.NormalMove)
        {
            player.transform.rotation = Quaternion.Euler(playerRotationForward);
            if (!isBlock)
            {
                player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().MoveClip;
                player.GetComponent<AudioSource>().volume = 1f;
                player.GetComponent<AudioSource>().Play();
                Move(player.transform, playerMoveForward, speed);
            }
            else
            {
                player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().NoMoveableClip;
                player.GetComponent<AudioSource>().volume = 0.5f;
                player.GetComponent<AudioSource>().Play();
            }
        }
        if (playerState == PlayerState.LockMove)
        {
            if (isForward)
            {
                if (!isBlock)
                {
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().MoveClip;
                    player.GetComponent<AudioSource>().volume = 1f;
                    player.GetComponent<AudioSource>().Play();
                    Move(player.transform, playerMoveForward, speed);
                }
            }
            if (!isForward)
            {
                if (!isBackBlock)
                {
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().MoveClip;
                    player.GetComponent<AudioSource>().volume = 1f;
                    player.GetComponent<AudioSource>().Play();
                    Move(player.transform, -playerMoveForward, speed);
                }
            }
        }

        BlockJudgeTriggerMove(blockJudgeTrigger, player);
    }

    public static bool MoveInterval(ref float time, float moveIntervalTime)
    {
        time += Time.deltaTime;
        if (time >= moveIntervalTime)
        {
            return true;
        }
        return false;
    }

    //障碍物信息传递
    public static void TriggerInfoTranslate(GameObject blockJudgeTrigger, ref bool[] isWitchBlock)
    {
        PlayerBlockJudgeTrigger[] triggerList = blockJudgeTrigger.GetComponentsInChildren<PlayerBlockJudgeTrigger>();
        for (int i = 0; i < triggerList.Length; i++)
        {
            isWitchBlock[i] = triggerList[i].isBlock;
        }
    }

    //判断玩家当前移动方向有无障碍物
    public static bool IsBlockJudgeing(PlayerMoveDirection playerMoveDirection, PlayerState playerState, ref Trigger willTriger, ref bool isLock, ref bool isBackBlock, bool[] isWitchBlock)
    {
        if (playerState == PlayerState.LockMove)
        {
            isLock = true;
        }
        else
        {
            isLock = false;
        }

        int triggerDirect = 0;
        int backTrigger = 0;
        switch (playerMoveDirection)
        {
            case PlayerMoveDirection.Forward:
                willTriger = Trigger.right;
                backTrigger = 2;
                triggerDirect = 3;
                break;
            case PlayerMoveDirection.Backward:
                willTriger = Trigger.left;
                backTrigger = 3;
                triggerDirect = 2;
                break;
            case PlayerMoveDirection.Left:
                willTriger = Trigger.up;
                backTrigger = 1;
                triggerDirect = 0;
                break;
            case PlayerMoveDirection.Right:
                willTriger = Trigger.down;
                backTrigger = 0;
                triggerDirect = 1;
                break;
        }
        isBackBlock = isWitchBlock[backTrigger];
        return isWitchBlock[triggerDirect];
    }

    //判断玩家周围有无障碍物
    public static void IsAroundBlockJudgeing(bool[] isWitchBlock, ref bool isUpBlock, ref bool isDownBlock, ref bool isLeftBlock, ref bool isRightBlock)
    {
        isUpBlock = isWitchBlock[0];
        isDownBlock = isWitchBlock[1];
        isLeftBlock = isWitchBlock[2];
        isRightBlock = isWitchBlock[3];
    }

    private static void Move(Transform transform, Vector3 moveDirect, float speed)
    {
        transform.Translate(moveDirect * speed, Space.World);
    }

    //玩家四周碰撞盒和玩家移动同步
    private static void BlockJudgeTriggerMove(GameObject blockJudgeTrigger, GameObject player)
    {
        blockJudgeTrigger.transform.position = player.transform.position;
    }
}
