using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreamerSet")]
public class CreamerSet : ScriptableObject
{
    public List<GameObject> _creamerPrefabs;
    public int _cost;
}
