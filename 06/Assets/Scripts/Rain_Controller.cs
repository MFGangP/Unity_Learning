using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Rain_Controller : MonoBehaviour
{
    public ParticleSystem Rain; // 파티클 시스템 레퍼런스
    public ParticleSystem Rain1; // 파티클 시스템 레퍼런스

    private float minSimulationSpeed = 0.5f; // 최소 시뮬레이션 속력
    private float maxSimulationSpeed = 2.0f; // 최대 시뮬레이션 속력
    private float updateInterval = 60.0f; // 업데이트 주기 (1분)
    private float DB_PredictData_Rain;

    private void Start()
    {
        Rain = GetComponent<ParticleSystem>();
        Rain1 = GetComponent<ParticleSystem>();

        // 주기적으로 값을 읽어오는 함수를 호출
        InvokeRepeating("ReadRainfall", 0f, updateInterval);
    }

    private void ReadRainfall()
    {
        DB_PredictData_Rain = float.Parse(API_Data.predictData.rain); // string 값 float 형으로 Parse

        if (DB_PredictData_Rain == 0)
        {
            UpdateRainEffect(0); // 강수량이 0일 때
        }
        else
        {
            UpdateRainEffect(DB_PredictData_Rain);
        }
    }

    public void UpdateRainEffect(float rainfall)
    {
        float normalizedRainfall = Mathf.Clamp01(rainfall / 100.0f); // 0~1 사이 값으로 정규화
        // 시뮬레이션 속력을 강수량에 따라 조절
        float newSimulationSpeed = Mathf.Lerp(minSimulationSpeed, maxSimulationSpeed, normalizedRainfall);

        var Rain_Speed = Rain.main;
        var Rain1_Speed = Rain1.main;

        Rain_Speed.simulationSpeed = newSimulationSpeed;
        Rain1_Speed.simulationSpeed = newSimulationSpeed;
    }
}
