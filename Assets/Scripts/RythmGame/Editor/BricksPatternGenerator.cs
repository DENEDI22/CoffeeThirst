using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RythmGame;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BricksPatternGenerator : EditorWindow
{
    [MenuItem("Window/UI Toolkit/BricksPatternGenerator")]
    public static void ShowExample()
    {
        BricksPatternGenerator wnd = GetWindow<BricksPatternGenerator>();
        wnd.titleContent = new GUIContent("BricksPatternGenerator");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        // VisualElement label = new Label("Hello World! From C#");
        // root.Add(label);

        // Import UXML
        var visualTree =
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/Scripts/RythmGame/Editor/BricksPatternGenerator.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        var buttons = root.Query<Button>();
        buttons.ForEach(RegisterHandler);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        // var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/RythmGame/Editor/BricksPatternGenerator.uss");
        // VisualElement labelWithStyle = new Label("Hello World! With Style");
        // labelWithStyle.styleSheets.Add(styleSheet);
        // root.Add(labelWithStyle);
    }

    private void RegisterHandler(Button _button)
    {
        _button.RegisterCallback<ClickEvent>(GenerateBrickPattern);
    }

    private void GenerateBrickPattern(ClickEvent _clickEvent)
    {
        VisualElement root = rootVisualElement;
        Button button = _clickEvent.currentTarget as Button;

        BrickSpawnerMusicPattern newPattern = CreateInstance<BrickSpawnerMusicPattern>();
        newPattern.gamespeedMultiplyer = root.Q<FloatField>("GameSpeedMultiplyerField").value;
        newPattern.startDelay = 1.113f;
        newPattern.brickSpawningParams =
            AudacityLabelsToBrickSpawningParams(root.Q<TextField>("JsonPathTextField").value,
                root.Q<FloatField>("GameSpeedMultiplyerField").value);
        AssetDatabase.CreateAsset(newPattern, $"Assets/{root.Q<TextField>("NewPatternName").value}.asset");
    }

    private List<BrickSpawningParams> AudacityLabelsToBrickSpawningParams(string _path, float _speedMultiplyer)
    {
        List<BrickSpawningParams> paramsList = new List<BrickSpawningParams>();
        List<AudacityLabel> labelsList;
        using (StreamReader _reader = new StreamReader(_path))
        {
            string value = _reader.ReadToEnd();
            labelsList = (JsonConvert.DeserializeObject<AudacityLabel[]>(value) ??
                          throw new InvalidOperationException()).ToList();
        }

        for (var index = 0; index < labelsList.Count; index++)
        {
            var label = labelsList[index];
            try
            {
                paramsList.Add(new BrickSpawningParams()
                {
                    brickParameters = new BrickParameters()
                    {
                        soundType = (SoundTypes)label.Label,
                        speed = 12f * _speedMultiplyer
                    },
                    /*nextParamDelay = labelsList[index + 1].StartTime - label.StartTime*/
                    numberOfTheTrack = label.Label,
                    nextParamTime = labelsList[index].StartTime - (2f/_speedMultiplyer)
                });
            }
            catch (Exception e)
            {
                Console.Write("Last object detected");
                paramsList.Add(new BrickSpawningParams()
                {
                    brickParameters = new BrickParameters()
                    {
                        soundType = (SoundTypes)label.Label,
                        speed = 5.75f
                    },
                    nextParamTime = 0
                });
            }
        }

        return paramsList;
    }
}