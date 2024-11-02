using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {
    private void Start()
    {
        Debug.Log("GameHandler.Start");

        int count = 0;
        FunctionPeriodic.Create(() =>
        {
            CMDebug.TextPopupMouse("Ding! " + count);
            count++;
        }, .300f);
    }
}
