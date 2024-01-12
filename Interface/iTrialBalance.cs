using Mongoose.IDO.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLEA_assemblies.Interface
{
    //Se arma una interface para enviar los datos desde la cuenta principal
    public interface iTrialBalance
    {
        void GetBalance(string DateInicio, string DateFin, string RFC, AppDB querys);
        
    }
}
