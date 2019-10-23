using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_1 : MonoBehaviour
{
	public new ParticleSystem particleSystem;
    public int particleNum = 3000;
    public float radiusIn = 8f;
    public float radiusOut = 12f;
    public float radiusOut_2 = 10f;
    public float speed = 1f;
    public float speed_2 = 0.2f;
    public float speed_3 = 1f;
    public float stdDev = 1.5f;
    public Gradient gradient;

    private ParticleSystem.Particle[] particles;
    private float[] particleRadius;
    private float[] particleAngle;
    private float[] particleRadius_1;
    private float[] particleRadius_2;
    private bool[] direction;

    private bool isHover;

    private void Start()
    {
        isHover = false;
        particles = new ParticleSystem.Particle[particleNum];
        particleRadius = new float[particleNum];
        particleRadius_1 = new float[particleNum];
        particleAngle = new float[particleNum];
        direction = new bool[particleNum];

        ParticleSystem.MainModule mainPSystem = particleSystem.main;
        mainPSystem.maxParticles = particleNum;
        particleSystem.Emit(particleNum);
        particleSystem.GetParticles(particles);

        for(int i = 0; i < particleNum; i++)
        {
            float angle = Random.Range(0f, 360f);
            float r = GetNormalDistribution((radiusOut + radiusIn) * 0.5f, stdDev);
            float rad = angle / 180 * Mathf.PI;
            particleRadius[i] = r;
            particleRadius_1[i] = r;
            particleAngle[i] = angle;
            particles[i].position = new Vector3(r * Mathf.Cos(rad), r * Mathf.Sin(rad), 0);

            if (i % 2 == 0)
                direction[i] = true;
            else
                direction[i] = false;
        }

        particleSystem.SetParticles(particles, particles.Length);
        particleRadius_2 = GetRandomArray(particleRadius);
        for(int i = 0; i < particleNum; i++)
        {
            if(particleRadius_1[i] > particleRadius_2[i])
            {
                float temp = particleRadius_2[i];
                particleRadius_2[i] = particleRadius_1[i];
                particleRadius_1[i] = temp;
            }
        }
    }

    float startRadius, endRadius, curSpeed;

    private void Update()
    {
        for(int i = 0; i < particleNum; i++)
        {
            if (isHover)
            {
                startRadius = radiusIn;
                endRadius = radiusOut_2;
                curSpeed = speed_3;
            }
            else
            {
                startRadius = particleRadius_1[i];
                endRadius = particleRadius_2[i];
                curSpeed = speed_2;
            }

            float num = speed * (i % 5 + 1) * Time.deltaTime;
            float num_2 = curSpeed * (i % 5 + 1) * Time.deltaTime;

            if (i%2 == 0)
                particleAngle[i] += num;
            else
                particleAngle[i] -= num;

            if (direction[i])
                particleRadius[i] += num_2;
            else
                particleRadius[i] -= num_2;

            if (particleAngle[i] > 360)
                particleAngle[i] -= 360;
            else if (particleAngle[i] < 0)
                particleAngle[i] += 360;

            if (particleRadius[i] > endRadius)
                direction[i] = false;
            else if (particleRadius[i] < startRadius)
                direction[i] = true;

            float rad = particleAngle[i] / 180 * Mathf.PI;
            float rs = particleRadius[i];
            particles[i].position = new Vector3(rs * Mathf.Cos(rad), rs * Mathf.Sin(rad), 0);
            particles[i].startColor = gradient.Evaluate(rs-radiusIn-0.5f);
        }

        particleSystem.SetParticles(particles, particles.Length);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("button"))
        {
            isHover = true;
        }else
        {
            isHover = false;
        }
    }

    private float GetNormalDistribution(float min, float Dev)
    {
        float u1 = Random.Range(0f, 1f);
        float u2 = Random.Range(0f, 1f);
        float r = Mathf.Sqrt(-2 * Mathf.Log(u1));
        float sita = u2 * Mathf.PI * 2;
        return min + Dev * r * Mathf.Sin(sita);
    }

    private float[] GetRandomArray(float[] arr)
    {
        float[] newArr = new float[arr.Length];
        int len = arr.Length;
        int num;
        float temp;
        for(int i = 0; i < len; i++)
        {
            newArr[i] = arr[i];
        }
        for(int i = 0; i < len; i++)
        {
            num = Random.Range(0, len);
            temp = newArr[num];
            newArr[num] = newArr[i];
            newArr[i] = temp;
        }

        return newArr;
    }
}
