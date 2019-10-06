using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace MiniGraber
{
    static class JSONProcessor
    {
        public static List<Person> ParsePeople(string response)
        {
            response = Regex.Replace(response, "{\"count\":([0-9]+),\"items\":", "");
            response = response.Substring(0, response.Length - 1);
            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(response);
            return people;
        }

        public static bool TryParseError(string response)
        {
            Error e;
            e = JsonConvert.DeserializeObject<Error>(response);
            if (e.error != null)
            {
                return true;
            }
            return false;
        }

        public static Person ParsePerson(string response)
        {
            return JsonConvert.DeserializeObject<Person>(response);
        }

    }
}
