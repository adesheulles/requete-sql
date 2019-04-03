using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GesperLibrary
{
    public class Employe
    {
        private Service service;
        private List<Diplome> lesDiplomes;
        private int id;
        private string nom;
        private string prenom;
        private double salaire;
        private byte cadre;
        private string sexe;
        public Employe(int id, string nom, string prenom, double salaire, byte cadre, string sexe,Service service)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.salaire = salaire;
            this.cadre = cadre;
            this.sexe = sexe;
            lesDiplomes = new List<Diplome>();
            this.service = service;
        }
        public int Id{get { return id; }}
        public string Nom{ get { return nom; } }
        public string Prenom{get { return prenom; }}
        public double Salaire{ get { return salaire; }}
        public byte Cadre{get { return cadre; }}
        public string Sexe {get { return sexe; }}
        internal Service Service{get { return service; }}
        internal List<Diplome> LesDiplomes{get { return lesDiplomes; }set { lesDiplomes = value; }}
        public void LesDiplomeEmployeAdd(Diplome diplome)
        {
            lesDiplomes.Add(diplome);
        }
        public string AfficherDiplome()
        {
            string s = ""+this.Nom+" "+this.Prenom+" :\n";
            foreach (Diplome d in lesDiplomes)
            {
                s += string.Format("id :{0} libelle : {1}\n", d.Id,d.Libelle);
            }
            return s;
        }
        public override string ToString()
        {
            return string.Format("id :{0} nom : {1} prenom : {2} salaire : {3} cadre : {4} sexe : {5}",this.id,this.nom,this.prenom,this.salaire,this.cadre,this.sexe);
        }
        public int CountDiplome()
        {
            return lesDiplomes.Count;
        }
    }
}
