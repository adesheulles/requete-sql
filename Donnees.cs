using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace GesperLibrary
{
    public class Donnees
    {
        static private MySqlConnection cnx;
        static private string scnx;
        private List<Diplome> lesDiplomes;
        private List<Employe> lesEmployes;
        private List<Service> lesServices;
        public Donnees()
        {
            lesDiplomes = new List<Diplome>();
            lesEmployes = new List<Employe>();
            lesServices = new List<Service>();
            Connexion();
        }
        public void Connexion()
        {
            scnx = string.Format("user=root;password=siojjr;host=localhost;database=gespertds");
            cnx = new MySqlConnection(scnx);
        }
        public void ChargerServices()
        {
            MySqlDataReader data;
            MySqlCommand cmsql = new MySqlCommand();
            cmsql.Connection = cnx;
            cmsql.CommandText = "service";
            cmsql.CommandType = CommandType.TableDirect;
            cmsql.Connection.Open();
            data = cmsql.ExecuteReader();
            while (data.Read())
            {
                if (data.GetString(2) == "P")
                {
                    Service s = new Service(Convert.ToInt32(data.GetString(0)), data.GetString(1), Convert.ToInt32(data.GetString(4)), data.GetString(3), Convert.ToChar(data.GetString(2)));
                    lesServices.Add(s);
                }
                else
                {
                    Service s = new Service(Convert.ToInt32(data[0]), Convert.ToInt32(data[5]), Convert.ToString(data[1]), Convert.ToChar(data[2]));
                    lesServices.Add(s);
                }
            }
            cnx.Close();
        }
        public void ChargerDiplome()
        {
            MySqlDataReader data;
            MySqlCommand cmsql = new MySqlCommand();
            cmsql.Connection = cnx;
            cmsql.CommandText = "diplome";
            cmsql.CommandType = CommandType.TableDirect;
            cmsql.Connection.Open();
            data = cmsql.ExecuteReader();
            while (data.Read())
            {
                Diplome d = new Diplome((int)data[0], (string)data[1]);
                lesDiplomes.Add(d);
            }
            cnx.Close();
        }
        public void ChargerEmploye()
        {
            MySqlDataReader data;
            MySqlCommand cmsql = new MySqlCommand();
            cmsql.Connection = cnx;
            cmsql.CommandText = "employe";
            cmsql.CommandType = CommandType.TableDirect;
            cmsql.Connection.Open();
            data = cmsql.ExecuteReader();
            while (data.Read())
            {
                Employe e = new Employe(Convert.ToInt32(data[0]), Convert.ToString(data[1]), Convert.ToString(data[2]), Convert.ToDouble(data[5]), Convert.ToByte(data[4]), Convert.ToString(data[3]), lesServices[(int)data[6] - 1]);
                lesEmployes.Add(e);
            }
            cnx.Close();
        }
        public void ToutCharger()
        {
            this.ChargerServices();
            this.ChargerDiplome();
            this.ChargerEmploye();
            this.ChargerLesDiplomeDesEmployes();
            this.ChargerLesEmployesDesServices();
            this.ChargerLesEmployesTitulairesDdesDiplomes();
        }
        public string Afficher()
        {
            string final = "";
            foreach (Service s in lesServices)
            {
                final += s.ToString() + "\n";
            }
            foreach (Employe e in lesEmployes)
            {
                final += e.ToString() + "\n";
            }
            foreach (Diplome d in lesDiplomes)
            {
                final += d.ToString() + "\n";
            }
            foreach (Service s in lesServices)
            {
                final += s.AfficherEmploye();
            }
            foreach (Diplome d in lesDiplomes)
            {
                final += d.AfficherEmploye();
            }
            foreach (Employe e in lesEmployes)
            {
                final += e.AfficherDiplome();
            }
            return final;
        }
        public void ChargerLesDiplomeDesEmployes()
        {
            foreach (Diplome d in lesDiplomes)
            {
                MySqlDataReader data;
                MySqlCommand cmsql = new MySqlCommand();
                cmsql.Connection = cnx;
                cmsql.Connection.Open();
                cmsql.CommandText = "select * from posseder where pos_diplome = @id";
                cmsql.CommandType = CommandType.Text;
                cmsql.Parameters.Add("@id", MySqlDbType.Int32);
                cmsql.Parameters["@id"].Value = d.Id;
                data = cmsql.ExecuteReader();
                while (data.Read())
                {
                    foreach (Employe e in lesEmployes)
                    {
                        if (data.GetInt32(1) == e.Id)
                        {
                            d.lesEmployesAdd(e);
                        }
                    }
                }
                cnx.Close();
            }


        }
        public void ChargerLesEmployesDesServices()
        {
            int idService;
            foreach (Service s in lesServices)
            {
                idService = s.Id;
                MySqlDataReader data;
                MySqlCommand cmsql = new MySqlCommand();
                cmsql.Connection = cnx;
                cmsql.Connection.Open();
                cmsql.CommandText = "select * from employe where emp_service = @id ";
                cmsql.CommandType = CommandType.Text;
                cmsql.Parameters.Add("@id", MySqlDbType.Int32);
                cmsql.Parameters["@id"].Value = idService;
                data = cmsql.ExecuteReader();
                while (data.Read())
                {
                    foreach (Employe e in lesEmployes)
                    {
                        if (data.GetInt32(0) == e.Id)
                        {
                            s.LesEmployesServiceAdd(e);
                        }
                    }
                }
                cnx.Close();
            }
        }
        public void ChargerLesEmployesTitulairesDdesDiplomes()
        {
            foreach (Employe e in lesEmployes)
            {
                MySqlDataReader data;
                MySqlCommand cmsql = new MySqlCommand();
                cmsql.Connection = cnx;
                cmsql.Connection.Open();
                cmsql.CommandText = "select * from posseder where pos_employe = @id";
                cmsql.CommandType = CommandType.Text;
                cmsql.Parameters.Add("@id", MySqlDbType.Int32);
                cmsql.Parameters["@id"].Value = e.Id;
                data = cmsql.ExecuteReader();
                while (data.Read())
                {
                    foreach (Diplome d in lesDiplomes)
                    {
                        if (data.GetInt32(0) == d.Id)
                        {
                            e.LesDiplomeEmployeAdd(d);
                        }
                    }
                }
                cnx.Close();
            }
        }
        public void AjouterService(int id, string designation, char type, string produit, int capacite)
        {
            Service s = new Service(id, designation, capacite, produit, type);
            lesServices.Add(s);
        }
        public void AjouterService(int id, string designation, char type, int budget)
        {
            Service s = new Service(id, budget, designation, type);
            lesServices.Add(s);
        }
        public void Sauvegarder()
        {
            MySqlCommand cmsql = new MySqlCommand();
            cmsql.Connection = cnx;
            cmsql.Connection.Open();
            cmsql.CommandText = "RemiseAZero";
            cmsql.CommandType = CommandType.StoredProcedure;
            cmsql.ExecuteNonQuery();
            cmsql.CommandText = "insert into service (ser_id,ser_designation,ser_type,ser_produit,ser_capacite,ser_budget) values(@id,@designation,@type,@produit,@capacite,@budget)";
            cmsql.CommandType = CommandType.Text;
            cmsql.Parameters.Add("@id", MySqlDbType.Int32);
            cmsql.Parameters.Add("@designation", MySqlDbType.String);
            cmsql.Parameters.Add("@type", MySqlDbType.VarChar);
            cmsql.Parameters.Add("@produit", MySqlDbType.String);
            cmsql.Parameters.Add("@capacite", MySqlDbType.Int32);
            cmsql.Parameters.Add("@budget", MySqlDbType.Int32);
            foreach (Service s in lesServices)
            {
                if (s.Type == 'P')
                {
                    cmsql.Parameters["@id"].Value = s.Id;
                    cmsql.Parameters["@designation"].Value = s.Designation;
                    cmsql.Parameters["@type"].Value = s.Type;
                    cmsql.Parameters["@produit"].Value = s.Produit;
                    cmsql.Parameters["@capacite"].Value = s.Id;
                    cmsql.Parameters["@budget"].Value = 0;
                }
                else
                {
                    cmsql.Parameters["@id"].Value = s.Id;
                    cmsql.Parameters["@designation"].Value = s.Designation;
                    cmsql.Parameters["@type"].Value = s.Type;
                    cmsql.Parameters["@budget"].Value = s.Budget;
                    cmsql.Parameters["@produit"].Value = "";
                    cmsql.Parameters["@capacite"].Value = 0;
                }
                cmsql.ExecuteNonQuery();
            }
            cmsql.Connection.Close();
            MySqlCommand cmsql1 = new MySqlCommand();
            cmsql1.Connection = cnx;
            cmsql1.Connection.Open();
            cmsql1.CommandText = "insert into employe (emp_id,emp_nom,emp_prenom,emp_sexe,emp_cadre,emp_salaire,emp_service) values(@id,@nom,@prenom,@sexe,@cadre,@salaire,@service)";
            cmsql1.CommandType = CommandType.Text;
            cmsql1.Parameters.Add("@id", MySqlDbType.Int32);
            cmsql1.Parameters.Add("@nom", MySqlDbType.String);
            cmsql1.Parameters.Add("@prenom", MySqlDbType.String);
            cmsql1.Parameters.Add("@sexe", MySqlDbType.VarChar);
            cmsql1.Parameters.Add("@cadre", MySqlDbType.Bit);
            cmsql1.Parameters.Add("@salaire", MySqlDbType.Int32);
            cmsql1.Parameters.Add("@service", MySqlDbType.Int32);
            foreach (Employe e in lesEmployes)
            {
                cmsql1.Parameters["@id"].Value = e.Id;
                cmsql1.Parameters["@nom"].Value = e.Nom;
                cmsql1.Parameters["@prenom"].Value = e.Prenom;
                cmsql1.Parameters["@sexe"].Value = e.Sexe;
                cmsql1.Parameters["@cadre"].Value = e.Cadre;
                cmsql1.Parameters["@salaire"].Value = e.Salaire;
                cmsql1.Parameters["@service"].Value = e.Service.Id;
                cmsql1.ExecuteNonQuery();
            }
            cmsql1.Connection.Close();
            MySqlCommand cmsql2 = new MySqlCommand();
            cmsql2.Connection = cnx;
            cmsql2.Connection.Open();
            cmsql2.CommandText = "insert into diplome (dip_id,dip_libelle) values(@id,@libelle)";
            cmsql2.CommandType = CommandType.Text;
            cmsql2.Parameters.Add("@id", MySqlDbType.Int32);
            cmsql2.Parameters.Add("@libelle", MySqlDbType.String);
            foreach (Diplome d in lesDiplomes)
            {
                cmsql2.Parameters["@id"].Value = d.Id;
                cmsql2.Parameters["@libelle"].Value = d.Libelle;
                cmsql2.ExecuteNonQuery();
            }
            cmsql2.Connection.Close();
            MySqlCommand cmsql3 = new MySqlCommand();
            cmsql3.Connection = cnx;
            cmsql3.Connection.Open();
            cmsql3.CommandText = "insert into posseder (pos_diplome,pos_employe) values(@diplome,@employe)";
            cmsql3.CommandType = CommandType.Text;
            cmsql3.Parameters.Add("@diplome", MySqlDbType.Int32);
            cmsql3.Parameters.Add("@employe", MySqlDbType.Int32);
            foreach (Employe e in lesEmployes)
            {
                for (int i = 0; i < e.CountDiplome(); i++)
                {
                    cmsql3.Parameters["@diplome"].Value = e.LesDiplomes[i].Id;
                    cmsql3.Parameters["@employe"].Value = e.Id;
                    cmsql3.ExecuteNonQuery();
                }
            }
            cmsql3.Connection.Close();
        }
    }
}
