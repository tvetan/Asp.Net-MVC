namespace TwitterCopy.Models
{
    using System.Collections.Generic;

    public class SettingsViewModel
    {
        public string Id { get; set; }
       
        public string UserName { get; set; }

        public string Email { get; set; }

        public int? LanguageId { get; set; }

        public Language Language { get; set; }

        public IEnumerable<Language> Languages { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public IEnumerable<Country> Countries { get; set; }

        public int? TimeZoneId { get; set; }

        public TimeZone TimeZone { get; set; }

        public IEnumerable<TimeZone> TimeZones { get; set; }
    }
}