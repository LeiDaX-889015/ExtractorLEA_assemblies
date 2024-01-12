using Mongoose.IDO.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Metodo de apoyo para llenar directo bases de datos de SL cloud by jose Barajas.

namespace ExtractorLEA_assemblies.Helprs
{
    public class ConexionBD
    {

        private AppDB AppDB;
        public ConexionBD(AppDB AppBD)
        {
            this.AppDB = AppBD;
        }
        public DataTable Select(string Joins, string AliasTabla, string Tabla, Dictionary<string, string> Columnas, string Where)
        {

            string COL = "";
            DataTable dataTable = new DataTable(); ;
            if (Columnas.Count > 0)
            {
                foreach (var col in Columnas)
                {
                    COL += col.Value + " as " + col.Key + ",";
                }
            }
            else
            {
                COL = "*";
            }
            COL = COL.TrimEnd(',');
            IDbCommand Sql = AppDB.CreateCommand();
            Sql.CommandText = "SELECT " + COL + " FROM " + Tabla + " AS " + AliasTabla + " " + Joins + " " + "WHERE " + Where;
            string Retult = "";
            try
            {
                using (IDataReader reader = Sql.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + Sql.CommandText);
            }
            return dataTable;
        }
        public DataTable Select(string AliasTabla, string Tabla, Dictionary<string, string> Columnas, string Where)
        {

            string COL = "";
            DataTable dataTable = new DataTable(); ;
            if (Columnas.Count > 0)
            {
                foreach (var col in Columnas)
                {
                    COL += col.Value + " as " + col.Key + ",";
                }
            }
            else
            {
                COL = "*";
            }
            COL = COL.TrimEnd(',');
            IDbCommand Sql = AppDB.CreateCommand();
            Sql.CommandText = "SELECT " + COL + " FROM " + Tabla + " AS " + AliasTabla + " " + "WHERE " + Where;
            string Retult = "";
            try
            {
                using (IDataReader reader = Sql.ExecuteReader())
                {
                    dataTable.Load(reader);
                }

            }
            catch (SqlException H)
            {
                throw new Exception(H + "\nComand: " + Sql.CommandText);
            }
            return dataTable;
        }
        public DataTable Select(string AliasTabla, string Tabla, Dictionary<string, string> Columnas)
        {
            string COL = "";
            DataTable dataTable = new DataTable();
            if (Columnas.Count > 0)
            {
                foreach (var col in Columnas)
                {
                    COL += " " + col.Value + " as " + col.Key + ",";
                }
            }
            else
            {
                COL = "*";
            }
            COL = COL.TrimEnd(',');
            IDbCommand Sql = AppDB.CreateCommand();
            Sql.CommandText = "SELECT " + COL + " FROM " + Tabla + " AS " + AliasTabla + " ";
            string Retult = "";
            try
            {
                using (IDataReader reader = Sql.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + Sql.CommandText);
            }
            return dataTable;
        }
        public DataTable SelectOrder(string AliasTabla, string Tabla, Dictionary<string, string> Columnas, string Orderby, int TOp)
        {
            string COL = "";
            DataTable dataTable = new DataTable();
            if (Columnas.Count > 0)
            {
                foreach (var col in Columnas)
                {
                    COL += " " + col.Value + " as " + col.Key + ",";
                }
            }
            else
            {
                COL = "*";
            }
            COL = COL.TrimEnd(',');
            IDbCommand Sql = AppDB.CreateCommand();
            Sql.CommandText = "SELECT top " + TOp + " " + COL + " FROM " + Tabla + " AS " + AliasTabla + " order by " + Orderby;
            string Retult = "";
            try
            {
                using (IDataReader reader = Sql.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + Sql.CommandText);
            }
            return dataTable;
        }
        public DataTable Select(string Joins, string AliasTabla, string Tabla, Dictionary<string, string> Columnas)
        {


            string COL = "";
            DataTable dataTable = new DataTable(); ;
            if (Columnas.Count > 0)
            {
                foreach (var col in Columnas)
                {
                    COL += col.Value + " as " + col.Key + ",";
                }
            }
            else
            {
                COL = "*";
            }
            COL = COL.TrimEnd(',');
            IDbCommand Sql = AppDB.CreateCommand();
            Sql.CommandText = "SELECT " + COL + " FROM " + Tabla + " AS " + AliasTabla + " " + Joins;
            try
            {
                string Retult = "";
                using (IDataReader reader = Sql.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
                return dataTable;
            }
            catch (SqlException ex)
            {
                throw new Exception(Sql.CommandText);
            }
        }
        public DataTable Select(string Joins, string AliasTabla, string Tabla, Dictionary<string, string> Columnas, string Where, string[] Parms)
        {
            string COL = "";
            DataTable dataTable = new DataTable(); ;
            if (Columnas.Count > 0)
            {
                foreach (var col in Columnas)
                {
                    COL += col.Value + " as " + col.Key + ",";
                }
            }
            else
            {
                COL = "*";
            }
            COL = COL.TrimEnd(',');
            SqlCommand Sql = AppDB.CreateCommand();
            int name = 0;
            foreach (string P in Parms)
            {
                if (string.IsNullOrEmpty(P))
                {
                    Sql.Parameters.AddWithValue("@p" + name, "");
                }
                else
                {
                    Sql.Parameters.AddWithValue("@p" + name, P);

                }
                name++;
            }



            Sql.CommandText = "SELECT " + COL + " FROM " + Tabla + " AS " + AliasTabla + " " + Joins + " " + "WHERE " + Where;
            string Retult = "";
            try
            {

                using (IDataReader reader = Sql.ExecuteReader())
                {

                    dataTable.Load(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Sql.CommandText);
            }
            return dataTable;
        }
        public DataTable Select(string AliasTabla, string Tabla, Dictionary<string, string> Columnas, string Where, string[] Parms)
        {
            string COL = "";
            DataTable dataTable = new DataTable(); ;
            if (Columnas.Count > 0)
            {
                foreach (var col in Columnas)
                {
                    COL += col.Value + " as " + col.Key + ",";
                }
            }
            else
            {
                COL = "*";
            }
            COL = COL.TrimEnd(',');
            SqlCommand Sql = AppDB.CreateCommand();
            int name = 0;
            foreach (string P in Parms)
            {
                if (string.IsNullOrEmpty(P))
                {
                    Sql.Parameters.AddWithValue("@p" + name, "");
                }
                else
                {
                    Sql.Parameters.AddWithValue("@p" + name, P);

                }

                name++;
            }
            Sql.CommandText = "SELECT " + COL + " FROM " + Tabla + " AS " + AliasTabla + " " + "WHERE " + Where;
            string Retult = "";
            try
            {
                Sql.ExecuteNonQuery();
                using (SqlDataReader reader = Sql.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + Sql.CommandText);
            }
            return dataTable;
        }
        public string Select(string qry, string[] Parms)
        {
            SqlCommand Sql = AppDB.CreateCommand();
            int name = 0;
            foreach (string P in Parms)
            {
                if (string.IsNullOrEmpty(P))
                {
                    Sql.Parameters.AddWithValue("@p" + name, "");
                }
                else
                {
                    Sql.Parameters.AddWithValue("@p" + name, P);

                }

                name++;
            }
            Sql.CommandText = qry;
            string Retult = "";
            try
            {
                Sql.ExecuteNonQuery();
                Retult = Sql.ExecuteScalar().ToString();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + Sql.CommandText);
            }

            return Retult;
        }
        public DataTable SelectDT(string qry, string[] Parms)
        {
            SqlCommand Sql = AppDB.CreateCommand();
            int name = 0;
            foreach (string P in Parms)
            {
                if (string.IsNullOrEmpty(P))
                {
                    Sql.Parameters.AddWithValue("@p" + name, "");
                }
                else
                {
                    Sql.Parameters.AddWithValue("@p" + name, P);

                }

                name++;
            }
            Sql.CommandText = qry;
            DataTable Retult = new DataTable();
            try
            {
                using (IDataReader reader = Sql.ExecuteReader())
                {

                    Retult.Load(reader);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + Sql.CommandText);
            }

            return Retult;
        }
        public DataTable SelectALL(string qry)
        {
            SqlCommand Sql = AppDB.CreateCommand();
            int name = 0;
            
            Sql.CommandText = qry;
            DataTable Retult = new DataTable();
            try
            {
                using (IDataReader reader = Sql.ExecuteReader())
                {

                    Retult.Load(reader);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + Sql.CommandText);
            }

            return Retult;
        }
        public int EjecutarQry(string qry, string[] Parms)
        {
            SqlCommand Sql = AppDB.CreateCommand();
            int name = 0;
            foreach (string P in Parms)
            {
                if (string.IsNullOrEmpty(P))
                {
                    Sql.Parameters.AddWithValue("@p" + name, "");
                }
                else
                {
                    Sql.Parameters.AddWithValue("@p" + name, P);

                }

                name++;
            }
            Sql.CommandText = qry;
            string Retult = "";
            return Sql.ExecuteNonQuery();
        }
        public int EjecutarQry(string qry, object[] Parms)
        {
            SqlCommand Sql = AppDB.CreateCommand();
            int name = 0;
            foreach (object P in Parms)
            {
                if (P == null || P == default)
                {
                    Sql.Parameters.AddWithValue("@p" + name, "");
                }
                else
                {
                    Sql.Parameters.AddWithValue("@p" + name, P);

                }

                name++;
            }
            Sql.CommandText = qry;
            string Retult = "";
            return Sql.ExecuteNonQuery();
        }
        public SqlParameterCollection EjecutarQry(string qry, List<SqlParameter> P)
        {
            SqlCommand Sql = AppDB.CreateCommand();
            int name = 0;
            Sql.Parameters.AddRange(P.ToArray());
            Sql.CommandText = qry;
            Sql.ExecuteNonQuery();
            return Sql.Parameters;
        }


    }
}
