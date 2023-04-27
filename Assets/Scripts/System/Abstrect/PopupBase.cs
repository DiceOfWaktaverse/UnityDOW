using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public abstract class PopupBase : MonoBehaviour, IPopup
    {
        abstract public int GetOrder();
        abstract public void Initialize();
        abstract public void Refresh();
        abstract public void ClosePopup();
        abstract public void Close();
        abstract public void SetActive(bool v);
    }
}