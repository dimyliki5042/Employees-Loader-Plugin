using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LoaderPlugin
{
    [Author(Name = "Dmitriy Popov")]
    public class Plugins : IPluggable
    {
        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            Console.Write("Enter a path to json file:");
            string path = Console.ReadLine();
            return ReadJson(path).Cast<DataTransferObject>();
        }

        private List<EmployeesDTO> ReadJson(string path)
        {
            JArray jArray = JArray.Parse(File.ReadAllText(path));
            List<EmployeesDTO> employeesDTOs = new List<EmployeesDTO>();
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
