using System.Data.SqlClient;
using System;
namespace ombyvery;

public class Declaration{
    public String idPerte {get; set;}
    public String idOmby {get; set;}
    public String idLieu {get; set;}
    public int etat {get; set;}
    public DateTime datePerte {get; set;}
    public DateTime? dateRetrouver {get; set;} 

    public Declaration(){

    }

    public Declaration(String idPerte,String idOmby,String idLieu,int etat,DateTime dperte,DateTime dRetrouv){
        this.idPerte = idPerte;
        this.idOmby = idOmby;
        this.idLieu = idLieu;
        this.etat = etat;
        this.datePerte = datePerte;
        this.dateRetrouver = dateRetrouver;
    }

    public void insertDeclaration(Declaration decla){
        
    }
}