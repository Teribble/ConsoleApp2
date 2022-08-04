using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ConsoleApp2
{
    public class Program
    {
        public static void Main()
        {

            List<string> titles = new List<string>();
            List<string> values = new List<string>();
            List<Product> prod = new List<Product>();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            var options = new ChromeOptions();

            options.AddArgument("--ignore-certificate-errors-spki-list");
            options.AddArgument("--ignore-ssl-errors");
            options.AddArgument("test-type");
            options.AddArguments("-incognito");
            options.AddArgument("no-sandbox");
            options.AddArgument("--start-maximized");

            var driver = new ChromeDriver(options);
            
            var url = @"https://www.dns-shop.ru/catalog/17a8a01d16404e77/smartfony/";

            driver.Navigate().GoToUrl(url);

            Thread.Sleep(1000);

            var pars_data = driver.FindElements(By.XPath("//a[@class='catalog-product__name ui-link ui-link_black']"));

            for (int i = 0; i < 1; i++)
            {
                var qweqwe = driver.FindElements(By.XPath("//a[@class='catalog-product__name ui-link ui-link_black']"));

                var back = driver.Url;

                Thread.Sleep(1000);

                qweqwe[i].Click();

                Thread.Sleep(1000);

                driver.FindElements(By.XPath("//div[@class='product-characteristics__group']")).ToList().ForEach(x =>
                {
                    x.FindElements(By.XPath("//div[@class='product-characteristics__spec-title']")).ToList().ForEach(title =>
                    {
                        titles.Add(title.GetAttribute("textContent").Trim(' '));
                    });

                    x.FindElements(By.XPath("//div[@class='product-characteristics__spec-value']")).ToList().ForEach(value =>
                    {
                        values.Add(value.GetAttribute("textContent").Trim(' '));
                    });
                });

                string price = String.Empty;

                try
                {
                    price = driver.FindElement(By.XPath("//div[@class='product-buy__price']")).GetAttribute("textContent");

                    Thread.Sleep(1500);
                }
                catch (Exception)
                {
                    price = driver.FindElement(By.XPath("//div[@class='product-buy__price product-buy__price_active']")).GetAttribute("textContent");
                }

                var t = driver.FindElements(By.XPath("//p"));
                string g = t[1].GetAttribute("textContent");

                var refs = driver.FindElement(By.XPath("//img[@data-src]")).GetAttribute("textContent");

                prod.Add(new Product
                {
                    Name =driver.FindElement(By.XPath("//div[@class='product-card-top__name']")).GetAttribute("textContent"),
                    Brend =driver.FindElement(By.XPath("//img[@alt]")).GetAttribute("textContent"),
                    Price = price,
                    PrictureRef =driver.FindElement(By.XPath("//img[@src]")).GetAttribute("textContent"),
                    Discription = g,
                    Type = values[titles.IndexOf("Тип")],
                    Model = values[titles.IndexOf("Модель")],
                    ReleaseYear = values[titles.IndexOf("Год релиза")]
                }) ;

                titles.Clear();

                values.Clear();

                Thread.Sleep(1000);

                driver.Navigate().GoToUrl(back);

                Thread.Sleep(1000);
            }

            Console.WriteLine("Hello");
        }
    }
}