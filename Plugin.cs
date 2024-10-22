using Newtonsoft.Json.Linq;
using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoaderPlugin
{
    [Author(Name = "Dmitriy Popov")]
    public class Plugins : IPluggable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            logger.Info("Starting Loader");
            logger.Info("Enter file path");
            string path = Console.ReadLine();
            return ReadJson(path).Cast<DataTransferObject>();
        }

        private List<EmployeesDTO> ReadJson(string path)
        {
            List<EmployeesDTO> employeesDTOs = new List<EmployeesDTO>();
            string json = "";
            try
            {
                json = File.ReadAllText(path);
            }catch(IOException e)
            {
                logger.Error(e.Message);
                return employeesDTOs;
            }
            JArray jArray = JArray.Parse(json);
            var firstName = jArray.Select(x => x["firstName"]).ToList();
            var lastName = jArray.Select(x => x["lastName"]).ToList();
            var maidenName = jArray.Select(x => x["maidenName"]).ToList();
            var phones = jArray.Select(x => x["phone"]).ToList();
            for (int i = 0; i < (firstName.Count); i++)
            {
                EmployeesDTO employeeDTO = new EmployeesDTO();
                employeeDTO.Name = $"{firstName[i]} {lastName[i]} {maidenName[i]}";
                employeeDTO.AddPhone(phones[i].ToString());
                employeesDTOs.Add(employeeDTO);
            }
            return employeesDTOs;
        }
    }
}
