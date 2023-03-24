using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using Telegram.Bot.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Telegram.Bot;
using System.Windows.Forms;
using System.Net;

namespace Blaze_2._0
{
    class Crawler
    {



        public static string[] retornos { get; private set; }
        public static bool Atualizou { get; set; }


        ChromeDriver driver;
        int horaJogada = 98;
        int horarioReiniciou = 99;

        private IWebDriver _webDriver;
        public static Form1 teste = new Form1();
        private static TelegramBotClient client = new TelegramBotClient("6117182932:AAEpaORpaDAfMh2DfDhWmjsK9uarcNkJ3ok");
        public void Iniciar()
        {
            try
            {
                DownloadChorme.DonwloadChorme();
                retornos = new string[12];
                int quantidadeWin = 0;
                int quantidadeLoss = 0;

                var nome = Environment.MachineName;

                Console.WriteLine(nome); //PC-162

                ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.GetTempPath() + @"\V4");
                string oi = Path.GetTempPath() + @"\V4";
                Console.WriteLine(Path.GetTempPath());
                service.HideCommandPromptWindow = true;

                ChromeOptions options = new ChromeOptions();
                // options.AddArgument("headless");

                try
                {

                    driver = new ChromeDriver(service, options);

                }
                catch
                {
                    driver = new ChromeDriver(service, options);

                }
                driver.Navigate().GoToUrl("https://casino.netbet.com/br/play/football-studio");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(90));
                WebDriverWait waitt = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                IWebElement firstResult = null;
                try
                {
                    firstResult = wait.Until(e => e.FindElement(By.XPath("/html/body/div[1]/div/div[8]/div/div[1]/div/div/div/div[2]/div/div[3]/div[3]")));
                    driver.FindElement(By.XPath("/html/body/div[1]/div/div[8]/div/div[1]/div/div/div/div[2]/div/div[3]/div[3]")).Click();
                }
                catch
                {
                }

                firstResult = wait.Until(e => e.FindElement(By.XPath("/html/body/div[1]/div/div[3]/div/div[1]/div/div[2]/div/div[1]/div/div[2]/div[3]/div[2]/div")));
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/div/div[1]/div/div[2]/div/div[1]/div/div[2]/div[3]/div[2]/div")).Click();
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[8]/div/div[2]/div/div/div/div[2]/div/form/div[2]/div[1]/input")).Click();
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[8]/div/div[2]/div/div/div/div[2]/div/form/div[2]/div[1]/input")).SendKeys("Edzxra");

                driver.FindElement(By.XPath("/html/body/div[1]/div/div[8]/div/div[2]/div/div/div/div[2]/div/form/div[2]/div[2]/input")).Click();
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[8]/div/div[2]/div/div/div/div[2]/div/form/div[2]/div[2]/input")).SendKeys("eder201180");

                driver.FindElement(By.XPath("/html/body/div[1]/div/div[8]/div/div[2]/div/div/div/div[2]/div/form/div[3]")).Click();

                try
                {
                    driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/div/div[1]/div/div[2]/div/div[1]/div/div[2]/div[3]/div[2]/div")).Click();
                }
                catch { }
                string[] ultimasEntradas = new string[10];
                firstResult = wait.Until(e => e.FindElement(By.XPath("/html/body/div[1]/div/main/div[3]/div[1]/div/div[2]/div[3]/div/iframe")));

                IWebElement iframe = driver.FindElement(By.XPath("/html/body/div[1]/div/main/div[3]/div[1]/div/div[2]/div[3]/div/iframe"));
                driver.SwitchTo().Frame(iframe);

                while (true)

