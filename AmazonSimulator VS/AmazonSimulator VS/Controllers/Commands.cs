using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Newtonsoft.Json;

namespace Controllers {

    public abstract class Command {
        /// <summary>
        /// type of the command, to differentiate between different commands
        /// </summary>
        private string type;
        /// <summary>
        /// the object that needs to perform the command
        /// </summary>
        private Object parameters;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">see this.type</param>
        /// <param name="parameters">see this.parameters</param>
        public Command(string type, Object parameters) {
            this.type = type;
            this.parameters = parameters;
        }

        /// <summary>
        /// Serialises the command and parameters to a JSON object
        /// </summary>
        /// <returns>string (JSON)</returns>
        public string ToJson() {
            return JsonConvert.SerializeObject(new {
                command = type,
                parameters = parameters
            });
        }
    }
    /// <summary>
    /// Passes the command to the correct object
    /// </summary>
    public abstract class Model3DCommand : Command {

        public Model3DCommand(string type, ThreeDModels parameters) : base(type, parameters) {
        }
    }
    /// <summary>
    /// Passes an update command to the correct objects
    /// </summary>
    public class UpdateModel3DCommand : Model3DCommand {
        
        public UpdateModel3DCommand(ThreeDModels parameters) : base("update", parameters) {
        }
    }
}