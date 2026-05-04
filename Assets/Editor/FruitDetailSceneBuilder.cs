#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;

[InitializeOnLoad]
public static class FruitDetailSceneBuilder
{
    static FruitDetailSceneBuilder()
    {
        EditorApplication.delayCall += CreateSceneIfMissing;
    }

    static void CreateSceneIfMissing()
    {
        if (!System.IO.File.Exists("Assets/Scenes/FruitDetailScene.unity"))
        {
            BuildScene();
        }
        else
        {
            // Still ensure it's in build settings
            AddToBuildSettings("Assets/Scenes/SampleScene.unity");
            AddToBuildSettings("Assets/Scenes/FruitDetailScene.unity");
        }
    }

    [MenuItem("ARPoster/Rebuild Fruit Detail Scene")]
    public static void BuildScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Dark background camera
        var cam = Object.FindObjectOfType<Camera>();
        if (cam != null)
        {
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0.05f, 0.07f, 0.12f);
        }

        // EventSystem
        var esGO = new GameObject("EventSystem");
        esGO.AddComponent<UnityEngine.EventSystems.EventSystem>();
        esGO.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();

        // Canvas
        var canvasGO = new GameObject("Canvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        scaler.matchWidthOrHeight = 0.5f;
        canvasGO.AddComponent<GraphicRaycaster>();

        // Full-screen background panel
        var bg = MakeRect("Background", canvasGO.transform);
        bg.AddComponent<Image>().color = new Color(0.05f, 0.07f, 0.12f);
        Stretch(bg);

        // Green header band
        var header = MakeRect("Header", bg.transform);
        Anchors(header, 0f, 0.86f, 1f, 1f);
        header.AddComponent<Image>().color = new Color(0.12f, 0.45f, 0.18f);

        // Fruit name in header
        var nameGO = MakeTMP("FruitNameText", header.transform, "Fruit Name", 80, FontStyles.Bold, TextAlignmentOptions.Center);
        Stretch(nameGO);

        // Thin divider line
        var divider = MakeRect("Divider", bg.transform);
        Anchors(divider, 0.05f, 0.845f, 0.95f, 0.858f);
        divider.AddComponent<Image>().color = new Color(0.3f, 0.8f, 0.35f);

        // Price text
        var priceGO = MakeTMP("PriceText", bg.transform, "Price: --", 52, FontStyles.Bold, TextAlignmentOptions.Center);
        Anchors(priceGO, 0f, 0.77f, 1f, 0.845f);
        priceGO.GetComponent<TMP_Text>().color = new Color(1f, 0.85f, 0.2f);

        // Section label
        var sectionLabel = MakeTMP("HealthLabel", bg.transform, "Health Benefits", 44, FontStyles.Bold, TextAlignmentOptions.Left);
        Anchors(sectionLabel, 0.06f, 0.70f, 0.94f, 0.77f);
        sectionLabel.GetComponent<TMP_Text>().color = new Color(0.45f, 0.95f, 0.5f);

        // Health benefits body
        var benefitsGO = MakeTMP("HealthBenefitsText", bg.transform, "Loading...", 36, FontStyles.Normal, TextAlignmentOptions.TopLeft);
        Anchors(benefitsGO, 0.06f, 0.22f, 0.94f, 0.70f);

        // Back button
        var btnGO = MakeRect("BackButton", bg.transform);
        Anchors(btnGO, 0.15f, 0.07f, 0.85f, 0.17f);
        btnGO.AddComponent<Image>().color = new Color(0.18f, 0.52f, 0.95f);
        btnGO.AddComponent<Button>();

        var btnText = MakeTMP("BackButtonText", btnGO.transform, "←  Back to AR", 44, FontStyles.Bold, TextAlignmentOptions.Center);
        Stretch(btnText);

        // DetailManager drives the UI at runtime via FruitDetailDisplay
        var mgr = new GameObject("DetailManager");
        mgr.AddComponent<FruitDetailDisplay>();

        string path = "Assets/Scenes/FruitDetailScene.unity";
        EditorSceneManager.SaveScene(scene, path);
        AssetDatabase.Refresh();

        AddToBuildSettings("Assets/Scenes/SampleScene.unity");
        AddToBuildSettings(path);

        Debug.Log("[ARPoster] FruitDetailScene created at " + path);
        EditorUtility.DisplayDialog("ARPoster", "FruitDetailScene created and added to Build Settings!", "OK");
    }

    // ── helpers ─────────────────────────────────────────────────────────────

    static GameObject MakeRect(string name, Transform parent)
    {
        var go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        return go;
    }

    static GameObject MakeTMP(string name, Transform parent, string text, float size, FontStyles style, TextAlignmentOptions align)
    {
        var go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        var t = go.AddComponent<TextMeshProUGUI>();
        t.text = text;
        t.fontSize = size;
        t.fontStyle = style;
        t.alignment = align;
        t.color = Color.white;
        t.enableWordWrapping = true;
        return go;
    }

    static void Stretch(GameObject go)
    {
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = rt.offsetMax = Vector2.zero;
    }

    static void Anchors(GameObject go, float x0, float y0, float x1, float y1)
    {
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(x0, y0);
        rt.anchorMax = new Vector2(x1, y1);
        rt.offsetMin = rt.offsetMax = Vector2.zero;
    }

    static void AddToBuildSettings(string path)
    {
        var scenes = EditorBuildSettings.scenes;
        foreach (var s in scenes)
            if (s.path == path) return;
        var list = new System.Collections.Generic.List<EditorBuildSettingsScene>(scenes);
        list.Add(new EditorBuildSettingsScene(path, true));
        EditorBuildSettings.scenes = list.ToArray();
    }
}
#endif
