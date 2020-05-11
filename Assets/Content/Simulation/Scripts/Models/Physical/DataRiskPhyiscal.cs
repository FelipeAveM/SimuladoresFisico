using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[System.Serializable]
public class DataRiskPhysical : System.Object {

    public int limitBusiness = 0;
    /*
   public int limitWorkers = 0;
   public int simulator = 0; //0: Biologicos, 1: Seguridad, 2: Quimicos
   public int index = 0;
   public bool isLocal = false;
   */

   //Archivo con la información de la empresa
   [SerializeField]
   readonly List<File1Physical> file1 =  new List<File1Physical>();
   
    /*
    //Workes
   [SerializeField]
   List<File2> file2 =  new List<File2>();
   //Evaluation
   [SerializeField]
   List<File3> file3 =  new List<File3>();
   //Table Evaluation
   [SerializeField]
   List<File4> file4 =  new List<File4>();
   //Controls
   [SerializeField]
   List<File5> file5 =  new List<File5>();

   [SerializeField]
   List<FileEvaluation> fileEvaluation = new List<FileEvaluation>();

   [SerializeField]
   List<FileEvaluatiomAnnexed> fileEvaluatiomAnnexed = new List<FileEvaluatiomAnnexed>();

   [SerializeField]
   List<FileEvaluationSecurity> fileEvaluationSecurity = new List<FileEvaluationSecurity>();

   [SerializeField]
   List<File3AChemistry> fileEvaluation3AChemistry = new List<File3AChemistry>();

   [SerializeField]
   List<File3BChemistry> fileEvaluation3BChemistry = new List<File3BChemistry>();

   [SerializeField]
   List<File3CChemistry> fileEvaluation3CChemistry = new List<File3CChemistry>();

   [SerializeField]
   List<FileControls> fileControls = new List<FileControls>();
   */

   public DataRiskPhysical(int business)
   {
       this.limitBusiness = business;
   }


   public List<File1Physical> File1 {
       get{ return file1;}
   }
   
    /*
   public List<File2> File2 {
       get{ return file2;}
   }

   public List<File3> File3 {
       get{ return file3;}
       set{ file3 = value;}
   }

   public List<File4> File4 {
       get{ return file4;}
       set{ file4 = value;}
   }

   public List<File5> File5 {
       get{ return file5;}
       set{ file5 = value;}
   }

   public List<FileEvaluation> FileEvaluation
   {
       get { return fileEvaluation; }
       set { fileEvaluation = value; }
   }

   public List<FileEvaluatiomAnnexed> FileEvaluatiomAnnexed
   {
       get { return fileEvaluatiomAnnexed; }
       set { fileEvaluatiomAnnexed = value; }
   }

   public List<FileEvaluationSecurity> FileEvaluationSecurity
   {
       get { return fileEvaluationSecurity; }
       set { fileEvaluationSecurity = value; }
   }

   public List<File3AChemistry> FileEvaluation3AChemistry
   {
       get { return fileEvaluation3AChemistry; }
       set { fileEvaluation3AChemistry = value; }
   }

   public List<File3BChemistry> FileEvaluation3BChemistry
   {
       get { return fileEvaluation3BChemistry; }
       set { fileEvaluation3BChemistry = value; }
   }

   public List<File3CChemistry> FileEvaluation3CChemistry
   {
       get { return fileEvaluation3CChemistry; }
       set { fileEvaluation3CChemistry = value; }
   }

   public List<FileControls> FileControls
   {
       get { return fileControls; }
       set { fileControls = value; }
   }

   public void AddFileRisk (File1 file){
       int index = file1.FindIndex(obj => obj.Risk == file.Risk);

       if (index < 0) {
           file1.Add (file);
       } else {
           file1 [index] = file;
       }
   }

   public void AddFileRisk3C (File3CChemistry file){
       int index = fileEvaluation3CChemistry.FindIndex(obj => obj.Risk == file.Risk);

       if (index < 0) {
           fileEvaluation3CChemistry.Add (file);
       } else {
           fileEvaluation3CChemistry [index] = file;
       }
   }

   public File1 GetRisk(string risk)
   {
       int index = file1.FindIndex(obj => obj.Risk == risk);
       if (index >= 0)
       {
           return file1[index];
       }
       return null;
   }

   public File3CChemistry GetRisk3C(string risk)
   {
       int index = fileEvaluation3CChemistry.FindIndex(obj => obj.Risk == risk);
       if (index >= 0)
       {
           return fileEvaluation3CChemistry[index];
       }
       return null;
   }

   public bool CheckLimitRisk(){
       return file1.Count >= limitRisk ;
   }

   public bool CheckLimitRisk3C(){
       return fileEvaluation3CChemistry.Count >= limitRisk ;
   }

   public void AddFileWorkers (File2 file){
       int index = file2.FindIndex(obj => obj.referenceName == file.referenceName);
       if (index < 0) {
           file2.Add (file);
       } else {
           file2 [index] = file;
       }
   }

   public File2 GetWorkers(string referenceName)
   {
       int index = file2.FindIndex(obj => obj.referenceName == referenceName);
       if (index >= 0)
       {
           return file2 [index];
       }
       return null;
   }

   public bool CheckLimitWorkers(){
       return file2.Count >= limitWorkers ;
   }

   public void AddEvaluation (File3 file){
       file3.Add (file);
   }

   public void AddEvaluationTable (File4 file){
       file4.Add (file);
   }

   public void AddControls (File5 file){
       file5.Add (file);
   }
   */
}
