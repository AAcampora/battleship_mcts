using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class UCT
{
    public static double UTCValue(int totalVisit, double nodeWinScore, 
        int nodeVisit)
    {
        if (nodeVisit == 0)
        {
            return int.MaxValue;
        }
        return (nodeWinScore /(double)nodeVisit) 
            + 1.41 * Math.Sqrt((Mathf.Log(totalVisit) / (double)nodeVisit));
    }

    public static Node FindBestNodeWithUTC(Node node)
    {
        int parentVisit = node.GetState().getVisitCount();
        return node.GetChildArray().OrderByDescending
            (x => UTCValue(parentVisit, x.GetState().getWinScore(),
            x.GetState().getVisitCount())).FirstOrDefault();
    }
}
