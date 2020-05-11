using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData : System.Object
{
    public string Id;
    public string Nombre;
    public string Apellido;
    public string Correo;
    public int RolId;
    public string Rol;
    public string Guid;
    public string Grupo;
    public string SubGrupo;
    public bool IsGrupal;
    public int Risk;

    public string _Id
    {
        get { return Id; }
        set { Id = value; }
    }
    public string _Nombre
    {
        get { return Nombre; }
        set { Nombre = value; }
    }
    public string _Apellido
    {
        get { return Apellido; }
        set { Apellido = value; }
    }
    public string _Correo
    {
        get { return Correo; }
        set { Correo = value; }
    }
    public string _Grupo
    {
        get { return Grupo; }
        set { Grupo = value; }
    }
    public string _SubGrupo
    {
        get { return SubGrupo; }
        set { SubGrupo = value; }
    }
    public bool _IsGrupal
    {
        get { return IsGrupal; }
        set { IsGrupal = value; }
    }
    public string _Guid
    {
        get { return Guid; }
        set { Guid = value; }
    }
    public int _Risk
    {
        get { return Risk; }
        set { Risk = value; }
    }
}