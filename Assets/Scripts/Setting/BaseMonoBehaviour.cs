using UnityEngine;

public class BaseMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        Debug.Log("Ham reset da dc chay");
        this.LoadComponents();
    }
    protected virtual void Start()
    {
        //For override
    }
    protected virtual void Awake()
    {
        this.LoadComponents();
        this.ResetValue();
    }
    protected virtual void LoadComponents()
    {
        // For override
    }
    protected virtual void ResetValue()
    {
        // For override
    }
    protected virtual void OnEnable()
    {
        //For override
    }
    protected virtual void OnDisable()
    {
        //For override
    }
}
