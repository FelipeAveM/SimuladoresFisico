using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class DataGame : System.Object{

	[SerializeField]
    public List<MatrizMedidores[]> listaFinalMedidores;
	[SerializeField]
	public List<MatrizRiesgosFisico[]> matrizFinalRiesgosFisicos;

	[SerializeField]
	public MatrizMedidores matrizMedidoresEmp1_1, matrizMedidoresEmp1_2, matrizMedidoresEmp1_3, matrizMedidoresEmp1_4,
	matrizMedidoresEmp1_5, matrizMedidoresEmp1_6, matrizMedidoresEmp1_7, matrizMedidoresEmp1_8;
	
	[SerializeField]
	public MatrizMedidores matrizMedidoresEmp2_1, matrizMedidoresEmp2_2, matrizMedidoresEmp2_3, matrizMedidoresEmp2_4,
	matrizMedidoresEmp2_5, matrizMedidoresEmp2_6, matrizMedidoresEmp2_7, matrizMedidoresEmp2_8;

	[SerializeField]
	public MatrizRiesgosFisico matrizRiesgosFisico;
	public DataGame(){
	}
	public DataGame(List<MatrizMedidores[]> listaFinalMedidores){
		this.listaFinalMedidores = listaFinalMedidores;
	}

	public DataGame(List<MatrizMedidores[]> listaFinalMedidores, List<MatrizRiesgosFisico[]> matrizFinalRiesgosFisicos){
		this.listaFinalMedidores = listaFinalMedidores;
		this.matrizFinalRiesgosFisicos = matrizFinalRiesgosFisicos;
    }

	public List<MatrizMedidores[]> getListaFinalMedidores() {
		return this.listaFinalMedidores;
	}

	public void setListaFinalMedidores(List<MatrizMedidores[]> listaFinalMedidores) {
		this.listaFinalMedidores = listaFinalMedidores;
	}

	public List<MatrizRiesgosFisico[]> getMatrizFinalRiesgosFisicos() {
		return this.matrizFinalRiesgosFisicos;
	}

	public void setMatrizFinalRiesgosFisicos(List<MatrizRiesgosFisico[]> matrizFinalRiesgosFisicos) {
		this.matrizFinalRiesgosFisicos = matrizFinalRiesgosFisicos;
	}

	public void setMatrizMedidores1emp(){
		matrizMedidoresEmp1_1 = listaFinalMedidores[0][0];
		matrizMedidoresEmp1_2 = listaFinalMedidores[0][1];
		matrizMedidoresEmp1_3 = listaFinalMedidores[0][2];
		matrizMedidoresEmp1_4 = listaFinalMedidores[0][3];
		matrizMedidoresEmp1_5 = listaFinalMedidores[0][4];
		matrizMedidoresEmp1_6 = listaFinalMedidores[0][5];
		matrizMedidoresEmp1_7 = listaFinalMedidores[0][6];
		matrizMedidoresEmp1_8 = listaFinalMedidores[0][7];
	}
	public void setMatrizMedidores2emp(){
		matrizMedidoresEmp2_1 = listaFinalMedidores[1][0];
		matrizMedidoresEmp2_2 = listaFinalMedidores[1][1];
		matrizMedidoresEmp2_3 = listaFinalMedidores[1][2];
		matrizMedidoresEmp2_4 = listaFinalMedidores[1][3];
		matrizMedidoresEmp2_5 = listaFinalMedidores[1][4];
		matrizMedidoresEmp2_6 = listaFinalMedidores[1][5];
		matrizMedidoresEmp2_7 = listaFinalMedidores[1][6];
		matrizMedidoresEmp2_8 = listaFinalMedidores[1][7];
	}
}

[System.Serializable]
public class DataPlayer : System.Object{
    
