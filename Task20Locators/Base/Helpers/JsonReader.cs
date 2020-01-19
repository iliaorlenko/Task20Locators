using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO;
using System.Linq;
using Task20Locators.Base;

namespace Helpers.Task20Locators.Base
{
    public static class JsonReader
    {
        public static IEnumerable GetLoginTestsData()
        {
            using (StreamReader reader = new StreamReader(Settings.baseDir + @"\TutBy\TestData.json"))
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