                {
                    try
                    {
                        try
                        {
                            firstResult = wait.Until(e => e.FindElement(By.ClassName("titleContainer--fe91d")));
                            Console.WriteLine(driver.FindElement(By.ClassName("titleContainer--fe91d")).Text);

                            if (!driver.FindElement(By.ClassName("titleContainer--fe91d")).Text.Contains("SALDO BAIXO"))
                            {
                                Close();
                                Iniciar();
                            }
                        }
                        catch
                        {
                            Close();
                           Iniciar();
                        }

                        try
                        {
                            if (driver.FindElement(By.ClassName("title--f4c0d")).Text.Contains("conexão"))
                            {
                                driver.Navigate().GoToUrl("https://www.br4bet.com/casino/?cat=live&gameid=2375");
                                firstResult = wait.Until(e => e.FindElement(By.XPath("/html/body/div[1]/div/main/div[3]/div[1]/div/div[2]/div[3]/div/iframe")));

                                 iframe = driver.FindElement(By.XPath("/html/body/div[1]/div/main/div[3]/div[1]/div/div[2]/div[3]/div/iframe"));
                                driver.SwitchTo().Frame(iframe);
                            }

                        }
                        catch
                        {
                            try
                            {
                                if (driver.FindElement(By.XPath("/html/body")).Text.Contains("User authentication failed"))
                                {
                                    driver.Navigate().GoToUrl("https://www.br4bet.com/casino/?cat=live&gameid=2375");
                                    firstResult = wait.Until(e => e.FindElement(By.XPath("/html/body/div[1]/div/main/div[3]/div[1]/div/div[2]/div[3]/div/iframe")));

                                     iframe = driver.FindElement(By.XPath("/html/body/div[1]/div/main/div[3]/div[1]/div/div[2]/div[3]/div/iframe"));
                                    driver.SwitchTo().Frame(iframe);
                                }
                            }
                            catch { }   
                        }
                        // Obter a hora atual
                        DateTime now = DateTime.Now;
                        // Verificar se a hora é 12h ou 00h

                        if ((now.Hour == 12 || now.Hour == 0) && now.Hour != horaJogada)
                        {

                            quantidadeWin = teste.QuantidadeWin();
                            quantidadeLoss = teste.QuantidadeLoss();
                            double porcentagem = 0;
                            if (quantidadeLoss == 0 && quantidadeWin == 0)
                                porcentagem = 100;
                            else
                            {
                                if (quantidadeLoss == 0)
                                    porcentagem = 100;
                                else
                                    porcentagem = (quantidadeLoss * 100) / (quantidadeWin + quantidadeLoss);
                                client.SendTextMessageAsync(chatId: -1001822411577, text: "📊 PLACAR BLACK OWLS 🦉 \r\n\r\n ✅ WIN:" + quantidadeWin + "\r\n ❌ LOSS: " + quantidadeLoss + "\r\n\r\n 🎯 Assertividade: " + porcentagem + "%"); ;
                                // Saída do loop infinitos
                                horaJogada = now.Hour;
                            }
                        }
                        int contador = 0;
                        try
                        {
                            driver.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div/div[2]/div[10]/div[1]/button")).Click();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                driver.FindElement(By.ClassName("clickable--394a7")).Click();
                            }
                            catch { }
                        }
                        foreach (var teste in driver.FindElement(By.CssSelector("div[data-role ='history-statistic']")).FindElements(By.ClassName("historyItem--a1907")))
                        {
                            string dadoAtual = teste.FindElement(By.CssSelector("svg[fill = 'none'")).FindElement(By.CssSelector("g[filter = 'url(#history)'")).FindElement(By.CssSelector("text[font-size = '16'")).Text;
                            if (dadoAtual == "V")
                                dadoAtual = "blue";
                            else if (dadoAtual == "C")
                                dadoAtual = "red";
                            else
                                dadoAtual = "white";
                            retornos[contador] = dadoAtual;
                            contador++;
                            if (contador >= 12)
                            {
                                break;
                            }
                        }


                        if (string.IsNullOrEmpty(ultimasEntradas[0]) &&
                        string.IsNullOrEmpty(ultimasEntradas[1]) &&
                        string.IsNullOrEmpty(ultimasEntradas[2]) &&
                        string.IsNullOrEmpty(ultimasEntradas[3]) &&
                        string.IsNullOrEmpty(ultimasEntradas[4]) &&
                        string.IsNullOrEmpty(ultimasEntradas[5]) &&
                        string.IsNullOrEmpty(ultimasEntradas[6]) &&
                        string.IsNullOrEmpty(ultimasEntradas[7]) &&
                        string.IsNullOrEmpty(ultimasEntradas[8]) &&
                        string.IsNullOrEmpty(ultimasEntradas[9]))
                        {

                            for (int t = 0; t < 10; t++)
                            {
                                ultimasEntradas[t] = retornos[t];
                            }

                            Atualizou = true;
                            Thread th = new Thread(InserirGame);
                            th.Start();
                            Thread.Sleep(500);

                        }
                        else
                        {

                            if (ultimasEntradas[0] != retornos[0] ||
                                ultimasEntradas[1] != retornos[1] ||
                                ultimasEntradas[2] != retornos[2] ||
                                ultimasEntradas[3] != retornos[3] ||
                                ultimasEntradas[4] != retornos[4] ||
                                ultimasEntradas[5] != retornos[5] ||
                                ultimasEntradas[6] != retornos[6] ||
                                ultimasEntradas[7] != retornos[7] ||
                                ultimasEntradas[8] != retornos[8] ||
                                ultimasEntradas[9] != retornos[9])
                            {



                                for (int t = 0; t < 10; t++)
                                {
                                    ultimasEntradas[t] = retornos[t];
                                }




                                Atualizou = true;
                                Thread th = new Thread(InserirGame);
                                th.Start();
                                Thread.Sleep(500);
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("disconnected"))
                        {
                            driver.Navigate().GoToUrl("https://www.br4bet.com/casino/?cat=live&gameid=2375");
                            firstResult = wait.Until(e => e.FindElement(By.XPath("/html/body/div[1]/div/main/div[3]/div[1]/div/div[2]/div[3]/div/iframe")));

                            iframe = driver.FindElement(By.XPath("/html/body/div[1]/div/main/div[3]/div[1]/div/div[2]/div[3]/div/iframe"));
                            driver.SwitchTo().Frame(iframe);
                        }
                        else if (ex.Message.Contains("no such element: Unable to locate element: {\"method\":\"css selector\",\"selector\":\".titleContainer\\-\\-fe91d"))
                        {
                            driver.Navigate().GoToUrl("https://www.br4bet.com/casino/?cat=live&gameid=2375");
                            firstResult = wait.Until(e => e.FindElement(By.XPath("/html/body/div[1]/div/main/div[3]/div[1]/div/div[2]/div[3]/div/iframe")));

                            iframe = driver.FindElement(By.XPath("/html/body/div[1]/div/main/div[3]/div[1]/div/div[2]/div[3]/div/iframe"));
                            driver.SwitchTo().Frame(iframe);
                        }
                    }

                }

            }
            catch
            {
            }
        }

        public static bool engine;

        private void InserirGame()
        {
            try
            {

                new DataBase().Insert($"INSERT INTO Game VALUES (Null,\"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\",\"{retornos[0].Replace("X", "")}\");");

            }
            catch
            {

            }
        }

        public void Close()
        {

            try
            {
                if (driver != null)
                {
                    driver.Quit();
                }
            }
            catch (Exception ex) { }
        }

    }
}
