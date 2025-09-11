using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationData", menuName = "Localization/LocalizationData")]
public class DialogData : ScriptableObject
{
    [System.Serializable]
    public class LocalizedLine
    {
        public string key;
        [TextArea(3, 10)]
        public string englishText;
        [TextArea(3, 10)]
        public string thaiText;
    }

    public List<LocalizedLine> lines = new List<LocalizedLine>();
}
