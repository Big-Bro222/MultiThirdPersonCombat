using System;
using Fusion;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.UIElements.BindingExtensions;

namespace BigBro
{
    [CreateAssetMenu(fileName = "SO_Player", menuName = "BigBro/ScriptableObjects/PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {
        public string Name;
        public Color PlayerOutfit;
        public Ability Ability;
        public Action OnDataChange ;

        public void Reset()
        {
            Name = "";
            PlayerOutfit = Color.black;
            Ability = Ability.Attacker;
            OnDataChange = null;
        }
    }
    
    
    [CustomEditor(typeof(PlayerData)),CanEditMultipleObjects]
    public class PlayerDataCustomEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();

            // Create FloatFields for serialized properties.
            var nameField = new TextField("Name") { bindingPath = "Name" };
            var outfitField = new ColorField("Player Outfit") { bindingPath = "PlayerOutfit" };
            var AbilityField = new EnumField("Ability") { bindingPath = "Ability" };
            root.Add(nameField);
            root.Add(outfitField);
            root.Add(AbilityField);
            root.TrackSerializedObjectValue(serializedObject, CheckForWarnings);

            return root;
        }

        // Check the current values of the serialized properties to either display or hide the warnings.
        void CheckForWarnings(SerializedObject serializedObject)
        {
            var player = serializedObject.targetObject as PlayerData;
            player.OnDataChange?.Invoke();
        }
    }

    public enum Ability
    {
        Attacker,
        Shield,
        Healer
    }
}

