using Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Views
{
    public abstract class View : IObserver<Command>
    {
        /// <summary>
        /// Constructor of View
        /// </summary>
        public View() { }

        /// <summary>
        /// On completed
        /// </summary>
        public abstract void OnCompleted();

        /// <summary>
        /// On error
        /// </summary>
        /// <param name="error">Error to handle</param>
        public abstract void OnError(Exception error);

        /// <summary>
        /// On next
        /// </summary>
        /// <param name="value">Send command value</param>
        public abstract void OnNext(Command value);
    }
}
