using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RISK_NAME_GRUPAL
{
    Aspergillus_Niger,
    Balantidiumcoli,
    Clostridium_Tetani,
    Erisipela,
    Hepatitis,
    Tuberculosis_TBC,
    Toxoplasma,
    Plasmodium_Malariae,
    Giardia,
    Virus_de_la_rabia,
    Aedes_aegypti,
    Brucellla,
    Bacteria_Salmonellla,
    Candidiasis,
    Proyección_de_Fluidos,
    Explosión_de_Autoclave,
    Material_Sólido_Cortopunzante,
    Elementos_de_Máquina,
    Piso_Húmedo,
    Desorden,
    Tensión_Electrica,
    Disenteria_Amíbica,
    Virus_de_la_gripa,
    Bacillus_Anthracis,
    Gusano_de_seda,
    Penicillium,
    Cladosporium,
    Orina_Ratón,
	//Quimicos Laboratorio
	Acetona,
	Ácido_Clorhídrico,
	Ácido_Sulfúrico,
	Sulfato_de_Sodio,
	Tolueno,
	Benceno,
	Éter_Dietílico,
    //Seguridad Servicios
    Condiciones_de_orden_y_aseo,
    Caída_de_objetos,
    Accidente,
    Contacto_partes_calientes_máquina,
    Trabajo_en_alturas,
    Orden_y_aseo,
    Material_proyectado_en_polvo,
    //Seguridad Manufactura
    Elementos_móviles_de_máquina,
    Explosión,
    Charco_jabón,
    Incendio,
    Condiciones_de_orden,
    //Seguridad Pozo
    Charco_de_grasa,
    Material_proyectado,
    Fuga_de_combustible,
    Corto_Tensión_Alta,
    Almacenamiento,
    Herramientas_Piezas,
    //Biologicos Servicios
    Staphylococcus_Aureus,
    Stachybotrys,
    Pseudomona,
    Leptospira,
    Plasmodium,
    Aspergillus,
    //Quimicos Manufactura
    Clorobenceno,
    Ftalatos,
    Alquifenoles,
    Colorantes_Azoicos,
    Formaldehidos,
    Detergente,
    Desengrasante_Industrial,
    //Químicos Servicios
    AcidoMuriatico,
    FibraSintetica,
    Hipoclorito,
    Ambientadores,
    PinturasConDisolventes,
    AcidoOxalico,
    //Químicos Pozo
    FugaDiesel,
    SulfatoDeAlumino,
    SodaCaustica,
    AcidoAceticoGlacial,
    OxidoDeCalcio,
    NitratoDeAlumino,
    CarbonatoDeSodio
     


}


[CreateAssetMenu(fileName = "Data", menuName = "Settings/risk_card_lab", order = 1)]
public class RiskCardGrupalObject : ScriptableObject{

    [SerializeField]
    private RISK_NAME_GRUPAL title = RISK_NAME_GRUPAL.Aspergillus_Niger;

    [SerializeField]
    private Sprite imageResource;

	[SerializeField]
	public Transition[] transitions;

	[SerializeField]
	public List<string> ppm;

	[SerializeField]
	public List<string> mg;

	[SerializeField]
	public string tls;

    public RISK_NAME_GRUPAL Title
    {
        get { return title; }
    }

    public Sprite ImageResource
    {
        get { return imageResource; }
    }

	public List<string> PPM
	{
		get { return ppm; }
	}
		
	public List<string> MG
	{
		get { return mg; }
	}
		
	public string TLS
	{
		get { return tls; }
	}
}
