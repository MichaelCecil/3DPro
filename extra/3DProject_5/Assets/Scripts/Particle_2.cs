using UnityEngine;
using System.Collections;

public class Particle_2 : MonoBehaviour
{

    public new ParticleSystem particleSystem; // 粒子系统
    private ParticleSystem.Particle[] particlesArray; //存储粒子的数组

    public float spacing = 0.3f; // 粒子之间的空隙

    public int seaResolution = 100; //粒子范围

    //noise 这里使用了柏林噪声，用以制造波浪效果
    public float noiseScale = 0.05f;  //噪声范围
    public float heightScale = 3f;  // 高度范围

    //..
    private float perlinNoiseAnimX = 0.01f; // 柏林噪声相关参数
    private float perlinNoiseAnimY = 0.01f;
    private float zPos;

    //...color
    public Gradient colorGradient; // 颜色的渐变

    void Start()
    {
        particlesArray = new ParticleSystem.Particle[seaResolution * seaResolution]; //初始化数组
        var particleMain = particleSystem.main;
        particleMain.maxParticles = seaResolution * seaResolution; //最大粒子数，也就是循环时用到的边界
        particleSystem.Emit(seaResolution * seaResolution); //发射粒子，参数为要发射的粒子数量
        particleSystem.GetParticles(particlesArray);
    }

    void Update()
    {
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {
                zPos = Mathf.PerlinNoise(i * noiseScale + perlinNoiseAnimX, j * noiseScale + perlinNoiseAnimY) * heightScale; // 由柏林噪声确定的高度值
                particlesArray[i * seaResolution + j].startColor = colorGradient.Evaluate(zPos); // 由高度值确定的颜色变化
                particlesArray[i * seaResolution + j].position = new Vector3(i * spacing, zPos, j * spacing);
            }
        }

        perlinNoiseAnimX += 0.01f;
        perlinNoiseAnimY += 0.01f;

        particleSystem.SetParticles(particlesArray, particlesArray.Length); // 设置该系统的粒子
    }

}

