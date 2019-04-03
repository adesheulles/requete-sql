using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GesperLibrary
{
     public class Service
    {
        private List<Employe> lesEmployes;
        private double budget;
        private int capacite;
        private string designation;
        private int id;
        private string produit;
        private char type;
        public Service(int id, string designation, int capacite, string produit, char type)
        {
            this.capacite = capacite;
            this.designation = designation;
            this.produit = produit;
            this.type = type;
            this.id = id;
            this.budget = 0;
            lesEmployes = new List<Employe>();
        }
        public Service(int id, int budget, string designation,  char type)
        {
            this.produit = "";
            this.capacite = 0;
            this.budget = budget;
            this.designation = designation;
            this.type = type;
            this.id = id;
            lesEmployes = new List<Employe>();
        }
        public double Budget{get { return budget; } }
        public int Capacite{ get { return capacite; }}
        public string Designation{get { return designation; }}
        public int Id{ get { return id; }}
        public string Produit{get { return produit; }}
        public char Type{get { return type; }}
        internal List<Employe> LesEmployes { get { return lesEmployes; } }
        public void LesEmployesServiceAdd(Employe e)
        {
            lesEmployes.Add(e);
        }
        public string AfficherEmploye()
        {
            string s = ""+this.Designation+" :\n";
            foreach (Employe e in lesEmployes)
            {
                s += string.Format("id : {0} nom : {1} prenom : {2} sexe : {3} cadre : {4} salaire : {5}\n", e.Id, e.Nom, e.Prenom, e.Sexe, e.Cadre, e.Salaire);
            }
            return s;
        }
        public override string ToString()
        {
            if (type == 'P')
            {
                return string.Format("id :{0} designation : {1} produit : {2} type : {3} budget : {4}", this.id, this.designation, this.produit, this.type, this.capacite);
            }
            else
            {
                return string.Format("id :{0} designation : {1}  type : {2} budget : {3}", this.id, this.designation, this.type, this.budget);
            }
        }
    }

}
