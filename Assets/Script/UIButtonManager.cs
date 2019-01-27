using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonManager : MonoBehaviour
{

    GameObject player;
    Player playerScript;

    public void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    public void LeftDown()
    {
        playerScript.inputLeft = true;
    }
    public void Leftup()
    {
        playerScript.inputLeft = false;
    }
    public void RightDown()
    {
        playerScript.inputRight = true;
    }
    public void RightUp()
    {
        playerScript.inputRight = false;
    }
    public void JumpClick()
    {
        if(playerScript.extraJumps !=0)
        playerScript.inputJump = true;
    }
}