using System;
using System.Collections.Generic;
using System.Text;

namespace PatientService.Domain.Shared
{
    public class PatientIdGenerator
    {
        private static int Id = 21;
        public static int GetNewId()
        {
            return Id++;
        }
    }
}
