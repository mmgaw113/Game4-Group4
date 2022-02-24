using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetButton()
    {
        Manager.ResetButton();
    }

    public void ConfirmButton()
    {
        Manager.ConfirmButton();
    }
}
