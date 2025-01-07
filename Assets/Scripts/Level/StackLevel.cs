using UnityEngine;

public class StackLevel : Level
{
    private StackDropper _stackDropper;
    private void Start()
    {
        base.Start();
        _stackDropper = _dropper.GetComponent<StackDropper>();
    }
    public void TapButtonPressed()
    {
        _stackDropper.TapButtonPressed();
    }
}
