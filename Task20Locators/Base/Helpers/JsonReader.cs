using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace Helpers.Task20Locators.Base
{
    public static class JsonReader
    {
        public static IEnumerable GetLoginTestsData()
        {
            using (StreamReader reader = new StreamReader(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppContext.BaseDirectory).ToString()).ToString()).ToString() + "\\TutBy\\TestData.json"))
            {
                JObject json = JObject.Parse(reader.ReadToEnd());

                return
                    from tests in json["TestData"]["Tests"]
                    let login = tests["Login"].ToString()
                    let password = tests["Password"].ToString()
                    let username = tests["Username"].ToString()

                    select new object[] { login, password, username };
            }
        }
    }
}
