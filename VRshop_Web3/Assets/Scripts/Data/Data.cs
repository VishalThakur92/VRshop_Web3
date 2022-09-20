using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Data
{
    #region Product Interaction Events
    public struct DataEvents
    {
        public static UnityAction<GameObject> OnProductRepositionStart;
        public static UnityAction OnProductRepositionEnd;
    }
    #endregion
}
