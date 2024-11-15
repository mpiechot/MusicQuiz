#nullable enable

using Musicmania.Exceptions;
using UnityEngine;

namespace Musicmania.Data
{
    public abstract class BaseData : ScriptableObject
    {
        [SerializeField]
        private new string? name = string.Empty;

        [SerializeField]
        private Sprite? image;

        [field: SerializeField]
        public CategoryTag Tags { get; private set; }

        public string Name => !string.IsNullOrEmpty(name) ? name : throw new SerializeFieldNotAssignedException();

        public Sprite? Image => image;
    }
}
