using System.Linq;
using Newtonsoft.Json;
using SoftJail.Data.Models;
using SoftJail.DataProcessor.ExportDto;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners
                .Where(p => ids.Contains(p.Id))
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.FullName,
                    CellNumber = x.Cell.CellNumber,
                    Officers = x.PrisonerOfficers.Select(op => new
                        {
                            OfficerName = op.Officer.FullName,
                            Department = op.Officer.Department.Name
                        }).OrderBy(o => o.OfficerName)
                        .ToList(),
                    TotalOfficerSalary = decimal.Parse(x.PrisonerOfficers
                        .Sum(y => y.Officer.Salary)
                        .ToString("f2"))
                }).OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToList();

            string result = JsonConvert.SerializeObject(prisoners, Formatting.Indented);

            return result;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var names = prisonersNames.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var prisoners = context.Prisoners
                .Where(p => names.Contains(p.FullName))
                .Select(x => new PrisonerViewModel
                {
                    Id = x.Id,
                    Name = x.FullName,
                    IncarcerationDate = x.IncarcerationDate.ToString("yyyy-MM-dd"),
                    EncryptedMessages = x.Mails.Select(m => new EncryptedMessageViewModel
                    {
                        Description = new string(m.Description.Reverse().ToArray())
                    }).ToArray()
                }).OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToList();

            var result = XmlConverter.Serialize(prisoners, "Prisoners");

            return result;
        }
    }
}