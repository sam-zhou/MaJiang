using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Core.WinProcessors
{
    public class WinProcessorFactory
    {
        private List<IWinProcessor> _winProcessors;

        public List<IWinProcessor> WinProcessors
        {
            get
            {
                if (_winProcessors == null)
                {
                    _winProcessors = new List<IWinProcessor>();
                }
                return _winProcessors;
            }
        }

        public WinProcessorFactory()
        {
            var processorTypes =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(q => typeof(IWinProcessor).IsAssignableFrom(q) && !q.IsInterface && !q.IsAbstract);



            foreach (var processorType in processorTypes)
            {
                //Create instances for each type
                var processor = (IWinProcessor)Activator.CreateInstance(processorType);
                WinProcessors.Add(processor);
            }
        }
    }
}
