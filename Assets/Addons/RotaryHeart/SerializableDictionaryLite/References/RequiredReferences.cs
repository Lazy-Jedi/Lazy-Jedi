using UnityEngine;
using UnityEngine.Tilemaps;

namespace RotaryHeart.Lib.SerializableDictionary
{
    /// <summary>
    /// This class is used so that the dictionary keys can have a default value, unity editor will give the default value, because it can't be null.
    /// This should only be used for UnityEngine.Object inherited classes
    /// </summary>
    public class RequiredReferences : ScriptableObject
    {
        //Important note, the fields need to be private so that the reflection code can find them.
        //Use [SerializeField] so that the editor draws the property field and sets a default value
        [SerializeField]
        private GameObject _gameObject;
        [SerializeField]
        private Texture2D _texture2D;
        [SerializeField]
        private Sprite _sprite;
        [SerializeField]
        private Color _color;
        [SerializeField]
        private Color32 _color32;
        [SerializeField]
        private Material _material;
        [SerializeField]
        private AudioClip _audioClip;
        [SerializeField]
        private TextAsset _textAsset;
        
        [SerializeField]
        private Collider _collider;
        [SerializeField]
        private BoxCollider _boxCollider;
        [SerializeField]
        private CapsuleCollider _capsuleCollider;
        [SerializeField]
        private MeshCollider _meshCollider;
        [SerializeField]
        private SphereCollider _sphereCollider;
        [SerializeField]
        private TerrainCollider _terrainCollider;
        [SerializeField]
        private WheelCollider _wheelCollider;

        [SerializeField]
        private Collider2D _collider2D;
        [SerializeField]
        private BoxCollider2D _boxCollider2D;
        [SerializeField]
        private CapsuleCollider2D _capsuleCollider2D;
        [SerializeField]
        private CircleCollider2D _circleCollider2D;
        [SerializeField]
        private CompositeCollider2D _compositeCollider2D;
        [SerializeField]
        private CustomCollider2D _customCollider2D;
        [SerializeField]
        private EdgeCollider2D _edgeCollider2D;
        [SerializeField]
        private PolygonCollider2D _polygonCollider2D;
        [SerializeField]
        private TilemapCollider2D _tilemapCollider2D;

        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
    }
}