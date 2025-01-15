using UnityEngine;

[CreateAssetMenu(menuName = "GameModes/Fort")]
public class FortGameMode : GameModeBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // TODO: this has to change
    private readonly int _scoreValue = 1;
    public override void CreamerLandCallback()
    {
        ScoreManager.Instance.ScoreEvent.Invoke(_scoreValue);
    }

    public override GameObject DropperInitialize(Transform transform)
    {
        return GameObject.Instantiate(_dropperPrefab, transform);
    }
}
