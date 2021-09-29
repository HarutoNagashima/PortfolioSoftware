using UnityEngine;

/// <summary>
/// UIŒn‚Ìˆ—‚ÌŠî’êƒNƒ‰ƒX
/// </summary>
public abstract class UIBase
{
    protected GameObject uiObject;

    public virtual void Init(GameObject uiObject)
    {
        this.uiObject = uiObject;
    }

    public virtual void ManagedUpdate(float dt) { }

    public virtual void Terminate()
    {
        GameObject.Destroy(uiObject);
        uiObject = null;
    }
}
