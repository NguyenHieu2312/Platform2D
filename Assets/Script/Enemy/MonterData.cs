using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Monster", menuName ="ScriptableObjects/MonsterType")]
public class MonterData : ScriptableObject
{
    [Header("General stats")]
    [SerializeField] public string _monsterName;

    [Header("Combat stats")]
    [SerializeField] public float _monsterDamage = 10f;
    [SerializeField] public float _monsterHealth = 100f;
    [SerializeField] public float _monsterSpeed = 5f;
}
