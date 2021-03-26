using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Mirror;

public class RankingList : NetworkBehaviour
{
    [SerializeField] private GameObject rankingPanel = null;
    [SerializeField] private GameObject rankingPanelPrefab = null;
    [SerializeField] private RectTransform rankingPanelList = null;
    [SyncVar]
    List<string> ranking = new List<string>();

    [Client]
    public void AddToRanking(string nick)
    {
        ranking.Add(nick);
    }

    [ClientRpc]
    public void RpcShowRanking()
    {
        ShowRanking();
    }

    [Client]
    private void ShowRanking()
    {
        rankingPanel.SetActive(true);

        for(int i = 0; i < ranking.Count; i++)
        {
            GameObject panel = Instantiate(rankingPanelPrefab);
            panel.transform.parent = rankingPanelList;

            Text textPanel = panel.GetComponentInChildren<Text>();
            textPanel.text = (i + 1) + ":  " + ranking[i];

        }
    }
}
