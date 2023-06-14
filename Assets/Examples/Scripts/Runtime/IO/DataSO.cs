using UnityEngine;

namespace LazyJedi.Examples
{
    [CreateAssetMenu(fileName = "DataSO", menuName = "ScriptableObjects/IODataSO", order = 90)]
    public class DataSO : ScriptableObject
    {
        public string ID;
        public string Name;
    }
}