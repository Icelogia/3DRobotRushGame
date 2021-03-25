using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class Ranking : NetworkBehaviour
{
    private Stack<string> playersNamesToRanking = new Stack<string>();

    [Server]
    public void ServerAddToRanking(string name)
    {
        playersNamesToRanking.Push(name);
    }
}
