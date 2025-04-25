using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "SoundConfig", menuName = "ScriptableObjects/SoundConfig", order = 0)]

//public class SoundConfig : ScriptableObject
//{
//    [SerializeField] private AudioClip music;
//    public AudioClip SoundClip => music;
//}
[CreateAssetMenu(fileName = "VolumeConfig", menuName = "Scriptable Objects/Audio/VolumeConfig", order = 1)]
public class VolumeConfig : ScriptableObject
{
    [Range(0f, 1f)]
    public float volume = 0.5f; 
}
