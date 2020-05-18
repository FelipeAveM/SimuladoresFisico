using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[SerializeField]
public class MatrizMedidoresPrueba : System.Object {
    [SerializeField]
    public static string [] med = {"12", "10", "11", "9", "7"};
    [SerializeField]
    public static MatrizMedidores m1 = new MatrizMedidores(
        "Laboratorio", "Recepcion", med, 2, 12, "12", true, true, 
        "10",  false, false, "11", false, false, false,
        "9",  false, false, "7", false, false, false, false
        //,"asdasd", "asdasd", "sdasñdlm", "No es un control necesario", "asdasdasd"
    );
    /*
    [SerializeField]
    public static MatrizMedidores m1 = new MatrizMedidores(
        "Laboratorio", "Recepcion", med, 2, 12, "12", true, false, 
        "10",  false, false, "11", false, false, false,
        "9",  false, false, "7", false, false, false, false
        );
        */
    [SerializeField]
    public static MatrizMedidores m2 = new MatrizMedidores(
        "Laboratorio", "Sala de espera", med, 3, 12, "12", false, false, 
        "10",  false, false, "11", false, false, false,
        "9",  false, false, "7", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m3 = new MatrizMedidores(
        "Laboratorio", "Administración", med, 1, 12, "12", false, false, 
        "10", false, false, "11", false, false, false,
        "9",  false, false, "7", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m4 = new MatrizMedidores(
        "Laboratorio", "Toma de muestras", med, 4, 12, "12", false, false, 
        "10",  false, false, "11", false, false, false,
        "9",  false, false, "7", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m5 = new MatrizMedidores(
        "Laboratorio", "Almacen", med, 6, 8, "12", false, false, 
        "10",  false, false, "11", false, false, false,
        "9",  false, false, "7", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m6 = new MatrizMedidores(
        "Laboratorio", "Esterilización", med, 5, 12, "12", false, false, 
        "10",  false, false, "11", false, false, false,
        "9",  false, false, "7", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m7 = new MatrizMedidores(
        "Laboratorio", "Baños", med, 2, 10, "12", false, false, 
        "10",  false, false, "11", false, false, false,
        "9",  false, false, "7", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m8 = new MatrizMedidores(
        "Laboratorio", "Basuras", med, 9, 12, "12", false, false, 
        "10",  false, false, "1", false, false, false,
        "9",  false, false, "7", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores [] matrizDePrueba = {m1, m2, m3, m4, m5, m6, m7, m8};
}

[SerializeField]
public class MatrizMedidoresPrueba2 : System.Object {
    [SerializeField]
    public static string [] med = {"234", "1234", "345", "19", "7"};
    [SerializeField]
    public static MatrizMedidores m1 = new MatrizMedidores(
        "Laboratorio", "Recepcion", med, 2, 12, "12", true, true, 
        "10",  false, false, "11", false, false, false,
        "9",  false, false, "7", false, false, false, false
        //,"asdasd", "asdasd", "sdasñdlm", "No es un control necesario", "asdasdasd"
    );
    /*
    [SerializeField]
    public static MatrizMedidores m1 = new MatrizMedidores(
        "Laboratorio", "Recepcion", med, 2, 12, "12", true, false, 
        "10",  false, false, "11", false, false, false,
        "9",  false, false, "7", false, false, false, false
        );
        */
    [SerializeField]
    public static MatrizMedidores m2 = new MatrizMedidores(
        "Laboratorio", "Sala de espera", med, 3, 12, "102", false, false, 
        "100",  true, true, "110", false, false, false,
        "90",  false, false, "70", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m3 = new MatrizMedidores(
        "Laboratorio", "Administración", med, 1, 12, "12", false, false, 
        "100", false, false, "101", false, false, false,
        "90",  false, false, "70", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m4 = new MatrizMedidores(
        "Laboratorio", "Toma de muestras", med, 4, 12, "102", false, false, 
        "100",  false, false, "101", false, false, false,
        "9",  false, false, "70", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m5 = new MatrizMedidores(
        "Laboratorio", "Almacen", med, 6, 8, "120", false, false, 
        "105",  false, false, "151", false, false, false,
        "95",  false, false, "57", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m6 = new MatrizMedidores(
        "Laboratorio", "Esterilización", med, 5, 12, "152", false, false, 
        "104",  false, false, "115", false, false, false,
        "69",  false, false, "57", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m7 = new MatrizMedidores(
        "Laboratorio", "Baños", med, 2, 10, "152", false, false, 
        "104",  false, false, "116", false, false, false,
        "99",  false, false, "78", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores m8 = new MatrizMedidores(
        "Laboratorio", "Basuras", med, 9, 12, "12", false, false, 
        "10",  false, false, "1", false, false, false,
        "9",  false, false, "7", false, false, false, false
    );
    [SerializeField]
    public static MatrizMedidores [] matrizDePrueba = {m1, m2, m3, m4, m5, m6, m7, m8};
}


