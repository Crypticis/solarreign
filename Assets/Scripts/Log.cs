using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public List<string> tickerLog = new List<string>();
    public List<string> tradeLog = new List<string>();

    public void AddToTickerLog(string text)
    {
        tickerLog.Add(text);
    }

    public void AddToTradeLog(string text)
    {
        tradeLog.Add(text);
    }
}
