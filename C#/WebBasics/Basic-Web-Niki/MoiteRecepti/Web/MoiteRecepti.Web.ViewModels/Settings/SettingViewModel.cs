namespace MoiteRecepti.Web.ViewModels.Settings
{
    using AutoMapper;

    using MoiteRecepti.Data.Models;
    using MoiteRecepti.Services.Mapping;

    public class SettingViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string NameAndValue { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
        }
    }
}
