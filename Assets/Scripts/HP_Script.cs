using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_Script : Entity
{
    // Start is called before the first frame update
    public Slider slider;
    public Gradient gradient;
    public Image Fill;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        Fill.color = gradient.Evaluate(1f);

    }

    public void SetHealth(int health)
    {
        slider.value = health;

        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
