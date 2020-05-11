using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHighlighter : MonoBehaviour {

	public const string colorName = "_EmissionColor";
	public Color hightColor;
	[HideInInspector]
	public bool isHight = false;
    public ParticleSystem particlesMaterial;
    public Color baseColor;

	void OnStart()
	{
		Highlighter (transform, baseColor);
	}

    public void Highlighter(Transform element, Color color){

        // Change material emision in parent
        Renderer rendererParent = element.GetComponent<Renderer>();
        if (rendererParent != null)
        {
            foreach (var shared in rendererParent.sharedMaterials)
            {
                shared.SetColor(colorName, color);
            }
        }

        // Change emision in childs
        foreach (Transform item in element)
        {
            Renderer renderer = item.GetComponent<Renderer>();
            if (renderer != null)
            {
                foreach (var shared in renderer.sharedMaterials)
                {
                    if (shared != null)
                    {
                        shared.SetColor(colorName, color);
                    }
                }
            }
        }
       
        // Change Particle System
        if (particlesMaterial != null)
        {
            var main = particlesMaterial.main;

            if (color == Color.black)
            {
                main.startColor = baseColor;
            }
            else
            {
                main.startColor = color;
            }

        }

        // Toggle color
		if (color == Color.black) {
			isHight = false;
		} else {
			isHight = true;
		}
	}

	void OnApplicationQuit()
	{
		Highlighter (transform, baseColor);
	}
}
