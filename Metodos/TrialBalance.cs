using CSI.Data.RecordSets;
using CSI.MG;
using CSI.Data.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSI.Reporting;
using System.Data;
using ExtractorLEA_assemblies.Helprs;
using Mongoose.IDO.DataAccess;
using ExtractorLEA_assemblies.Models;
using ExtractorLEA_assemblies.Interface;

//Se arma el metodo principal el cual llamara en el IDO y ara todo el trabajo.

namespace ExtractorLEA_assemblies.Metodos
{
    internal class TrialBalance : iTrialBalance
    {

        IApplicationDB appDB;
        readonly IBunchedLoadCollection bunchedLoadCollection;
        IDataTableToCollectionLoadResponse dataTableToCollectionLoadResponse;

        public ConexionBD Query; // agregar para poder llamar a la clase ConexionDB


        public TrialBalance(IApplicationDB appDB, IBunchedLoadCollection bunchedLoadCollection, IDataTableToCollectionLoadResponse dataTableToCollectionLoadResponse)
        {
            this.appDB = appDB;
            this.bunchedLoadCollection = bunchedLoadCollection;
            this.dataTableToCollectionLoadResponse = dataTableToCollectionLoadResponse;


        }
        public void GetBalance(string DateInicio, string DateFin, string RFC, AppDB querys)
        {
            //como consultar datos de otras tablas
            Query = new ConexionBD(querys);
            List<string>Parametros = new List<string>();
            Parametros.Add("0");
            DataTable SITEDefault = Query.SelectDT("Select site from parms where parm_Key = @p0",Parametros.ToArray());
            string SITE = SITEDefault.Rows[0]["site"].ToString();
            Parametros.Clear();
            Parametros.Add(SITE);
            DataTable SiteGroupsDataTable= Query.SelectDT("Select Site_group from site_group where site = @p0", Parametros.ToArray());
            string SiteGroups = SiteGroupsDataTable.Rows[0]["Site_group"].ToString() ;    // Se agrega la columna a buscar
            Parametros.Clear();     

            //var ConsultaMetodo = new Rpt_GeneralLedgerbyAccount(appDB, bunchedLoadCollection, dataTableToCollectionLoadResponse);
            //var Balance = ConsultaMetodo.Rpt_GeneralLedgerbyAccountSp(Convert.ToDateTime(DateInicio),Convert.ToDateTime(DateFin), 0, "D", "N",0,0,"ALORED",null,null, SITE, null,null,0,1,0,null, null, null, null, null, null, null, null,0, SiteGroups, null,null,null,null);

            
            DataTable ChartsDatable = Query.SelectALL("Select * from chart ");
            List<Cuenta> Cuentas = new List<Cuenta>();

            for (int i = 0; i < ChartsDatable.Rows.Count; i++)
            {
                
                Cuenta NuevaCuenta = new Cuenta();
                NuevaCuenta.Account = ChartsDatable.Rows[i]["Acct"].ToString();
                NuevaCuenta.Decription = ChartsDatable.Rows[i]["description"].ToString();
                NuevaCuenta.Type = ChartsDatable.Rows[i]["Acct"].ToString();

                if (NuevaCuenta.Type == "A" || NuevaCuenta.Type == "E")
                {
                    NuevaCuenta.Type = "D";

                }
                else
                {
                    NuevaCuenta.Type= "A";
                }

                Cuentas.Add(NuevaCuenta);
            }

            for (int i = 0; i < Cuentas.Count; i++)
            {

                Parametros.Clear();

                string Account = Cuentas[i].Account;
                string Description = Cuentas[i].Decription;
                string Type = Cuentas[i].Type;

                Parametros.Add(RFC);
                Parametros.Add(Account);
                Parametros.Add(Description);
                Parametros.Add(Type);

                Query.EjecutarQry("Insert into MXEAAD(RFC,Account,Description,Nature) Values (@p0,@p1,@p2,@p3)", Parametros.ToArray());
            }

        }
            

    }
}
