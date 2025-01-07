using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "GameModes/Stack")]
public class StackGameMode : GameModeBase
{
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
