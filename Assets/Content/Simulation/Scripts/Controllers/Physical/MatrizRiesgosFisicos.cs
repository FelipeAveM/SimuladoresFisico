using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class MatrizRiesgosFisico : System.Object{

    [SerializeField]
    public string empresa;
    [SerializeField]
    public string area;
    [SerializeField]
    public string riesgo;
    [SerializeField]
    public string descRiesgo;
    [SerializeField]
    public string efectosPosibles;
    [SerializeField]
    public int nd;  
    [SerializeField]
    public string ndCuali;
    [SerializeField]
    public int ne;
    [SerializeField]
    public string neCuali;
    [SerializeField]
    public int proba;
    [SerializeField]
    public string impactProba;
    [SerializeField]
    public int nc;
    [SerializeField]
    public string ncCuali;
    [SerializeField]
    public int nr;
    [SerializeField]
    public string nrInter;
    [SerializeField]        
    public string aceptabilidadRiesgo;
    [SerializeField]
    public int numEmpleados;
    [SerializeField]
    public string porConsecuencia;
    [SerializeField]
    public string existenciaRequisito;
    [SerializeField]
    public string sustitucion;
    [SerializeField]
    public string eliminacion;
    [SerializeField]
    public string controles;
    [SerializeField]
    public string senalizacion;
    [SerializeField]
    public string epp;


    public MatrizRiesgosFisico(string empresa, string area, string riesgo, string descRiesgo, string efectosPosibles, int nd, string ndCuali, int ne, string neCuali, int proba, string impactProba, int nc, string ncCuali, int nr, string nrInter, string aceptabilidadRiesgo, int numEmpleados, string porConsecuencia, string existenciaRequisito, string sustitucion, string eliminacion, string controles, string senalizacion, string epp){
        this.empresa = empresa;
        this.area = area;
        this.riesgo = riesgo;
        this.descRiesgo = descRiesgo;
        this.efectosPosibles = efectosPosibles;
        this.nd = nd;
        this.ndCuali = ndCuali;
        this.ne = ne;
        this.neCuali = neCuali;
        this.proba = proba;
        this.impactProba = impactProba;
        this.nc = nc;
        this.ncCuali = ncCuali;
        this.nr = nr;
        this.nrInter = nrInter;
        this.aceptabilidadRiesgo = aceptabilidadRiesgo;
        this.numEmpleados = numEmpleados;
        this.porConsecuencia = porConsecuencia;
        this.existenciaRequisito = existenciaRequisito;
        this.sustitucion = sustitucion;
        this.eliminacion = eliminacion;
        this.controles = controles;
        this.epp = epp;
        this.empresa = empresa;
        this.area = area;
        this.riesgo = riesgo;
        this.descRiesgo = descRiesgo;
        this.efectosPosibles = efectosPosibles;
        this.nd = nd;
        this.ndCuali = ndCuali;
        this.ne = ne;
        this.neCuali = neCuali;
        this.proba = proba;
        this.impactProba = impactProba;
        this.nc = nc;
        this.ncCuali = ncCuali;
        this.nr = nr;
        this.nrInter = nrInter;
        this.aceptabilidadRiesgo = aceptabilidadRiesgo;
        this.numEmpleados = numEmpleados;
        this.porConsecuencia = porConsecuencia;
        this.existenciaRequisito = existenciaRequisito;
        this.sustitucion = sustitucion;
        this.eliminacion = eliminacion;
        this.controles = controles;
        this.senalizacion = senalizacion;
        this.epp = epp;
    }



	public string getEmpresa() {
		return this.empresa;
	}

	public void setEmpresa(string empresa) {
		this.empresa = empresa;
	}

	public string getArea() {
		return this.area;
	}

	public void setArea(string area) {
		this.area = area;
	}

	public string getRiesgo() {
		return this.riesgo;
	}

	public void setRiesgo(string riesgo) {
		this.riesgo = riesgo;
	}

	public string getDescRiesgo() {
		return this.descRiesgo;
	}

	public void setDescRiesgo(string descRiesgo) {
		this.descRiesgo = descRiesgo;
	}

	public string getEfectosPosibles() {
		return this.efectosPosibles;
	}

	public void setEfectosPosibles(string efectosPosibles) {
		this.efectosPosibles = efectosPosibles;
	}

	public int getNd() {
		return this.nd;
	}

	public void setNd(int nd) {
		this.nd = nd;
	}

	public string getNdCuali() {
		return this.ndCuali;
	}

	public void setNdCuali(string ndCuali) {
		this.ndCuali = ndCuali;
	}

	public int getNe() {
		return this.ne;
	}

	public void setNe(int ne) {
		this.ne = ne;
	}

	public string getNeCuali() {
		return this.neCuali;
	}

	public void setNeCuali(string neCuali) {
		this.neCuali = neCuali;
	}

	public int getProba() {
		return this.proba;
	}

	public void setProba(int proba) {
		this.proba = proba;
	}

	public string getImpactProba() {
		return this.impactProba;
	}

	public void setImpactProba(string impactProba) {
		this.impactProba = impactProba;
	}

	public int getNc() {
		return this.nc;
	}

	public void setNc(int nc) {
		this.nc = nc;
	}

	public string getNcCuali() {
		return this.ncCuali;
	}

	public void setNcCuali(string ncCuali) {
		this.ncCuali = ncCuali;
	}

	public int getNr() {
		return this.nr;
	}

	public void setNr(int nr) {
		this.nr = nr;
	}

	public string getNrInter() {
		return this.nrInter;
	}

	public void setNrInter(string nrInter) {
		this.nrInter = nrInter;
	}

	public string getAceptabilidadRiesgo() {
		return this.aceptabilidadRiesgo;
	}

	public void setAceptabilidadRiesgo(string aceptabilidadRiesgo) {
		this.aceptabilidadRiesgo = aceptabilidadRiesgo;
	}

	public int getNumEmpleados() {
		return this.numEmpleados;
	}

	public void setNumEmpleados(int numEmpleados) {
		this.numEmpleados = numEmpleados;
	}

	public string getPorConsecuencia() {
		return this.porConsecuencia;
	}

	public void setPorConsecuencia(string porConsecuencia) {
		this.porConsecuencia = porConsecuencia;
	}

	public string getExistenciaRequisito() {
		return this.existenciaRequisito;
	}

	public void setExistenciaRequisito(string existenciaRequisito) {
		this.existenciaRequisito = existenciaRequisito;
	}

	public string getSustitucion() {
		return this.sustitucion;
	}

	public void setSustitucion(string sustitucion) {
		this.sustitucion = sustitucion;
	}

	public string getEliminacion() {
		return this.eliminacion;
	}

	public void setEliminacion(string eliminacion) {
		this.eliminacion = eliminacion;
	}

	public string getControles() {
		return this.controles;
	}

	public void setControles(string controles) {
		this.controles = controles;
	}

    public string getSenalizacion() {
		return this.senalizacion;
	}

	public void setSenalizacion(string senalizacion) {
		this.senalizacion = senalizacion;
	}

	public string getEpp() {
		return this.epp;
	}

	public void setEpp(string epp) {
		this.epp = epp;
	}
}

[System.Serializable]
public class DatosPruebaUsuario : System.Object{

    [SerializeField]
    public string user_id = "usuarioPoli";
    [SerializeField]
    public string grupo = "Fisico";
	[SerializeField]
    public string sub_grupo = "Laboratorio";
    [SerializeField]
    public bool is_groupal = false;
    [SerializeField]
    public bool is_finished = false;

}