using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private bool isFrozen = false;
    private float previousTimeScale;
    public void FreezeTime(float duration)
    {
        if (isFrozen)
            return;

        StartCoroutine(_FreezeTime(duration));
    }

    private IEnumerator _FreezeTime(float duration)
    {
        // Freeze begins and previous scale is saved
        isFrozen = true;
        previousTimeScale = Time.timeScale; 
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(duration);

        // Freeze ends and previous scaler is set again
        Time.timeScale = previousTimeScale;
        isFrozen = false;
    }
}
