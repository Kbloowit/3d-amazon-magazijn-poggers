using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Models;
using Views;

namespace Controllers {
    struct ObservingClient {
        public ClientView cv;
        public IDisposable unsubscribe;
    }
    public class SimulationController {
        /// <summary>
        /// World the simulation is running in
        /// </summary>
        private World w;
        /// <summary>
        /// List of views
        /// </summary>
        private List<ObservingClient> views = new List<ObservingClient>();
        /// <summary>
        /// Bool, simultion is running or not
        /// </summary>
        private bool running = false;
        /// <summary>
        /// Ticktime = 50, means 20 times per second
        /// </summary>
        private int tickTime = 50;

        /// <summary>
        /// Constructor of Simulation controller, sets world
        /// </summary>
        /// <param name="w"></param>
        public SimulationController(World w) {
            this.w = w;
        }

        /// <summary>
        /// Adds a view to views
        /// </summary>
        /// <param name="v"></param>
        public void AddView(ClientView v) {
            ObservingClient oc = new ObservingClient();

            oc.unsubscribe = this.w.Subscribe(v);
            oc.cv = v;

            views.Add(oc);
        }

        /// <summary>
        /// Removes a view from views
        /// </summary>
        /// <param name="v"></param>
        public void RemoveView(ClientView v) {
            for(int i = 0; i < views.Count; i++) {
                ObservingClient currentOC = views[i];

                if(currentOC.cv == v) {
                    views.Remove(currentOC);
                    currentOC.unsubscribe.Dispose();
                }
            }
        }

        /// <summary>
        /// Run the simulation
        /// </summary>
        public void Simulate() {
            running = true;

            while(running) {
                w.Update(tickTime);
                Thread.Sleep(tickTime);
            }
        }

        /// <summary>
        /// End the simulation (simulation stops running)
        /// </summary>
        public void EndSimulation() {
            running = false;
        }
    }
}