public interface IToolitemable
{
    public virtual void Shoot() { }
    public virtual void Shooting() { }
    public virtual void Zoom() { }
    public virtual void Recharge() { }

    public abstract void Initialize(PlayerHUD playerHUD);
    public abstract void Destroy();
}
