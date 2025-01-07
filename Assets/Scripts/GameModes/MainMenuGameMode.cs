using UnityEngine;

[CreateAssetMenu(menuName = "GameModes/Main Menu")]
public class MainMenuGameMode : GameModeBase
{
    public override void CreamerLandCallback()
    {
    }

    public override GameObject DropperInitialize(Transform transform)
    {
        return null;
    }
}
