using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SpritesConfig", menuName = "ScriptableObjects/Configs/SpritesConfig")]
public class SpritesConfig : ScriptableObject
{
    [SerializeField] private List<SpriteConfig> _configs;
    public Dictionary<int, SpriteConfig> Configs
    {
        get
        {
            Dictionary<int, SpriteConfig> keyValuePairs = new Dictionary<int, SpriteConfig>();
            
            foreach (SpriteConfig config in _configs)
            {
                keyValuePairs.Add(config.ID, config);
            }

            return keyValuePairs;
        }
    }
        
}

[Serializable]
public class SpriteConfig
{
    [SerializeField] private int _id;
    public int ID => _id;

    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;
}