    [SerializeField]
    public string user_id; 
    [SerializeField]
    public string nombre; 
    [SerializeField]
    public string apellido; 
    [SerializeField]
    public string correo; 
    [SerializeField]
    public string grupo; 
    [SerializeField]
    public string sub_grupo; 
    [SerializeField]
    public bool is_groupal;
	[SerializeField]
    public bool is_finished = false;
    [SerializeField]
    public int fichaActual; 
	[SerializeField]
    public int emp1;
    [SerializeField]
    public int emp2; 
	/*Es un string no un MatrizMedidores
    [SerializeField]
    public List<MatrizMedidores[]> listaFinalMedidores;
	[SerializeField]
	public List<MatrizRiesgosFisico[]> matrizFinalRiesgosFisicos;
	*/
	[SerializeField]
	public DataGame data;

	public DataPlayer(string user_id, string nombre, string apellido, string correo, string grupo, string sub_grupo, bool is_groupal, int fichaActual, int emp1, int emp2 
		, DataGame data){//, List<MatrizMedidores[]> listaFinalMedidores, List<MatrizRiesgosFisico[]> matrizFinalRiesgosFisicos){
        this.user_id = user_id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.correo = correo;
        this.grupo = grupo;
        this.sub_grupo = sub_grupo;
        this.is_groupal = is_groupal;
        this.fichaActual = fichaActual;
		this.emp1 = emp1;
		this.emp2 = emp2;
		this.data = data;
		//this.listaFinalMedidores = listaFinalMedidores;
		//this.matrizFinalRiesgosFisicos = matrizFinalRiesgosFisicos;
    }
    public string getUser_id() {
		return this.user_id;
	}

	public void setUser_id(string user_id) {
		this.user_id = user_id;
	}

	public string getNombre() {
		return this.nombre;
	}

	public void setNombre(string nombre) {
		this.nombre = nombre;
	}

	public string getApellido() {
		return this.apellido;
	}

	public void setApellido(string apellido) {
		this.apellido = apellido;
	}

	public string getCorreo() {
		return this.correo;
	}

	public void setCorreo(string correo) {
		this.correo = correo;
	}

	public string getGrupo() {
		return this.grupo;
	}

	public void setGrupo(string grupo) {
		this.grupo = grupo;
	}

	public string getSub_grupo() {
		return this.sub_grupo;
	}

	public void setSub_grupo(string sub_grupo) {
		this.sub_grupo = sub_grupo;
	}

	public bool isIs_groupal() {
		return this.is_groupal;
	}

	public void setIs_groupal(bool is_groupal) {
		this.is_groupal = is_groupal;
	}

	public bool isFinished() {
		return this.is_finished;
	}

	public void setIsFinished(bool is_finished) {
		this.is_finished = is_finished;
	}

	public int getFichaActual() {
		return this.fichaActual;
	}

	public void setFichaActual(int fichaActual) {
		this.fichaActual = fichaActual;
	}

	public int getEmp1() {
		return this.emp1;
	}

	public void setEmp1(int emp1) {
		this.emp1 = emp1;
	}
	public int getEmp2() {
		return this.emp2;
	}

	public void setEmp2(int emp2) {
		this.emp2 = emp2;
	}
	/*
	public List<MatrizMedidores[]> getListaFinalMedidores() {
		return this.listaFinalMedidores;
	}

	public void setListaFinalMedidores(List<MatrizMedidores[]> listaFinalMedidores) {
		this.listaFinalMedidores = listaFinalMedidores;
	}

	public List<MatrizRiesgosFisico[]> getMatrizFinalRiesgosFisicos() {
		return this.matrizFinalRiesgosFisicos;
	}

	public void setMatrizFinalRiesgosFisicos(List<MatrizRiesgosFisico[]> matrizFinalRiesgosFisicos) {
		this.matrizFinalRiesgosFisicos = matrizFinalRiesgosFisicos;
	}
	*/

	public DataGame getDataGame() {
		return this.data;
	}

	public void setDataGame(DataGame data) {
		this.data = data;
	}

}