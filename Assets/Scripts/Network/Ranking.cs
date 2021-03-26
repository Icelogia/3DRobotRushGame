using UnityEngine;
using Mirror;
using System.Collections.Generic;
using System;

public class Ranking : NetworkBehaviour
{
    private List<string> playersNameList = new List<string>();
    private bool canShowRanking = false;
    

    [Server]
    public void ServerAddToRanking(string name)
    {
        playersNameList.Remove(name);
        RpcAddToRankingList(name);
    }

    [Server]
    public void ServerAddToPlayerNamesList(string name)
    {
        playersNameList.Add(name);
        if(playersNameList.Count >= 2)
        {
            canShowRanking = true;
        }
    }

    [Server]
    public void ServerRemoveFromPlayerNamesList(string name)
    {
        playersNameList.Remove(name);
    }

    [ServerCallback]
    private void Update()
    {

        if(!canShowRanking) { return; }

        if (playersNameList.Count == 1)
        {
            RpcAddToRankingList(playersNameList[0]);
            RpcShowRankingList();
            canShowRanking = false;
            
        }
        else if (playersNameList.Count < 1)
        {
            RpcShowRankingList();
            canShowRanking = false;
        }
    }

    [Server]
    private void RpcShowRankingList()
    {
        RankingList rankingList = FindObjectOfType<RankingList>();
        rankingList.RpcShowRanking();
    }

    [ClientRpc]
    private void RpcAddToRankingList(string nick)
    {
        RankingList rankingList = FindObjectOfType<RankingList>();
        rankingList.AddToRanking(nick);
    }
}
