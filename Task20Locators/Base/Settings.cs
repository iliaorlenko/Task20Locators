using System;
using System.IO;

namespace Task20Locators.Base
{
    public static class Settings
    {
        public static string baseDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppContext.BaseDirectory).ToString()).ToString()).ToString();
    }
}
