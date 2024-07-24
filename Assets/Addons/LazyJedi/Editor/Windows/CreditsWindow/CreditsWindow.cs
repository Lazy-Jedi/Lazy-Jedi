using LazyJedi.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsWindow : EditorWindow
{
    #region WINDOW

    [SerializeField]
    private VisualTreeAsset _visualTreeAsset;
    private static CreditsWindow _creditsWindow;

    public static void CreateWindow()
    {
        _creditsWindow = GetWindow<CreditsWindow>();
        _creditsWindow.titleContent = new GUIContent("Credits");
        _creditsWindow.ShowModalUtility();
    }

    #endregion

    #region FIELDS

    private VisualElement _root;

    private const string LAZY_GITHUB = "https://github.com/Lazy-Jedi/lazy-jedi";
    private const string BLU_GITHUB = "https://github.com/BLUDRAG";
    private const string KENNEY_ASSETS = "https://www.kenney.nl/assets";
    private const string FLAT_ICON = "https://www.flaticon.com/free-icon/light-saber_922809?term=star+wars&page=1&position=2&origin=tag&related_id=922809";

    #endregion

    #region UNITY METHODS

    public void CreateGUI()
    {
        _root = _visualTreeAsset.CloneTree();

        ScrollView scrollView = new ScrollView();

        Label creditsLabel = UIToolkitUtility.CreateLabel("Credits", scrollView);
        creditsLabel.AddToClassList("header-label");

        VisualElement container = UIToolkitUtility.CreateContainer(scrollView);
        container.AddToClassList("container-border");

        Label lazyLabel = UIToolkitUtility.CreateLabel("Lazy-Jedi", container);
        lazyLabel.AddToClassList("label");
        Button lazyButton = UIToolkitUtility.CreateButton("Lazy-Jedi GitHub", () => { Application.OpenURL(LAZY_GITHUB); }, container);

        Label nakamaLabel = UIToolkitUtility.CreateLabel("Nakama", container);
        nakamaLabel.AddToClassList("label");
        Button bluButton = UIToolkitUtility.CreateButton("BluMalice GitHub", () => { Application.OpenURL(BLU_GITHUB); }, container);

        Label assetsLabel = UIToolkitUtility.CreateLabel("Assets", container);
        assetsLabel.AddToClassList("label");
        Button kenneyButton = UIToolkitUtility.CreateButton("Kenney Assets", () => { Application.OpenURL(KENNEY_ASSETS); }, container);

        Label iconsLabel = UIToolkitUtility.CreateLabel("Icons", container);
        iconsLabel.AddToClassList("label");
        Button flatIconButton = UIToolkitUtility.CreateButton("FlatIcon - Star Wars Icon", () => { Application.OpenURL(FLAT_ICON); }, container);

        _root.Add(scrollView);
        rootVisualElement.Add(_root);
    }

    #endregion
}