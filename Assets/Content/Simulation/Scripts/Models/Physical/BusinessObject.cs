using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum BUSINESS
{
    CENTRO_DE_DIAGNÓSTICO_MÉDICO,
    MANUFACTURA_INDUSTRIAS_AXEN,
    OFICINA_COMPAÑÍA_CONSTRUIMOS_TU_HOGAR,
    POZO_PETROLERO_PETROCOL,
    SERVICIOS_GENERALES_GRUPO_ALPHA
}

[System.Serializable]
public class Section : System.Object {
    [SerializeField]
    private string sectionName;

    [SerializeField]
    private Sprite imageResource;

    public string SectionName
    {
        get { return sectionName; }
    }

    public Sprite ImageResource
    {
        get { return imageResource; }
    }
}

[CreateAssetMenu(fileName = "Data", menuName = "Settings/busines_card", order = 1)]
public class BusinessObject : ScriptableObject
{

    [SerializeField]
    public BUSINESS name = BUSINESS.CENTRO_DE_DIAGNÓSTICO_MÉDICO;

    [SerializeField]
    public Sprite imageResource;

    [SerializeField]
    public List<Section> sections = new List<Section>();
        
   public BUSINESS Name
    {
        get { return name; }
    }
        
    public Sprite ImageResource
    {
        get { return imageResource; }
    }

    public List<Section> Sections {
        get { return sections;  }
        set { sections = value; }
    }
}
