using UnityEngine;

public interface IPurchasable
{
    public abstract void Purchase();
    public int UniqueId { get; }
    public int Cost { get; }

    public abstract void AssignUniqueID(int id);

    public static readonly int DEFAULT_ID = 2;
}
