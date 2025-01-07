using UnityEngine;

public interface IGameMode
{
    public void CreamerLandCallback();
    public GameObject DropperInitialize(Transform transform);
}

public abstract class GameModeBase : ScriptableObject, IGameMode
{
    [SerializeField] protected GameObject _dropperPrefab;
    public abstract void CreamerLandCallback();
    public abstract GameObject DropperInitialize(Transform transform);
}