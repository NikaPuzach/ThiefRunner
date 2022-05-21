using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Game;

public class AssetsContainer : MonoBehaviour
{
    public static AssetsContainer Instance { get; private set; }

    [SerializeField] DataContainer dataContainer;
    public DataContainer DataContainer => dataContainer;

    //[Dependency] LocalizationData localization;
    //public ILocalization Localization => localization;

    [SerializeField] DesignContainer designContainer;
    public DesignContainer Design => designContainer;

    [Space]
    [SerializeField] SpriteAtlas iconsAtlas;
    public SpriteAtlas IconsAtlas => iconsAtlas;
    [SerializeField] Sprite defalutEmptySprite;
    public Sprite DefaltEmptySprite => defalutEmptySprite;
    [SerializeField] Sprite defalutErrorSprite;
    public Sprite DefaltErrorSprite => defalutErrorSprite;

    void Awake()
    {
        Instance = this;
    }

    private readonly Dictionary<System.Type, string> LoadPathes
        = new Dictionary<System.Type, string>()
        {
            //{ typeof(UnitModelController),      "Models/Units" },
        };
    private string GetCachedPath<T>() where T : UnityEngine.Object
    {
        foreach (var pair in LoadPathes)
            if (typeof(T) == pair.Key || typeof(T).IsSubclassOf(pair.Key))
                return pair.Value;

        // By default we assume that the file is in default 'Resources/' folder
        return string.Empty;
    }

    public T Get<T>(string filename) where T : UnityEngine.Object
    {
        var path = GetCachedPath<T>();

        var asset = Resources.Load<T>($"{path}{filename}");
        if (asset == null)
            Debug.LogError($"There is no asset at path 'Resources/{path}' with name '{filename}' matching type '{typeof(T).Name}'");

        return asset;
    }
    public bool TryGet<T>(string filename, out T asset) where T : UnityEngine.Object
    {
        var path = GetCachedPath<T>();

        asset = Resources.Load<T>($"{path}{filename}");
        if (asset == null)
        {
            Debug.LogError($"There is no asset at path 'Resources/{path}' with name '{filename}' matching type '{typeof(T).Name}'");
            return false;
        }
        else
            return true;
    }
}
