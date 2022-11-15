using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerItem : MonoBehaviour
{
    public Text PlayerName;
    public void setPlayerName(Player player)
    {
        PlayerName.text = player.NickName;
    }
}
