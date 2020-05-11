using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using FMODUnity;

public enum WORKERS_OFFICE {
    Adolfo,
    Adrian,
    Alejandra,
    Alirio,
    Alfonso,
    Amparito,
    Andres,
    Camilo,
    Edgar,
    Elber,
    Felipe,
    Francisco,
    Gerardo,
    Henry,
    Jairo,
    Jhon_jairo,
    Jorge,
    Jose,
    Juan,
    Karla,
    Karlina,
    Nicolas,
    Maritza,
    Mario,
    Martha,
    Mary,
    Monica,
    Laura,
    Leidy,
	Pilar,
    Sonia,
    Wilder,
    Yamid,
    }

[CreateAssetMenu(fileName = "Data", menuName = "Settings/worker_card", order = 1)]
public class WorkerCardObject : ScriptableObject {

	[SerializeField]
	private WORKERS_OFFICE nameWorker = WORKERS_OFFICE.Sonia;

	[SerializeField]
    [EventRef]
    private string audioPath;

	[SerializeField]
	private Sprite imageCharacter;

	[SerializeField]
	public File2 response;

	public Sprite ImageCharacter {
		get { return imageCharacter;}
	}

	public string AudioPath {
		get { return audioPath;}
	}

	public WORKERS_OFFICE Name {
		get { return nameWorker;}
	}
}
