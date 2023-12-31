using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BigBro.SO
{
    [CreateAssetMenu(fileName = "SO_Player", menuName = "BigBro/ScriptableObjects/PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {
        //Value that expose to the editor should be public fields
        //Should Update the NetworkPlayerData class as well for network purpose
        public string Name;
        public Color PlayerOutfit;
        public Ability Ability;
        
        public Action OnDataChanged;
        public bool ShouldNetworkUpdate => _shouldNetworkUpdate;
        private bool _shouldNetworkUpdate = true;

        public void ChangeData()
        {
            OnDataChanged.Invoke();
        }
        
        public void Reset()
        {
            Name = "";
            PlayerOutfit = Color.black;
            Ability = Ability.Attacker;
            OnDataChanged = null;
        }

        public void SetChangeNetworkAvaliable(bool isAvaliable)
        {
            _shouldNetworkUpdate = isAvaliable;
            if (isAvaliable)
            {
                //if set avaliable call onDataChange for once in case the change is not solid
                OnDataChanged?.Invoke();
            }
        }

        public PlayerDataHolder GetPlayerDataHolder()
        {
            FieldInfo[] plyFieldInfos = typeof(PlayerData).GetFields();
            PlayerDataHolder holder = new PlayerDataHolder();
            Dictionary<string, FieldInfo> holderFieldDic = PlayerDataHolder.GetDic();
            foreach (var fieldInfo in plyFieldInfos)
            {
                if (fieldInfo.IsPublic)
                {
                    object plyDataValue = fieldInfo.GetValue(this);
                    if (holderFieldDic.TryGetValue(fieldInfo.Name, out FieldInfo holderFieldInfo))
                    {
                        holderFieldInfo.SetValue(holder,plyDataValue);
                    };
                }
            }
            return holder;
        }

        public void SetPlayerData(PlayerDataHolder holder)
        {
            FieldInfo[] plyFieldInfos = typeof(PlayerData).GetFields();
            Dictionary<string, FieldInfo> holderFieldDic = PlayerDataHolder.GetDic();
            foreach (var fieldInfo in plyFieldInfos)
            {
                if (fieldInfo.IsPublic)
                {
                    if (holderFieldDic.TryGetValue(fieldInfo.Name, out FieldInfo holderFieldInfo))
                    {
                        object holderValue = holderFieldInfo?.GetValue(holder);
                        fieldInfo.SetValue(this,holderValue);
                    }
                }
            }
        }
    }

    public class PlayerDataHolder
    {
        public string Name;
        public Color PlayerOutfit;
        public Ability Ability;
        
        private static Dictionary<string, FieldInfo> holderfieldDic = new Dictionary<string, FieldInfo>();
        static PlayerDataHolder()
        {
            FieldInfo[] holderfieldInfos = typeof(PlayerDataHolder).GetFields();
            foreach (var fieldInfo in holderfieldInfos)
            {
                if (fieldInfo.IsPublic)
                {
                    holderfieldDic.Add(fieldInfo.Name,fieldInfo);
                }
            }
        }
        public static Dictionary<string, FieldInfo> GetDic()
        {
            return holderfieldDic;
        }
    }


    [CustomEditor(typeof(PlayerData)), CanEditMultipleObjects]
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
            
            //For editor changes only
            root.TrackSerializedObjectValue(serializedObject, CheckForWarnings);

            return root;
        }
        
        void CheckForWarnings(SerializedObject serializedObject)
        {
            var player = serializedObject.targetObject as PlayerData;
            player.OnDataChanged?.Invoke();
        }
    }

    public enum Ability
    {
        Attacker,
        Shield,
        Healer
    }
}