using System;
using UnityEngine;

namespace LazyJedi.Guid
{
    [Serializable]
    public class SerializableGuid
    {
        [SerializeField, HideInInspector]
        private string _guid;

        public string Guid
        {
            get => _guid;
        }

        public SerializableGuid()
        {
            _guid = System.Guid.NewGuid().ToString();
        }

        public SerializableGuid(string guid)
        {
            _guid = guid;
        }

        public override string ToString()
        {
            return _guid;
        }

        public static implicit operator string(SerializableGuid guid)
        {
            return guid._guid;
        }

        public static implicit operator SerializableGuid(string guid)
        {
            return new SerializableGuid(guid);
        }
    }
}