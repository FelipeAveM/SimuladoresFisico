using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {

	public static string  DataSerialization(Document document){
		return JsonUtility.ToJson (document).Replace("\\","").Replace("\"data\":\"","\"data\":").Replace(",end\"","").Replace("\"rows\":\"","\"rows\":");
	}

}
