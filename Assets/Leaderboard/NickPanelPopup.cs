using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NickPanelPopup : MonoBehaviour
{
  public void OnClickTrigger()
  {
      Action N_Action = () => Debug.Log("On Click OK Button");
      NickNamePanel.Instance.ShowNickPanel(N_Action);
  }
}
