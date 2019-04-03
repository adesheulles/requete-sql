using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GesperLibrary
{
    public class Diplome
    {
        private List<Employe> lesEmployes;
        private int id;
        private string libelle;
        public Diplome(int id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
            lesEmployes = new List<Employe>();
        }
        public int Id{get { return id; }}
        public string Libelle{get { return libelle; } }
        internal List<Employe> LesEmployes{get { return lesEmployes; } }
        public void lesEmployesAdd(Employe e)
        {
            lesEmployes.Add(e);
        }
        public string AfficherEmploye()
        {
            string s = ""+this.Libelle+" :\n";
            foreach (Employe e in lesEmployes)
            {
                s +=  string.Format("id : {0} nom : {1} prenom : {2} sexe : {3} cadre : {4} salaire : {5}\n", e.Id, e.Nom, e.Prenom, e.Sexe, e.Cadre, e.Salaire);
            }
            return s;
        }
        public override string ToString()
        {
            return string.Format("id :{0} libelle : {1}\n",this.id,this.libelle);
        }
    }
}
