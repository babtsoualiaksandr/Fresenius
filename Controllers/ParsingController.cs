using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using Fresenius.Data;
using Microsoft.AspNetCore.Mvc;

namespace Fresenius.Controllers
{
    public class ParsingController : Controller
    {
        static public  async Task<IdentityCard> GetDatarcethby(string InIm )
        { 
                // Setup the configuration to support document loading
                var config = Configuration.Default.WithDefaultLoader();
                // Load the names of all The Big Bang Theory episodes from Wikipedia
                var address = "http://rceth.by/Refbank/reestr_medicinskoy_tehniki/details/" + InIm + "?allproducts=True";
                // Asynchronously get the document in a new context using the configuration
                var document = await BrowsingContext.New(config).OpenAsync(address);
                // This CSS selector gets the desired content
                var cellSelector = ".results table td";
                // Perform the query to get all cells with the content
                var cells = document.QuerySelectorAll(cellSelector);
                // We are only interested in the text - select it with LINQ
                var titles = cells.Select(m => m.TextContent);
                string[] title = new string[titles.Count()];
                int i = 0;
                foreach (var item in titles)
                {
                    title[i] = item;
                    i++;
                }    
                IdentityCard identityCard = new IdentityCard
                {    
                    Number = title[0],
                    DateOfRegistration =DateTime.Parse( title[1]),
                    Expiration = DateTime.Parse(title[2]),
                    Applicant = title[3],
                    Purpose = title[4]                    
                };
            return identityCard;
        }

        public Task<IdentityCard> Card() => GetDatarcethby("7.105921");


    }
}