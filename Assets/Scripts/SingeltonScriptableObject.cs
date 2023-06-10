using UnityEngine;

public class SingeltonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] result = Resources.FindObjectsOfTypeAll<T>();
                if (result.Length == 0)
                {
                    Debug.LogError("results length is 0 for type " + typeof(T).ToString());
                    return null;
                }
                if (result.Length > 1)
                {
                    Debug.LogError("results length >1 for type " + typeof(T).ToString());
                    return null;
                }
                _instance = result[0];
            }
            return _instance;
        }
    }
}
