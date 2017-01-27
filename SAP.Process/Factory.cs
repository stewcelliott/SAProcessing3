using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Interfaces;
using Microsoft.Practices.Unity;

namespace SAP.Process
{
    /// <summary>
    /// sets up IoC with Unity
    /// </summary>
    public class Factory
    {
        private static readonly Factory _current = new Factory();
        private static readonly UnityContainer _unity = new UnityContainer();

        private Factory()
        {
        }
        
        public static Factory Current
        {
            get
            {
                return _current;
            }
        }

        public void RegisterInterfaces()
        {
            _unity.RegisterType<IEngine, Engine>();
            _unity.RegisterType<IAnalyser, Analyser>();
        }

        public IEngine Engine()
        {
            return _unity.Resolve<IEngine>();
        }

        public IAnalyser Analyse()
        {
            return _unity.Resolve<IAnalyser>();
        }

    }
}
