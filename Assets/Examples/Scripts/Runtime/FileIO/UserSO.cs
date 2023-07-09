using UnityEngine;

namespace LazyJedi.Examples
{
    [CreateAssetMenu(fileName = "User", menuName = "ScriptableObjects/User", order = 0)]
    public class UserSO : ScriptableObject
    {
        [SerializeField]
        public User User;
    }
}