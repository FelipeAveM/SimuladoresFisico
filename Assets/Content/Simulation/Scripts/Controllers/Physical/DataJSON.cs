using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class DataJSON : System.Object{
	
    [SerializeField]
    public int id;
    [SerializeField]
    public string simulator;
    [SerializeField]
    public string simulation;
    [SerializeField]
    public bool is_groupal;
    [SerializeField]
    public DataGame dataSim;
    [SerializeField]
    public string user_id;
    /*[SerializeField]
    public Date created_at;
    [SerializeField]
    public Date updated_at;*/
    
    public DataJSON(){
    }

    public DataJSON(int id, string simulator, string simulation, bool is_groupal, 
                    DataGame dataSim, string user_id){ //, Date created_at, Date updated_at) {
        this.id = id;
        this.simulator = simulator;
        this.simulation = simulation;
        this.is_groupal = is_groupal;
        this.dataSim = dataSim;
        this.user_id = user_id;
        //this.created_at = created_at;
        //this.updated_at = updated_at;
    }
	public int getId() {
		return this.id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public string getSimulator() {
		return this.simulator;
	}

	public void setSimulator(string simulator) {
		this.simulator = simulator;
	}

	public string getSimulation() {
		return this.simulation;
	}

	public void setSimulation(string simulation) {
		this.simulation = simulation;
	}

	public bool isIs_groupal() {
		return this.is_groupal;
	}

	public void setIs_groupal(bool is_groupal) {
		this.is_groupal = is_groupal;
	}

	public DataGame getDataSim() {
		return this.dataSim;
	}

	public void setDataSim(DataGame dataSim) {
		this.dataSim = dataSim;
	}

	public string getUser_id() {
		return this.user_id;
	}

	public void setUser_id(string user_id) {
		this.user_id = user_id;
	}    
}

