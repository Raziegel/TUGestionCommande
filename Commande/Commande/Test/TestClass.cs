using GestionCommande.controleur;
using GestionCommande.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCommande.Test
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void TestAjoutClient()
        {
            //Question à l'attention du correcteur: Plutot Controleur ou CommandeControleur en premier?
            CommandeControleur ctrl = new CommandeControleur();

            //Initilisation d'un client
            Client c = new Client();
            c.Nom = "Xavier";
            c.Prenom = "Charles";
            c.Mail = "cerebro@gmail.com";

            //Ajout du client
            ctrl.CreerClient(c);

            //Vérification: le dernier item de la liste de clients doit avoir le Nom "Xavier"
            Assert.AreEqual("Xavier", ctrl.GetClients().Last().Nom);
        }

        [TestMethod]
        public void TestAjoutProduit()
        {
            CommandeControleur ctrl = new CommandeControleur();

            //Initialisation d'un produit
            Produit p = new Produit();
            p.Designation = "Costume";// Pour rester dans le thème
            p.Prix = 100;

            //Ajout du Produit
            ctrl.CreerProduit(p);

            //Vérification: le dernier item de la liste de produit doit avoir la désignation "Costume"
            Assert.AreEqual("Costume", ctrl.GetProduits().Last().Designation);

        }

        [TestMethod]
        public void TestAjoutCommande()
        {
            CommandeControleur ctrl = new CommandeControleur();
            //Initialisation d'un produit
            Produit p = new Produit();
            p.Designation = "Squelette d'adamantium";
            p.Prix = 50;

            //Initilisation de la liste de ligneCommande
            ICollection<LigneCommande> listeC = new List<LigneCommande>();
            LigneCommande lc = new LigneCommande();
            lc.Produit = p;
            lc.Quantite = 1;
            lc.ProduitRefId = p.Id;

            //Initilisation d'un client
            Client c = new Client();
            c.Nom = " ";
            c.Prenom = "Logan";
            c.Mail = "wolverine@gmail.com";
            c.Commandes = new List<Commande>();

            

            //Ajout de client et de produit
            ctrl.CreerClient(c);
            ctrl.CreerProduit(p);

            //Initilisation d'une Ligne Commande
            

            
            listeC.Add(lc);
            //Ajout de la commande
            ctrl.CreerCommande(ctrl.GetClients().Last(), listeC);

            Assert.AreEqual(ctrl.GetClients().Last(), ctrl.GetCommandes().Last().Client);
            
        }
    }
}
