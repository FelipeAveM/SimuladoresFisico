using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class DataGame : System.Object{

	[SerializeField]
	public int fichaActual; 
	[SerializeField]
    public int emp1;
    [SerializeField]
    public int emp2;
	[SerializeField]
    public List<MatrizMedidores[]> listaFinalMedidores = new List<MatrizMedidores[]>();
	[SerializeField]
	public List<MatrizRiesgosFisico[]> matrizFinalRiesgosFisicos = new List<MatrizRiesgosFisico[]>();
	[SerializeField]
	public MatrizMedidores matrizMedidoresEmp1_1, matrizMedidoresEmp1_2, matrizMedidoresEmp1_3, matrizMedidoresEmp1_4,
	matrizMedidoresEmp1_5, matrizMedidoresEmp1_6, matrizMedidoresEmp1_7, matrizMedidoresEmp1_8;
	[SerializeField]
	public MatrizMedidores matrizMedidoresEmp2_1, matrizMedidoresEmp2_2, matrizMedidoresEmp2_3, matrizMedidoresEmp2_4,
	matrizMedidoresEmp2_5, matrizMedidoresEmp2_6, matrizMedidoresEmp2_7, matrizMedidoresEmp2_8;
	[SerializeField]
	public static MatrizRiesgosFisico matrizRiesgosFisico1_1, matrizRiesgosFisico1_2, matrizRiesgosFisico1_3, matrizRiesgosFisico1_4, matrizRiesgosFisico1_5, matrizRiesgosFisico1_6, matrizRiesgosFisico1_7, matrizRiesgosFisico1_8, matrizRiesgosFisico1_9, matrizRiesgosFisico1_10, 
	matrizRiesgosFisico1_11, matrizRiesgosFisico1_12, matrizRiesgosFisico1_13, matrizRiesgosFisico1_14, matrizRiesgosFisico1_15, matrizRiesgosFisico1_16, matrizRiesgosFisico1_17, matrizRiesgosFisico1_18, matrizRiesgosFisico1_19, matrizRiesgosFisico1_20, 
	matrizRiesgosFisico1_21, matrizRiesgosFisico1_22, matrizRiesgosFisico1_23, matrizRiesgosFisico1_24, matrizRiesgosFisico1_25, matrizRiesgosFisico1_26, matrizRiesgosFisico1_27, matrizRiesgosFisico1_28, matrizRiesgosFisico1_29, matrizRiesgosFisico1_30, 
	matrizRiesgosFisico1_31, matrizRiesgosFisico1_32, matrizRiesgosFisico1_33, matrizRiesgosFisico1_34, matrizRiesgosFisico1_35, matrizRiesgosFisico1_36, matrizRiesgosFisico1_37, matrizRiesgosFisico1_38, matrizRiesgosFisico1_39, matrizRiesgosFisico1_40;
	[SerializeField]
	public static MatrizRiesgosFisico matrizRiesgosFisico2_1, matrizRiesgosFisico2_2, matrizRiesgosFisico2_3, matrizRiesgosFisico2_4, matrizRiesgosFisico2_5, matrizRiesgosFisico2_6, matrizRiesgosFisico2_7, matrizRiesgosFisico2_8, matrizRiesgosFisico2_9, matrizRiesgosFisico2_10, 
	matrizRiesgosFisico2_11, matrizRiesgosFisico2_12, matrizRiesgosFisico2_13, matrizRiesgosFisico2_14, matrizRiesgosFisico2_15, matrizRiesgosFisico2_16, matrizRiesgosFisico2_17, matrizRiesgosFisico2_18, matrizRiesgosFisico2_19, matrizRiesgosFisico2_20, 
	matrizRiesgosFisico2_21, matrizRiesgosFisico2_22, matrizRiesgosFisico2_23, matrizRiesgosFisico2_24, matrizRiesgosFisico2_25, matrizRiesgosFisico2_26, matrizRiesgosFisico2_27, matrizRiesgosFisico2_28, matrizRiesgosFisico2_29, matrizRiesgosFisico2_30, 
	matrizRiesgosFisico2_31, matrizRiesgosFisico2_32, matrizRiesgosFisico2_33, matrizRiesgosFisico2_34, matrizRiesgosFisico2_35, matrizRiesgosFisico2_36, matrizRiesgosFisico2_37, matrizRiesgosFisico2_38, matrizRiesgosFisico2_39, matrizRiesgosFisico2_40;
	public MatrizRiesgosFisico[] matrizRiesgosFisico1 = {matrizRiesgosFisico1_1, matrizRiesgosFisico1_2, matrizRiesgosFisico1_3, matrizRiesgosFisico1_4, matrizRiesgosFisico1_5, matrizRiesgosFisico1_6, matrizRiesgosFisico1_7, matrizRiesgosFisico1_8, matrizRiesgosFisico1_9, matrizRiesgosFisico1_10, 
	matrizRiesgosFisico1_11, matrizRiesgosFisico1_12, matrizRiesgosFisico1_13, matrizRiesgosFisico1_14, matrizRiesgosFisico1_15, matrizRiesgosFisico1_16, matrizRiesgosFisico1_17, matrizRiesgosFisico1_18, matrizRiesgosFisico1_19, matrizRiesgosFisico1_20, 
	matrizRiesgosFisico1_21, matrizRiesgosFisico1_22, matrizRiesgosFisico1_23, matrizRiesgosFisico1_24, matrizRiesgosFisico1_25, matrizRiesgosFisico1_26, matrizRiesgosFisico1_27, matrizRiesgosFisico1_28, matrizRiesgosFisico1_29, matrizRiesgosFisico1_30, 
	matrizRiesgosFisico1_31, matrizRiesgosFisico1_32, matrizRiesgosFisico1_33, matrizRiesgosFisico1_34, matrizRiesgosFisico1_35, matrizRiesgosFisico1_36, matrizRiesgosFisico1_37, matrizRiesgosFisico1_38, matrizRiesgosFisico1_39, matrizRiesgosFisico1_40};
	public MatrizRiesgosFisico[] matrizRiesgosFisico2 = {matrizRiesgosFisico1_1, matrizRiesgosFisico1_2, matrizRiesgosFisico1_3, matrizRiesgosFisico1_4, matrizRiesgosFisico1_5, matrizRiesgosFisico1_6, matrizRiesgosFisico1_7, matrizRiesgosFisico1_8, matrizRiesgosFisico1_9, matrizRiesgosFisico1_10, 
	matrizRiesgosFisico1_11, matrizRiesgosFisico1_12, matrizRiesgosFisico1_13, matrizRiesgosFisico1_14, matrizRiesgosFisico1_15, matrizRiesgosFisico1_16, matrizRiesgosFisico1_17, matrizRiesgosFisico1_18, matrizRiesgosFisico1_19, matrizRiesgosFisico1_20, 
	matrizRiesgosFisico1_21, matrizRiesgosFisico1_22, matrizRiesgosFisico1_23, matrizRiesgosFisico1_24, matrizRiesgosFisico1_25, matrizRiesgosFisico1_26, matrizRiesgosFisico1_27, matrizRiesgosFisico1_28, matrizRiesgosFisico1_29, matrizRiesgosFisico1_30, 
	matrizRiesgosFisico1_31, matrizRiesgosFisico1_32, matrizRiesgosFisico1_33, matrizRiesgosFisico1_34, matrizRiesgosFisico1_35, matrizRiesgosFisico1_36, matrizRiesgosFisico1_37, matrizRiesgosFisico1_38, matrizRiesgosFisico1_39, matrizRiesgosFisico1_40};

	public DataGame(){
	}
	public DataGame(List<MatrizMedidores[]> listaFinalMedidores){
		this.listaFinalMedidores = listaFinalMedidores;
	}
	public DataGame(int fichaActual, int emp1, int emp2, List<MatrizMedidores[]> listaFinalMedidores, List<MatrizRiesgosFisico[]> matrizFinalRiesgosFisicos){
		this.fichaActual = fichaActual;
		this.emp1 = emp1;
		this.emp2 = emp2;
		this.listaFinalMedidores = listaFinalMedidores;
		this.matrizFinalRiesgosFisicos = matrizFinalRiesgosFisicos;
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
	public void setMM(int state){
		if(state == 2){
			setMR1();
			MatrizMedidores [] mM = {matrizMedidoresEmp1_1, matrizMedidoresEmp1_2, matrizMedidoresEmp1_3, matrizMedidoresEmp1_4,
									matrizMedidoresEmp1_5, matrizMedidoresEmp1_6, matrizMedidoresEmp1_7, matrizMedidoresEmp1_8};
			listaFinalMedidores.Add(mM);
			Ficha1Fisicos.empresasCompletadas = 1;
			//setMatrizRiesgos1emp();
			MatrizMedidores [] mM2 = {matrizMedidoresEmp2_1, matrizMedidoresEmp2_2, matrizMedidoresEmp2_3, matrizMedidoresEmp2_4,
									matrizMedidoresEmp2_5, matrizMedidoresEmp2_6, matrizMedidoresEmp2_7, matrizMedidoresEmp2_8};
			listaFinalMedidores.Add(mM2);
		}
		else if(state == 1){
			MatrizMedidores [] mM1 = {matrizMedidoresEmp1_1, matrizMedidoresEmp1_2, matrizMedidoresEmp1_3, matrizMedidoresEmp1_4,
									matrizMedidoresEmp1_5, matrizMedidoresEmp1_6, matrizMedidoresEmp1_7, matrizMedidoresEmp1_8};
			listaFinalMedidores.Add(mM1);
		}
		else{
			listaFinalMedidores.Clear();
		}
	}
	public void setMM2(){
		Debug.Log("MM2");
		MatrizMedidores [] mM2 = {matrizMedidoresEmp2_1, matrizMedidoresEmp2_2, matrizMedidoresEmp2_3, matrizMedidoresEmp2_4,
								 matrizMedidoresEmp2_5, matrizMedidoresEmp2_6, matrizMedidoresEmp2_7, matrizMedidoresEmp2_8};
		listaFinalMedidores.Add(mM2);
		for (int i = 0; i < FlowControllerPhysical.data.listaFinalMedidores.Count; i++){
			Debug.Log("Matriz: " + i);
			for (int j = 0; j < FlowControllerPhysical.data.listaFinalMedidores[i].Length; j++){
				Debug.Log(JsonUtility.ToJson(FlowControllerPhysical.data.listaFinalMedidores[i][j]));
			}
		}
	}
	public void setMR1(){
		MatrizRiesgosFisico [] mR = {matrizRiesgosFisico1_1, matrizRiesgosFisico1_2, matrizRiesgosFisico1_3, matrizRiesgosFisico1_4, matrizRiesgosFisico1_5, matrizRiesgosFisico1_6, matrizRiesgosFisico1_7, matrizRiesgosFisico1_8, matrizRiesgosFisico1_9, matrizRiesgosFisico1_10, 
			matrizRiesgosFisico1_11, matrizRiesgosFisico1_12, matrizRiesgosFisico1_13, matrizRiesgosFisico1_14, matrizRiesgosFisico1_15, matrizRiesgosFisico1_16, matrizRiesgosFisico1_17, matrizRiesgosFisico1_18, matrizRiesgosFisico1_19, matrizRiesgosFisico1_20, 
			matrizRiesgosFisico1_21, matrizRiesgosFisico1_22, matrizRiesgosFisico1_23, matrizRiesgosFisico1_24, matrizRiesgosFisico1_25, matrizRiesgosFisico1_26, matrizRiesgosFisico1_27, matrizRiesgosFisico1_28, matrizRiesgosFisico1_29, matrizRiesgosFisico1_30, 
			matrizRiesgosFisico1_31, matrizRiesgosFisico1_32, matrizRiesgosFisico1_33, matrizRiesgosFisico1_34, matrizRiesgosFisico1_35, matrizRiesgosFisico1_36, matrizRiesgosFisico1_37, matrizRiesgosFisico1_38, matrizRiesgosFisico1_39, matrizRiesgosFisico1_40};
		matrizFinalRiesgosFisicos.Add(mR);
	}
	public void setMR2(){
		Debug.Log("MR2");
		MatrizRiesgosFisico [] mR2 = {matrizRiesgosFisico2_1, matrizRiesgosFisico2_2, matrizRiesgosFisico2_3, matrizRiesgosFisico2_4, matrizRiesgosFisico2_5, matrizRiesgosFisico2_6, matrizRiesgosFisico2_7, matrizRiesgosFisico2_8, matrizRiesgosFisico2_9, matrizRiesgosFisico2_10, 
			matrizRiesgosFisico2_11, matrizRiesgosFisico2_12, matrizRiesgosFisico2_13, matrizRiesgosFisico2_14, matrizRiesgosFisico2_15, matrizRiesgosFisico2_16, matrizRiesgosFisico2_17, matrizRiesgosFisico2_18, matrizRiesgosFisico2_19, matrizRiesgosFisico2_20, 
			matrizRiesgosFisico2_21, matrizRiesgosFisico2_22, matrizRiesgosFisico2_23, matrizRiesgosFisico2_24, matrizRiesgosFisico2_25, matrizRiesgosFisico2_26, matrizRiesgosFisico2_27, matrizRiesgosFisico2_28, matrizRiesgosFisico2_29, matrizRiesgosFisico2_30, 
			matrizRiesgosFisico2_31, matrizRiesgosFisico2_32, matrizRiesgosFisico2_33, matrizRiesgosFisico2_34, matrizRiesgosFisico2_35, matrizRiesgosFisico2_36, matrizRiesgosFisico2_37, matrizRiesgosFisico2_38, matrizRiesgosFisico2_39, matrizRiesgosFisico2_40};
		matrizFinalRiesgosFisicos.Add(mR2);
		for (int i = 0; i < matrizFinalRiesgosFisicos.Count; i++){
            Debug.Log("Matriz: " + i);
            for (int j = 0; j < matrizFinalRiesgosFisicos[i].Length; j++){
                if(matrizFinalRiesgosFisicos[i][j] != null)
					Debug.Log(JsonUtility.ToJson(matrizFinalRiesgosFisicos[i][j]));
            }
        }
		setMatrizRiesgos2emp();

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

	public void setMatrizRiesgos1emp(){
		for (int i = 0; i < matrizFinalRiesgosFisicos[0].Length; i++){
			matrizRiesgosFisico1[i] = matrizFinalRiesgosFisicos[0][i];
		}
	}
	public void setMatrizRiesgos2emp(){
		for (int i = 0; i < matrizFinalRiesgosFisicos[1].Length; i++){
			matrizRiesgosFisico2[i] = matrizFinalRiesgosFisicos[1][i];
		}

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
   
	/*Es un string no un MatrizMedidores
    [SerializeField]
    public List<MatrizMedidores[]> listaFinalMedidores;
	[SerializeField]
	public List<MatrizRiesgosFisico[]> matrizFinalRiesgosFisicos;
	*/
	[SerializeField]
	public DataGame data;

	public DataPlayer(){
		
	}

	public DataPlayer(string user_id, string nombre, string apellido, string correo, string grupo, string sub_grupo, bool is_groupal, DataGame data){
        this.user_id = user_id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.correo = correo;
        this.grupo = grupo;
        this.sub_grupo = sub_grupo;
        this.is_groupal = is_groupal;
		this.data = data;
		//this.listaFinalMedidores = listaFinalMedidores;
		//this.matrizFinalRiesgosFisicos = matrizFinalRiesgosFisicos;
    }

	public DataPlayer(string user_id, string nombre, string apellido, string correo, string grupo, string sub_grupo, bool is_groupal
		){//, DataGame data){//, List<MatrizMedidores[]> listaFinalMedidores, List<MatrizRiesgosFisico[]> matrizFinalRiesgosFisicos){
        this.user_id = user_id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.correo = correo;
        this.grupo = grupo;
        this.sub_grupo = sub_grupo;
        this.is_groupal = is_groupal;
		//this.data = data;
		//this.listaFinalMedidores = listaFinalMedidores;
		//this.matrizFinalRiesgosFisicos = matrizFinalRiesgosFisicos;
    }

	public void setData(string user_id, string nombre, string apellido, string correo, string grupo, string sub_grupo, bool is_groupal
		, DataGame data){//, List<MatrizMedidores[]> listaFinalMedidores, List<MatrizRiesgosFisico[]> matrizFinalRiesgosFisicos){
        this.user_id = user_id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.correo = correo;
        this.grupo = grupo;
        this.sub_grupo = sub_grupo;
        this.is_groupal = is_groupal;
		this.data = data;
		//this.listaFinalMedidores = listaFinalMedidores;
		//this.matrizFinalRiesgosFisicos = matrizFinalRiesgosFisicos;
    }
	public void setData1(string user_id, string nombre, string apellido, string correo, string grupo, string sub_grupo, bool is_groupal){
        this.user_id = user_id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.correo = correo;
        this.grupo = grupo;
        this.sub_grupo = sub_grupo;
        this.is_groupal = is_groupal;
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