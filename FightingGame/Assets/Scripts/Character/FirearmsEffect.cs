using FGDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmsEffect : MonoBehaviour
{
    private SpriteRenderer effectSpriteRender;
    Sprite[] sprites;

    // currentSetPosition Value {downX, downY, upX, upY, side}
    private float[] currentSetPosition = new float[5];
    float[] gunPosition;
    float[] riflePosition;

    public void init()
    {
        sprites = Resources.LoadAll<Sprite>("Art/BulletEffect/");
        effectSpriteRender = gameObject.GetComponent<SpriteRenderer>();

        gunPosition = new float[5] { 0.04f, -0.4f, -0.04f, 1.9f, 1.0f };
        riflePosition = new float[5] { 0.05f, -0.5f, -0.05f, 2.0f, 1.25f };
    }

    // 총기류 이펙트 설정
    public void SetWeaponEffect(ENUM_WEAPON_TYPE weaponType) 
    {
        if (weaponType == ENUM_WEAPON_TYPE.Gun) 
        {
            effectSpriteRender.sprite = sprites[0];
            currentSetPosition = gunPosition;
        }
        else if(weaponType == ENUM_WEAPON_TYPE.Rifle)
        {
            effectSpriteRender.sprite = sprites[1];
            currentSetPosition = riflePosition;
        }
    }

    // 캐릭터 방향에 따른 이펙트 위치와 각도 조정
    public void SetEffectPosition(Vector2 vec) 
    {
        if (vec.y != 0)
        {
            float rotateZ = (vec.y <= 0) ? 90 : -90;
            int vecY = (int)vec.y;

            effectSpriteRender.transform.localPosition = new Vector2(currentSetPosition[vecY + 1], currentSetPosition[vecY + 2]);
            effectSpriteRender.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotateZ));
        }
        else if (vec.x != 0)
        {
            float rotateY = (vec.x == 1) ? 180 : 0;
         
            effectSpriteRender.transform.localPosition = new Vector2(vec.x * currentSetPosition[4], 0.5f);
            effectSpriteRender.transform.rotation = Quaternion.Euler(new Vector3(0, rotateY, 0));
        }
    }
}