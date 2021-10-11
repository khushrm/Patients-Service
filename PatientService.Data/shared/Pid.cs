using System;
using System.Collections.Generic;
using System.Text;

namespace PatientService.Data.shared
{
    public class Pid
    {
        private static int Id = 1;
        public static int GetNewId()
        {
            return Id++;
        }
    }
}
