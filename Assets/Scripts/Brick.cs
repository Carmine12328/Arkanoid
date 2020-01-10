using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    public int hitpoint = 3;

    public ParticleSystem destroyEffect;

    public static event Action<Brick> OnBrickDestruction;

    private SpriteRenderer sr;
    private void Awake()
    {
        this.sr = this.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ApplyCollisionLogic(ball);
    }

    private void ApplyCollisionLogic(Ball ball)
    {
        this.hitpoint--;

        if(this.hitpoint <= 0)
        {
            OnBrickDestruction?.Invoke(this);
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            this.sr.sprite = BricksManager.Instance.Sprites[this.hitpoint - 1];
        }
    }

    private void SpawnDestroyEffect()
    {
        Vector3 bricPos = gameObject.transform.position;
        Vector3 spawnPos = new Vector3(bricPos.x, bricPos.y, bricPos.z - 0.2f);
        GameObject effect = Instantiate(destroyEffect.gameObject, spawnPos, Quaternion.identity);

        MainModule nm = effect.GetComponent<ParticleSystem>().main;
        nm.startColor = this.sr.color;
        Destroy(effect, destroyEffect.main.startLifetime.constant);
    }

    internal void Init(Transform conteinerTransform, Sprite sprite, Color color, int hitpoints)
    {
        this.transform.SetParent(conteinerTransform);
        this.sr.sprite = sprite;
        this.sr.color = color;
        this.hitpoint = hitpoints;
    }
}
