using UnityEngine;

public static class SpawnUtility
{
    public static GameObject Spawn(
        GameObject prefab,
        Vector3 position,
        Quaternion rotation,
        Transform parent = null)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Cannot spawn: prefab is missing.");
            return null;
        }

        return Object.Instantiate(prefab, position, rotation, parent);
    }
}