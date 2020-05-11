using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class MatrizMedidores : System.Object{

    [SerializeField]
    public string empresa;
    [SerializeField]
    public string area;
    [SerializeField]
    public string [] mediciones;
    [SerializeField]
    public int numPersonas;
    [SerializeField]
    public int horasTrabajadas;
    [SerializeField]
    public string luxometro;
    [SerializeField]
    public bool riesgo;
    [SerializeField]
    public bool rutinaria;
    [SerializeField]
    public string sonometro;
    [SerializeField]
    public string cantidadRuido;
    [SerializeField]
    public bool riesgoSono;
    [SerializeField]
    public bool rutinariaSono;
    [SerializeField]
    public string dosimetro;
    [SerializeField]
    public bool esIonizante;
    [SerializeField]
    public bool riesgoDosi;
    [SerializeField]
    public bool rutinariaDosi;
    [SerializeField]
    public string termometro;
    [SerializeField]
    public string temperaturaArea;
    [SerializeField]
    public bool riesgoTermo;
    [SerializeField]
    public bool rutinariaTermo;
    [SerializeField]
    public string vibrometro;
    [SerializeField]
    public bool vibraManoBrazo;
    [SerializeField]
    public bool vibraCuerpo;
    [SerializeField]
    public bool riesgoVibro;
    [SerializeField]
    public bool rutinariaVibro;
    [SerializeField]
    public string controlRiesgoLux = "No es necesario un control";
    [SerializeField]
    public string controlRiesgoSono = "No es necesario un control";
    [SerializeField]
    public string controlRiesgoDosi = "No es necesario un control";
    [SerializeField]
    public string controlRiesgoTermo = "No es necesario un control";
    [SerializeField]
    public string controlRiesgoVibro = "No es necesario un control";  
    [SerializeField]
    public MatrizMedidores(string empresa, string area, string[] mediciones, int numPersonas, 
    int horasTrabajadas, string luxometro, bool riesgo, bool rutinaria, string sonometro, /*string cantidadRuido,*/
    bool riesgoSono, bool rutinariaSono, string dosimetro, bool esIonizante, 
    bool riesgoDosi, bool rutinariaDosi, string termometro, /*string temperaturaArea,*/
    bool riesgoTermo, bool rutinariaTermo, string vibrometro, bool vibraManoBrazo, 
    bool vibraCuerpo, bool riesgoVibro, bool rutinariaVibro)
    {
        this.empresa = empresa;
        this.area = area;
        this.mediciones = mediciones;
        this.numPersonas = numPersonas;
        this.horasTrabajadas = horasTrabajadas;
        this.luxometro = luxometro;
        this.riesgo = riesgo;
        this.rutinaria = rutinaria;
        this.sonometro = sonometro;
        //this.cantidadRuido = cantidadRuido;
        this.riesgoSono = riesgoSono;
        this.rutinariaSono = rutinariaSono;
        this.dosimetro = dosimetro;
        this.esIonizante = esIonizante;
        this.riesgoDosi = riesgoDosi;
        this.rutinariaDosi = rutinariaDosi;
        this.termometro = termometro;
        //this.temperaturaArea = temperaturaArea;
        this.riesgoTermo = riesgoTermo;
        this.rutinariaTermo = rutinariaTermo;
        this.vibrometro = vibrometro;
        this.vibraManoBrazo = vibraManoBrazo;
        this.vibraCuerpo = vibraCuerpo;
        this.riesgoVibro = riesgoVibro;
        this.rutinariaVibro = rutinariaVibro;
    }

    public MatrizMedidores(string empresa, string area, string[] mediciones, int numPersonas, 
    int horasTrabajadas, string luxometro, bool riesgo, bool rutinaria, string sonometro, /*string cantidadRuido,*/
    bool riesgoSono, bool rutinariaSono, string dosimetro, bool esIonizante, 
    bool riesgoDosi, bool rutinariaDosi, string termometro, /*string temperaturaArea,*/
    bool riesgoTermo, bool rutinariaTermo, string vibrometro, bool vibraManoBrazo, 
    bool vibraCuerpo, bool riesgoVibro, bool rutinariaVibro, string controlRiesgoLux, string controlRiesgoSono,
    string controlRiesgoDosi, string controlRiesgoTermo, string controlRiesgoVibro)
    {
        this.empresa = empresa;
        this.area = area;
        this.mediciones = mediciones;
        this.numPersonas = numPersonas;
        this.horasTrabajadas = horasTrabajadas;
        this.luxometro = luxometro;
        this.riesgo = riesgo;
        this.rutinaria = rutinaria;
        this.sonometro = sonometro;
        //this.cantidadRuido = cantidadRuido;
        this.riesgoSono = riesgoSono;
        this.rutinariaSono = rutinariaSono;
        this.dosimetro = dosimetro;
        this.esIonizante = esIonizante;
        this.riesgoDosi = riesgoDosi;
        this.rutinariaDosi = rutinariaDosi;
        this.termometro = termometro;
        //this.temperaturaArea = temperaturaArea;
        this.riesgoTermo = riesgoTermo;
        this.rutinariaTermo = rutinariaTermo;
        this.vibrometro = vibrometro;
        this.vibraManoBrazo = vibraManoBrazo;
        this.vibraCuerpo = vibraCuerpo;
        this.riesgoVibro = riesgoVibro;
        this.rutinariaVibro = rutinariaVibro;
        this.controlRiesgoLux = controlRiesgoLux;
        this.controlRiesgoLux = controlRiesgoLux;
        this.controlRiesgoDosi = controlRiesgoDosi;
        this.controlRiesgoTermo = controlRiesgoTermo;
        this.controlRiesgoVibro = controlRiesgoVibro;
    }

    public MatrizMedidores()
    {

    }
    public string getEmpresa()
    {
        return this.empresa;
    }

    public void setEmpresa(string empresa)
    {
        this.empresa = empresa;
    }

    public string getArea()
    {
        return this.area;
    }

    public void setArea(string area)
    {
        this.area = area;
    }
     public string [] getMediciones()
    {
        return this.mediciones;
    }

    public void setMediciones(string [] mediciones)
    {
        this.mediciones = mediciones;
    }

    public int getNumPersonas()
    {
        return this.numPersonas;
    }

    public void setNumPersonas(int numPersonas)
    {
        this.numPersonas = numPersonas;
    }

    public int getHorasTrabajadas()
    {
        return this.horasTrabajadas;
    }

    public void setHorasTrabajadas(int horasTrabajadas)
    {
        this.horasTrabajadas = horasTrabajadas;
    }

    public string getLuxometro()
    {
        return this.luxometro;
    }

    public void setLuxometro(string luxometro)
    {
        this.luxometro = luxometro;
    }

    public bool getRiesgo()
    {
        return this.riesgo;
    }

    public void isRiesgo(bool riesgo)
    {
        this.riesgo = riesgo;
    }

    public bool getRutinaria()
    {
        return this.rutinaria;
    }

    public void isRutinaria(bool rutinaria)
    {
        this.rutinaria = rutinaria;
    }

    public string getSonometro()
    {
        return this.sonometro;
    }

    public void setSonometro(string sonometro)
    {
        this.sonometro = sonometro;
    }
    
    public string getCantidadRuido()
    {
        return this.cantidadRuido;
    }
    public void setCantidadRuido(string cantidadRuido)
    {
        this.cantidadRuido = cantidadRuido;
    }
     
    public bool getRiesgoSono()
    {
        return this.riesgoSono;
    }

    public void isRiesgoSono(bool riesgoSono)
    {
        this.riesgoSono = riesgoSono;
    }

    public bool getRutinariaSono()
    {
        return this.rutinariaSono;
    }

    public void isRutinariaSono(bool rutinariaSono)
    {
        this.rutinariaSono = rutinariaSono;
    }

    public string getDosimetro()
    {
        return this.dosimetro;
    }

    public void setDosimetro(string dosimetro)
    {
        this.dosimetro = dosimetro;
    }

    public bool getEsIonizante()
    {
        return this.esIonizante;
    }

    public void isEsIonizante(bool esIonizante)
    {
        this.esIonizante = esIonizante;
    }

    public bool getRiesgoDosi()
    {
        return this.riesgoDosi;
    }

    public void isRiesgoDosi(bool riesgoDosi)
    {
        this.riesgoDosi = riesgoDosi;
    }

    public bool getRutinariaDosi()
    {
        return this.rutinariaDosi;
    }

    public void isRutinariaDosi(bool rutinariaDosi)
    {
        this.rutinariaDosi = rutinariaDosi;
    }

    public string getTermometro()
    {
        return this.termometro;
    }

    public void setTermometro(string termometro)
    {
        this.termometro = termometro;
    }
    
    public string getTemperaturaArea()
    {
        return this.temperaturaArea;
    }

    public void setTemperaturaArea(string temperaturaArea)
    {
        this.temperaturaArea = temperaturaArea;
    }
    
    public bool getRiesgoTermo()
    {
        return this.riesgoTermo;
    }

    public void isRiesgoTermo(bool riesgoTermo)
    {
        this.riesgoTermo = riesgoTermo;
    }

    public bool getRutinariaTermo()
    {
        return this.rutinariaTermo;
    }

    public void isRutinariaTermo(bool rutinariaTermo)
    {
        this.rutinariaTermo = rutinariaTermo;
    }

    public string getVibrometro()
    {
        return this.vibrometro;
    }

    public void setVibrometro(string vibrometro)
    {
        this.vibrometro = vibrometro;
    }

    public bool getVibraManoBrazo()
    {
        return this.vibraManoBrazo;
    }

    public void isVibraManoBrazo(bool vibraManoBrazo)
    {
        this.vibraManoBrazo = vibraManoBrazo;
    }

    public bool getVibraCuerpo()
    {
        return this.vibraCuerpo;
    }

    public void isVibraCuerpo(bool vibraCuerpo)
    {
        this.vibraCuerpo = vibraCuerpo;
    }

    public bool getRiesgoVibro()
    {
        return this.riesgoVibro;
    }

    public void isRiesgoVibro(bool riesgoVibro)
    {
        this.riesgoVibro = riesgoVibro;
    }

    public bool getRutinariaVibro()
    {
        return this.rutinariaVibro;
    }

    public void isRutinariaVibro(bool rutinariaVibro)
    {
        this.rutinariaVibro = rutinariaVibro;
    }
    public string getControlRiesgoLux() {
		return this.controlRiesgoLux;
	}

	public void setControlRiesgoLux(string controlRiesgoLux) {
		this.controlRiesgoLux = controlRiesgoLux;
	}

	public string getControlRiesgoSono() {
		return this.controlRiesgoSono;
	}

	public void setControlRiesgoSono(string controlRiesgoSono) {
		this.controlRiesgoSono = controlRiesgoSono;
	}

	public string getControlRiesgoDosi() {
		return this.controlRiesgoDosi;
	}

	public void setControlRiesgoDosi(string controlRiesgoDosi) {
		this.controlRiesgoDosi = controlRiesgoDosi;
	}

	public string getControlRiesgoTermo() {
		return this.controlRiesgoTermo;
	}

	public void setControlRiesgoTermo(string controlRiesgoTermo) {
		this.controlRiesgoTermo = controlRiesgoTermo;
	}

	public string getControlRiesgoVibro() {
		return this.controlRiesgoVibro;
	}

	public void setControlRiesgoVibro(string controlRiesgoVibro) {
		this.controlRiesgoVibro = controlRiesgoVibro;
	}



}