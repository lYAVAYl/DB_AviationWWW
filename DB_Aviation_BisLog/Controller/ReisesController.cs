using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB_Aviation_BisLog.Model;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



namespace DB_Aviation_BisLog.Controller

{
    public class ReisesController
    {
        public Reises Reises { get; }

        public ReisesController(Reises reises)
        {
            Reises = reises ?? throw new ArgumentException("Рейсы не могут быть раны nulll",nameof(reises));
        }

        public void Save()
        {
            var formatter = new BinaryFormatter();
                       
            using (var savetofile = new FileStream("ReisesController.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(savetofile, Reises);
            }
        }
       

        public Reises Load()
        {
            var formatter = new BinaryFormatter();

            using (var savetofile = new FileStream("ReisesController.dat", FileMode.OpenOrCreate))
            {
                if (formatter.Deserialize(savetofile) is Reises)
                {
                    return 
                }
            }
        }



    }
}
