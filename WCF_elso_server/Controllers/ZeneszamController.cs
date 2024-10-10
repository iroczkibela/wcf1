using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using WCF_elso_server.Models;
namespace WCF_elso_server.Controllers
{
    public class ZeneszamController
    {
        public List<Zeneszam> ZeneszamLista()
        {
            string[] sorok = File.ReadAllLines("C:\\Users\\Béci\\Desktop\\backend\\WCF_elso\\WCF_elso_server\\ZeneszamAdatok.txt");
            List<Zeneszam> list = new List<Zeneszam>();
            for (int i = 1; i < sorok.Length; i++)
            {
                string[] bontas = sorok[i].Split(';');
                list.Add(new Zeneszam()
                {
                    ZeneszamAz = int.Parse(bontas[0]),
                    ZeneszamCim = bontas[1],
                    ZeneszamHossz = int.Parse(bontas[2])

                });
            }
            return list;
        }

        public string InsertZeneszam(Zeneszam ujZeneszam)
        {
            ujZeneszam.ZeneszamAz = GenerateID();
            StreamWriter kimenet = new StreamWriter("C:\\Users\\Béci\\Desktop\\backend\\WCF_elso\\WCF_elso_server\\ZeneszamAdatok.txt", true);

            kimenet.WriteLine(ujZeneszam.ToString());
            kimenet.Close();
            return "Sikeresen mentettük a zeneszámot!";
        }

        int GenerateID()
        {
            return ZeneszamLista().Select(zeneszam => zeneszam.ZeneszamAz).ToList().Max() + 1;
        }

        public string UpdateZeneszam(Zeneszam zeneszam)
        {
            //beolvasok az állományból
            List<Zeneszam> aktualis = ZeneszamLista();
            //Megkeresem az ID-t a listában
            int index = 0;
            while (index < aktualis.Count &&
                aktualis[index].ZeneszamAz != zeneszam.ZeneszamAz)
            {
                index++;
            }

            if (index < aktualis.Count)
            {   //Ha találok, módosítom a listát
                aktualis[index].ZeneszamCim = zeneszam.ZeneszamCim;
                //A módosított lista alapján újragenerálom az állományt
                StreamWriter ujAllomany = new StreamWriter("C:\\Users\\Béci\\Desktop\\backend\\WCF_elso\\WCF_elso_server\\ZeneszamAdatok.txt");
                ujAllomany.WriteLine("zeneszamAzon;zeneszamCim");
                foreach (Zeneszam a in aktualis)
                {
                    ujAllomany.WriteLine(a.ToString());
                }
                ujAllomany.Close();
                //Üzenem, hogy a módosítás sikeres
                return "A módosítás sikeres";
            }
            else
                //Ha nem találom, üzenem, hogy nincs ilyen
                return "Nincs ilyen azonosítójú zene";
        }

        public string TorolZeneszam(int id)
        {
            //beolvasok az állományból
            List<Zeneszam> aktualis = ZeneszamLista();
            //Megkeresem az ID-t a listában
            int index = 0;
            while (index < aktualis.Count &&
                aktualis[index].ZeneszamAz != id)
            {
                index++;
            }

            if (index < aktualis.Count)
            {   //Ha találok, törlöm a listából az elemet

                //A módosított lista alapján újragenerálom az állományt
                StreamWriter ujAllomany = new StreamWriter("C:\\Users\\Béci\\Desktop\\backend\\WCF_elso\\WCF_elso_server\\ZeneszamAdatok.txt");
                ujAllomany.WriteLine("zeneszamAzon;zeneszamCim");
                foreach (Zeneszam a in aktualis)
                {
                    ujAllomany.WriteLine(a.ToString());
                }
                ujAllomany.Close();
                //Üzenem, hogy a módosítás sikeres
                return "A törlés sikeres";
            }
            else
            {
                //Ha nem találom, üzenem, hogy nincs ilyen
                return "Nincs ilyen azonosítójú zene";
            }

        }
    }
}