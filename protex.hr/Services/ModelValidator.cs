using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace protex.hr.Services
{
    public class ModelValidator
    {
        
        public void Validate(object model)
        {
            string msg = "";
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var output = Validator.TryValidateObject(model, context, results, true);

            if (!output)
            {
                foreach (var item in results)
                {
                    msg += $"-{item.ErrorMessage}\n";
                }

                throw new Exception(msg);
            }
        }


        public void Validate(AttendanceRecord a)
        {
            string msg = "";
            bool output = true;

            try
            {
                Validate((object)a);
            }
            catch (Exception ex) 
            {
                msg = ex.Message;
                output = false;
            }

            // Controllo esclusivo di AttendanceRecord
            if (a.CheckOutTime < a.CheckInTime)
            {
                msg += "-L'orario di check out deve essere maggiore dell'orario di check in";
                output = false;
            }
               
            if(!output)
                throw new Exception(msg);
        }

        public void Validate(LeaveRequest l)
        {
            string msg = "";
            bool output = true;

            try
            {
                Validate((object)l);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                output = false;
            }

            // Controllo esclusivo di LeaveRequest
            if (l.StartDate > l.EndDate)
            {
                msg += "-La data di fine ferie deve essere maggiore della data di inizio ferie";
                output = false;
            }

            if (!output)
                throw new Exception(msg);

        }




    }
}
