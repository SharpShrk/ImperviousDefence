using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveActions : MonoBehaviour
{
    [SerializeField] private int _waveIndexForAds = 5;

    private Waves _waves;

    // ����� ����� ��������� ������ ����� ����� ������ � ��� ��� ����� 3,2,1 ����� �������� �������.
    // �������� ����� ������� ������ �����, ������� ����� ��� ������, ��. � ����� � ��� ����. ������ ������������� ��� ��� ��� �������

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
