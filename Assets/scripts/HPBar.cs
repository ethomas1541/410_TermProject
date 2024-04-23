// made by following https://www.youtube.com/watch?v=_lREXfAMUcE&t=4s&ab_channel=BMo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    // make this the slider component of canvas
    [SerializeField] private Slider slider;
    // make player cam
    [SerializeField] private Camera Camera;
    // make this the entity this health bar represents
    [SerializeField] private Transform Target;
    // represents the offset of the bar to make it look right above the entity
    [SerializeField] private Vector3 offset;

    public void UpdateHPBar(float CurrentHP, float MaxHP)
    {
        slider.value = CurrentHP/MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.transform.rotation;
        Target.position = Target.position + offset;
    }
}
