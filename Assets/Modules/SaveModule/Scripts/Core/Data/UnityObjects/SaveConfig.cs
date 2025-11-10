using Modules.SaveModule.Scripts.Core.Data.ValueObjects;
using UnityEngine;

namespace Modules.SaveModule.Scripts.Core.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "SaveConfig", menuName = "CardMatch/Save Configuration")]
    public class SaveConfig : ScriptableObject
    {
        public SaveSettings settings;
    }
}