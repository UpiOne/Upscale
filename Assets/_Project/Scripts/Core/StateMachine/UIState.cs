public abstract class UIState
{
    protected UIManager UIManager;
    
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }

    public void SetUIManager(UIManager uiManager)
    {
        UIManager = uiManager;
    }
}