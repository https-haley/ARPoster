using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections;

public class ResetAROnReturn : MonoBehaviour
{
    public ARSession arSession;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);

        if (arSession != null)
            arSession.Reset();
    }
}