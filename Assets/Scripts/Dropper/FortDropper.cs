using UnityEngine;

public class FortDropper : Dropper
{

    private GameObject _currentCreamer = null;
    [Range(1.0f, 2.0f)] public float DropperSensitivity = 1.0f;

    private void Start()
    {
        base.Start();
    }

    protected override void GameOver()
    {
        //throw new System.NotImplementedException();
    }

    protected override void ScoreEvent(int score)
    {
        //throw new System.NotImplementedException();
    }

    public void SpawnAtBase()
    {
        GameObject creamer = Instantiate(_creamerSet._creamerPrefabs[_creamerIndex], transform.position, Quaternion.identity, transform);
        _currentCreamer = creamer;
        _creamerIndex = (_creamerIndex + 1) % _creamerSet._creamerPrefabs.Count;
    }

    public void ReleaseCreamer()
    {
        _currentCreamer.transform.SetParent(null);
        _currentCreamer.GetComponent<CreamerBase>().Drop();
    }

    public void UpdatePosition(Vector3 pos)
    {
        transform.position = pos;
    }

}
