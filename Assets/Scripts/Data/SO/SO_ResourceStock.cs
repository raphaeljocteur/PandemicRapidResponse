using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SO_Resource", order = 1)]
public class SO_ResourceStock : ScriptableObject
{
    [SerializeField] RESOURCE type;
}
