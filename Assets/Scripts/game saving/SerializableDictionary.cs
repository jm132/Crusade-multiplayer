using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<Tkey, TValue> : Dictionary<Tkey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<Tkey> keys = new List<Tkey>();
    [SerializeField] private List<TValue> value = new List<TValue>();
    public void OnBeforeSerialize()
    {
        keys.Clear();
        value.Clear();

        foreach (KeyValuePair<Tkey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            value.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        Clear();

        if (keys.Count != value.Count)
        {
            Debug.LogError("KEY COUNT DOES NOT MATCH VALUE COUNT, SOMTHING IS WRONG");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            Add(keys[i], value[i]);
        }
    }
}