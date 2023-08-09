using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Rain_Controller : MonoBehaviour
{
    public ParticleSystem Rain; // ��ƼŬ �ý��� ���۷���
    public ParticleSystem Rain1; // ��ƼŬ �ý��� ���۷���

    private float minSimulationSpeed = 0.5f; // �ּ� �ùķ��̼� �ӷ�
    private float maxSimulationSpeed = 2.0f; // �ִ� �ùķ��̼� �ӷ�
    private float updateInterval = 60.0f; // ������Ʈ �ֱ� (1��)
    private float DB_PredictData_Rain;

    private void Start()
    {
        Rain = GetComponent<ParticleSystem>();
        Rain1 = GetComponent<ParticleSystem>();

        // �ֱ������� ���� �о���� �Լ��� ȣ��
        InvokeRepeating("ReadRainfall", 0f, updateInterval);
    }

    private void ReadRainfall()
    {
        DB_PredictData_Rain = float.Parse(API_Data.predictData.rain); // string �� float ������ Parse

        if (DB_PredictData_Rain == 0)
        {
            UpdateRainEffect(0); // �������� 0�� ��
        }
        else
        {
            UpdateRainEffect(DB_PredictData_Rain);
        }
    }

    public void UpdateRainEffect(float rainfall)
    {
        float normalizedRainfall = Mathf.Clamp01(rainfall / 100.0f); // 0~1 ���� ������ ����ȭ
        // �ùķ��̼� �ӷ��� �������� ���� ����
        float newSimulationSpeed = Mathf.Lerp(minSimulationSpeed, maxSimulationSpeed, normalizedRainfall);

        var Rain_Speed = Rain.main;
        var Rain1_Speed = Rain1.main;

        Rain_Speed.simulationSpeed = newSimulationSpeed;
        Rain1_Speed.simulationSpeed = newSimulationSpeed;
    }
}
