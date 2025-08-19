using UnityEngine;

public interface IPurchasable
{
    public abstract void Purchase();
    public int UniqueId { get; }
    public int Cost { get; }
}
