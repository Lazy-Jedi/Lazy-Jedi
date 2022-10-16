using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;


public static class MonoBehaviourExtensions
{
    #region FIELDS

    #endregion

    #region METHODS

    public static void PrintWarning(this MonoBehaviour behaviour, string message)
    {
        Debug.LogWarning(message);
    }

    public static void PrintError(this MonoBehaviour behaviour, string message)
    {
        Debug.LogError(message);
    }

    public static void PrintException(this MonoBehaviour behaviour, Exception exception)
    {
        Debug.LogException(exception);
    }

    public static void ActivateGO(this MonoBehaviour behaviour)
    {
        behaviour.gameObject.Activate();
    }

    public static void DeactivateGO(this MonoBehaviour behaviour)
    {
        behaviour.gameObject.Deactivate();
    }

    public static void DeactivateGO(this MonoBehaviour behaviour, float seconds)
    {
        behaviour.StartCoroutine(behaviour.gameObject.Deactivate(seconds));
    }

    public static void DestroyGO(this MonoBehaviour behaviour)
    {
        behaviour.gameObject.Destroy();
    }

    public static void DestroyGO(this MonoBehaviour behaviour, float seconds)
    {
        behaviour.gameObject.Destroy(seconds);
    }

    public static void Activate(this MonoBehaviour behaviour)
    {
        behaviour.enabled = true;
    }

    public static void Deactivate(this MonoBehaviour behaviour)
    {
        behaviour.enabled = false;
    }

    public static void Deactivate(this MonoBehaviour behaviour, float seconds)
    {
        behaviour.StartCoroutine(behaviour.DeactivateRoutine(seconds));
    }

    private static IEnumerator DeactivateRoutine(this MonoBehaviour behaviour, float seconds)
    {
        float elapsedTime = 0f;
        while (elapsedTime <= seconds)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        behaviour.Deactivate();
    }

    public static void Destroy(this MonoBehaviour behaviour)
    {
        Object.Destroy(behaviour);
    }
    

    public static void Destroy(this MonoBehaviour behaviour, float seconds)
    {
        Object.Destroy(behaviour, seconds);
    }

    public static void SetParent(this MonoBehaviour behaviour, Transform parent)
    {
        behaviour.transform.SetParent(parent);
    }

    #endregion
}