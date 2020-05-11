using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum Risk_Name {
    Ácaros_del_polvo,
    Pared_húmeda_Penicillium,
    Agua_del_garrafón_Escherichiacoli,
    Corriente_de_aire_acondicionado,
	Papel_Asperguillus,
    Balde_con_Desinfectante,
    Piso_mojado,
    Polvo_Superficies,
    Desinfectante,
    Desengrasante,
    Aire_Acondicionado,
    Spray,
    Extintor,
    Guillotina_y_bisturi,
    Cables_expuestos_Interruptor,
    Cajas_AZ_y_papel,
    Escalón_de_entrada,
    Corto_circuito_Multitoma,
    Ruido_exterior,
    Deslumbramiento_en_puesto_de_trabajo
}

public enum ANSWER_CLASSIFICATION {
	Bacterias,
    Fibras,
    Fluidos_y_excrementos,
    Gases_y_vapores,
    Hongos,
    Humos_metálicos_no_metálicos,
    Líquidos_Nieblas_y_rocíos,
    Material_Particulado,
    Parasitos,
    Picaduras,
    Polvos_orgánicos_e_inorgánicos,
    Rickettsias,
    Virus,
    No_es_un_riesgo_Biologico,
    No_es_un_riesgo_químico,
    No_es_un_riesgo_de_seguridad,    
    Tecnológico_Incendio,
    Eléctrico,
    Mecánico,
    Locativo_Orden_y_aseo,
    Accidentes_de_tránsito,
	Locativo_Superficies_irregulares
}

public enum CONTROL {
    Aumento_de_ventilación,
    Cambio_de_ambientador,
    Cambio_de_desengrasante,
    Cambio_de_proveedor,
    Cambio_desinfectante,
    Cambio_del_dispensador,
    Cambio_del_proceso_de_limpieza,
    Control_de_temperatura,
    Formación_e_información,
    Fumigación_con_aerosol,
    Inspecciones_locativas,
    Instalación_de_ventiladores,
    Lijar_y_pintar,
    Limpieza_y_desinfección,
    Mantenimiento_AC,
    Ventilacion_adecuación_ventanas,
    No_es_necesario,
	Adquirir_papel_de_diferentes_tamaños_y_uso_de_tijeras,
	Capacitaciones_sobre_manejo_de_herramientas_manuales,
	Guante_desechable,
	Contratar_personal_certificado_para_trabajos_eléctricos,
	Capacitar_al_trabajador_en_trabajos_eléctricos,
	Dotar_de_herramientas_dieléctricas,
	Estandarización_de_lugares_para_almacenamiento,
	Programa_de_orden_y_aseo,
	Mayor_frecuencia_en_la_recoleccion_de_basura_y_mas_canecas_disponibles,
	Realizacion_de_rampa_para_acceder_a_la_oficina,
	Señalizar_y_demarcar_el_escalon,
	Capacitacion_en_prevención_de_riesgos,
	Canalizar_conexiones_eléctricas,
	Desconectar_los_equipos_que_no_se_encuentren_en_uso,
	Realizar_inspecciones_de_riesgo_eléctrico,
	Mantenimiento_del_dispensador
}

public enum CONTROL_CLASSIFICATION {
    Sustitución,
    Eliminación,
    Administrativo,
    Ingenería,
    Equipos,
    No_es_necesario,
}

[CreateAssetMenu(fileName = "Data", menuName = "Settings/risk_card", order = 1)]
public class RiskCardObject : ScriptableObject {

	[SerializeField]
	private Risk_Name title = Risk_Name.Ácaros_del_polvo;

	[SerializeField]
	private Sprite imageResource ;

	[SerializeField]
	private ANSWER_CLASSIFICATION answerClassification;

	[SerializeField]
	private LEVEL_OF_RISK  levelOfRisk = LEVEL_OF_RISK.IV; 

	[SerializeField]
	private CONTROL  control = CONTROL.Aumento_de_ventilación;

	[SerializeField]
	private CONTROL_CLASSIFICATION  controlClassification= CONTROL_CLASSIFICATION.Sustitución; 

	[SerializeField]
	private LEVEL_OF_RISK[] levelOfRiskValid; 

	[SerializeField]
	private CONTROL[] controlsValid; 

	[SerializeField]
	private CONTROL_CLASSIFICATION[]  controlClassificationValids;

    [SerializeField]
    public Transition[] transitions;

    public Risk_Name Title {
		get { return title; }
	}

	public Sprite ImageResource {
		get { return imageResource;}
	}

	public ANSWER_CLASSIFICATION AnswerClassification {
		get { return answerClassification; }
	}

	public LEVEL_OF_RISK LevelOfRisk {
		get { return levelOfRisk; }
		set { levelOfRisk = value; }
	}

	public CONTROL Control {
		get { return control; }
		set { control = value; }
	}

	public CONTROL_CLASSIFICATION ControlClassification {
		get { return controlClassification; }
		set { controlClassification = value; }
	}

	public LEVEL_OF_RISK[] LevelOfRiskValid {
		get { return levelOfRiskValid; }
		set { levelOfRiskValid = value; }
	}

	public CONTROL[] ControlsValid {
		get { return controlsValid; }
		set { controlsValid = value; }
	}

	public CONTROL_CLASSIFICATION[] ControlClassificationValids {
		get { return controlClassificationValids; }
		set { controlClassificationValids = value; }
	}
}