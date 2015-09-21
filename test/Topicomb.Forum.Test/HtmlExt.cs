using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Linq;
namespace CodeComb.HtmlAgilityPack
{
    public static class HtmlExt
    {
        public static HtmlNode GetElementById(this HtmlNode self, string id)
        {
            if (self.Id == id) return self;
            foreach (var c in self.ChildNodes)
            {
                return c.GetElementById(id);
            }
            return null;
        }

        public static HtmlNode GetElementByName(this HtmlNode self, string name)
        {
            if (self.Name == name) return self;
            foreach (var c in self.ChildNodes)
            {
                return c.GetElementByName(name);
            }
            return null;
        }
        
        public static string GetRequestVerificationToken(this HtmlNode self, string formId)
        {
            try
            {
                return self.GetElementById(formId).GetElementByName("__RequestVerificationToken").Attributes.Where(x => x.Name == "value").First().Value;
            }
            catch
            {
                return null;
            }
        }
        
        public static string GetRequestVerificationToken(this HtmlNode self)
        {
            try
            {
                return self.GetElementByName("__RequestVerificationToken").Attributes.Where(x => x.Name == "value").First().Value;
            }
            catch
            {
                return null;
            }
        }
    }
}