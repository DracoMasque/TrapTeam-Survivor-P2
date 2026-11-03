using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JaugeElement : MonoBehaviour
{
    public static JaugeElement instance;
    [SerializeField] public Dictionary<Elements, Slider> elementJauge = new Dictionary<Elements, Slider>
    {
        {Elements.Feu, null},
        {Elements.Glace, null},
        {Elements.Nature, null},
        {Elements.Maudit, null}
    };
    [SerializeField] private Dictionary<Elements, GameObject> elementFrameUI = new Dictionary<Elements, GameObject>
    {
        {Elements.Feu, null},
        {Elements.Glace, null},
        {Elements.Nature, null},
        {Elements.Maudit, null}
    };
    [SerializeField] private Dictionary<Elements, GameObject> elementLevelUI = new Dictionary<Elements, GameObject>
    {
        {Elements.Feu, null},
        {Elements.Glace, null},
        {Elements.Nature, null},
        {Elements.Maudit, null}
    };

    [SerializeField] private Dictionary<int, Sprite> elementLevelTextures = new Dictionary<int, Sprite>
    {
        {0, null},
        {1, null},
        {2, null},
        {3, null},
        {4, null}
    };
    
    void Start()
    {
        instance = this;
        RefreshJauge(elementJauge.Keys.ToArray());
    }

    public void RefreshJauge(Elements[] elements)
    {
        foreach (Elements e in elements)
        {
            elementJauge[e].value = ElementManager.instance.elementRatios[e];
            RefreshLevel(elements);
            RefreshFrame(elements);
        }
    }
    
    
    //Match le level de chaques element dans le UI
    private void RefreshLevel(Elements[] elements)
    {
        foreach (Elements e in elements)
        {
            int level = ElementManager.instance.elementLevels[e];
            elementLevelUI[e].GetComponent<Image>().sprite = elementLevelTextures[level];
        }
    }
    //match le frame du BuildingMode avec l'element le plus présent pour le UI
    private void RefreshFrame(Elements[] elements)
    {
        //find the highest value element
        //if an element's value is equal to another the frame will always be null
        Elements highestElement = Elements.None;
        float highestValue = 0;
        List<float> values = new List<float>();
        foreach (Elements e in elements)
        {
            if (e != Elements.None)
            {
                values.Add(ElementManager.instance.elementRatios[e]);
                if (ElementManager.instance.elementRatios[e] > highestValue)
                {
                    highestElement = e;
                    highestValue = ElementManager.instance.elementRatios[e];
                } 
                else if (ElementManager.instance.elementRatios[e] == highestValue && ElementManager.instance.elementRatios[e] > 0)
                {
                    highestElement = Elements.None;
                    break;
                }
            }
        }
        
        //Now that we have our overarching element we can set the frame
        switch (highestElement)
        {
            case Elements.None:
                HideFrame(Elements.Feu);
                HideFrame(Elements.Glace);
                HideFrame(Elements.Nature);
                HideFrame(Elements.Maudit);
                break;
            case Elements.Feu:
                ShowFrame(Elements.Feu);
                HideFrame(Elements.Glace);
                HideFrame(Elements.Nature);
                HideFrame(Elements.Maudit);
                break;
            case Elements.Glace:
                HideFrame(Elements.Feu);
                ShowFrame(Elements.Glace);
                HideFrame(Elements.Nature);
                HideFrame(Elements.Maudit);
                break;
            case Elements.Nature:
                HideFrame(Elements.Feu);
                HideFrame(Elements.Glace);
                ShowFrame(Elements.Nature);
                HideFrame(Elements.Maudit);
                break;
            case Elements.Maudit:
                print("frame maudit");
                HideFrame(Elements.Feu);
                HideFrame(Elements.Glace);
                HideFrame(Elements.Nature);
                ShowFrame(Elements.Maudit);
                break;
                
        }
        
    }

    private Tween fadeTweenFire;
    private Tween fadeTweenGlace;
    private Tween fadeTweenNature;
    private Tween fadeTweenMaudit;
    //Tweens Frame's image color into opaque
    private void ShowFrame(Elements element)
    {
        print("SHOW");
        var frame = elementFrameUI[element];
        Image frameImage = frame.GetComponent<Image>();
        var goalColor = new Color(1, 1, 1, 1);

        switch (element)
        {
            case Elements.Feu:
                FadeFire(frameImage, goalColor, 1);
                break;
            case Elements.Glace:
                FadeGlace(frameImage, goalColor, 1);
                break;
            case Elements.Nature:
                FadeNature(frameImage, goalColor, 1);
                break;
            case Elements.Maudit:
                FadeMaudit(frameImage, goalColor, 1);
                break;
        }
    }
    //Tweens Frame's image color into transparent
    private void HideFrame(Elements element)
    {
        print("HIDE");
        GameObject frame = elementFrameUI[element];
        Image frameImage = frame.GetComponent<Image>();
        var goalColor = new Color(1, 1, 1, 0);
        
        switch (element)
        {
            case Elements.Feu:
                FadeFire(frameImage, goalColor, 1);
                break;
            case Elements.Glace:
                FadeGlace(frameImage, goalColor, 1);
                break;
            case Elements.Nature:
                FadeNature(frameImage, goalColor, 1);
                break;
            case Elements.Maudit:
                FadeMaudit(frameImage, goalColor, 1);
                break;
        }
    }

    private void FadeFire(Image image, Color endValue, float duration)
    {
        if (fadeTweenFire != null)
        {
            fadeTweenFire.Kill(false);//boolean asks if it should evoke the callback or not if we added one through fadeTween.onComplete += onEnd; TweenCallback on End
        }
        fadeTweenFire = image.GetComponent<Image>().DOColor(endValue, duration);
    }
    private void FadeGlace(Image image, Color endValue, float duration)
    {
        if (fadeTweenGlace != null)
        {
            fadeTweenGlace.Kill();
        }
        fadeTweenGlace = image.GetComponent<Image>().DOColor(endValue, duration);
    }
    private void FadeNature(Image image, Color endValue, float duration)
    {
        if (fadeTweenNature != null)
        {
            fadeTweenNature.Kill();
        }
        fadeTweenNature = image.GetComponent<Image>().DOColor(endValue, duration);
    }
    private void FadeMaudit(Image image, Color endValue, float duration)
    {
        if (fadeTweenMaudit != null)
        {
            fadeTweenMaudit.Kill();
        }
        fadeTweenMaudit = image.GetComponent<Image>().DOColor(endValue, duration);
    }
}
