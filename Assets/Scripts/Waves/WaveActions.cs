using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveActions : MonoBehaviour
{
    [SerializeField] private int _waveIndexForAds = 5;

    private Waves _waves;

    // здесь можно запускать каждую п€тую волну плашку о том что через 3,2,1 будет показана реклама.
    // возможно стоит создать другой класс, который будет это чекать, хз. ј может и тут пр€м. “олько переименовать что это дл€ рекламы

    private void Start()
    {
        _waves = GetComponent<Waves>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void CheckWave()
    {

    }
}
