using CSI.Data;
using CSI;
using CSI.MG;
using Microsoft.Extensions.DependencyInjection;
using Mongoose.IDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtractorLEA_assemblies.Metodos;
using ExtractorLEA_assemblies.Interface;

namespace ExtractorLEA_assemblies
{
    [IDOExtensionClass("Accounts")] 
 
//Se empieza con una clase principal esta es la principal.

    public class Accounts : CSIExtensionClassBase
    {

        private CSI.Data.IAppDBProvider appDBProvider;
        IServiceProvider serviceProvider;
        public override object GetService(System.Type serviceType)
        {
            var sc = new ServiceCollection();

            //populate the service collection with standard services
            var cr = new CompositionRoot();
            //classes
            sc.AddScoped<TrialBalance>(); // Clase principal
            //interfaces
            sc.AddScoped<iTrialBalance, TrialBalance>();  // interfas ( Agrear interfas a la clase)

            //Register Services
            cr.RegisterMongooseBasedServices(sc, new MGCoreFeatures(this));

            //register any custom services
            this.RegisterCustomServices(sc);

            //build the service provider
            var serviceProvider = sc.BuildServiceProvider();

            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

            appDBProvider = serviceProvider.GetService<CSI.Data.IAppDBProvider>();

            return serviceProvider.GetService<IAppDBProxyFactory>().Create(serviceType, serviceProvider.GetService(serviceType: serviceType), appDBProvider);
        }

        public int GettrialBalance(string Fechainicio, string FechaFin, string RFC, ref string INFO)
        {

            try
            {

                using (var MGAppDB = this.CreateAppDB())
                {
                    //var appDB = new CSIAppDBFactory().CreateAppDB(MGAppDB, this.Context, this.GetMessageProvider());
                    //var iSubmitInvoice = new SubmitInvoiceFactory().Create(this, false);
                    TrialBalance iSubmitInvoice = this.GetService<TrialBalance>();

                    iSubmitInvoice.GetBalance(Fechainicio, FechaFin, RFC, MGAppDB);
                    
                }

                return 0;
            }
            catch (Exception h)
            {

                INFO = Convert.ToString(h);
                return 1;
            }
        }


    }
}
