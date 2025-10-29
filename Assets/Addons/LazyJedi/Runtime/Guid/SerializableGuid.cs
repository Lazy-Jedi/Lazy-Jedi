using System;
using UnityEngine;

namespace LazyJedi.Guid
{
    [Serializable]
    public class SerializableGuid
    {
        [SerializeField, HideInInspector]
        private string _guidString;

        public string GuidString => _guidString;

        #region CONSTRUCTORS

        public SerializableGuid()
        {
            _guidString = EmptyGuidString();
        }

        public SerializableGuid(string guidString)
        {
            _guidString = string.IsNullOrEmpty(guidString) ? System.Guid.NewGuid().ToString() : guidString;
        }

        public SerializableGuid(System.Guid guid)
        {
            _guidString = guid.ToString();
        }

        #endregion

        #region METHODS

        public static System.Guid NewGuid() => System.Guid.NewGuid();

        public static string NewGuidString() => NewGuid().ToString();

        public static System.Guid EmptyGuid() => System.Guid.Empty;

        public static string EmptyGuidString() => EmptyGuid().ToString();

        public override string ToString() => _guidString;

        public System.Guid ToGuid()
        {
            // Safely parse; return Guid.Empty if invalid
            if (System.Guid.TryParse(_guidString, out var result))
                return result;

            Debug.LogWarning($"Invalid GUID string: '{_guidString}'. Returning Guid.Empty.");
            return System.Guid.Empty;
        }

        public void Regenerate()
        {
            _guidString = System.Guid.NewGuid().ToString();
        }

        #endregion

        #region EQUALITY

        public bool Equals(SerializableGuid other)
        {
            return other is not null && string.Equals(_guidString, other._guidString, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SerializableGuid);
        }

        public override int GetHashCode()
        {
            return _guidString?.GetHashCode() ?? 0;
        }

        public static bool operator ==(SerializableGuid a, SerializableGuid b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a is null || b is null)
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(SerializableGuid a, SerializableGuid b) => !(a == b);

        #endregion

        #region IMPLICIT CONVERSIONS

        public static implicit operator string(SerializableGuid guid) => guid?.GuidString;
        public static implicit operator SerializableGuid(string guid) => new(guid);
        public static implicit operator System.Guid(SerializableGuid guid) => guid?.ToGuid() ?? System.Guid.Empty;
        public static implicit operator SerializableGuid(System.Guid guid) => new(guid);

        #endregion
    }
}