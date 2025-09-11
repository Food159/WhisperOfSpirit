using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NameData", menuName = "Localization/NameData")]
public class NameData : ScriptableObject
{
    [System.Serializable]
    public class NameLine
    {
        public string key;
        public string englishName;
        public string thaiName;
    }

    public List<NameLine> names;
}
